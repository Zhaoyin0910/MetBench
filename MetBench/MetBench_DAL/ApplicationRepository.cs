using LiteDB;
using MetBench_Domain;
using MetBench_IDAL;
using System.Collections.ObjectModel;


namespace MetBench_DAL
{
    public class ApplicationRepository : IApplicationRepository
    {
        //数据库连接字符串 
        private string _conn;
        private DbConfig _dbConfig;

        private ILiteCollection<MetamorphicRelation> MetamorphicRelations;
        private ILiteCollection<Application> Applications;
        private ILiteCollection<Domain> Domains;
        public ApplicationRepository()
        {
            _dbConfig = DbConfig.GetInstance();
            _conn = _dbConfig._conn;
        }

        /// <summary>
        /// 获得应用程序的Id
        /// </summary>
        /// <param name="Name">应用程序的名称</param>
        /// <returns>应用程序的Id</returns>
        //返回id
        public int Get(string Name)
        {
            using (var db = new LiteDatabase(_conn))
            {
                //返回0 表示应用名称为Name的应用程序在数据表中不存在
                var id = 0;
                Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);
                //通过应用程序的名称获取应用程序
                var application = Applications.FindOne(x => x.Name.Equals(Name));
                if (application != null)
                {
                    id = application.IdApplication;
                }
                return id;
            }
        }

        /// <summary>
        ///  三表联合查询
        /// </summary>
        /// <returns>查询结果</returns>
        public ObservableCollection<Applications_QueryResultData> GetAll_MIX()
        {
            using (var db = new LiteDatabase(_conn))
            {
                //数据集合，相当于数据表
                MetamorphicRelations = db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key);
                Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);
                Domains = db.GetCollection<Domain>(_dbConfig.Domains_Collection_Key);

                //将数据表的全部记录保存到集合中
                var metamorphicRelations = new ObservableCollection<MetamorphicRelation>(MetamorphicRelations.FindAll());
                var applications = new ObservableCollection<Application>(Applications.FindAll());
                var domains = new ObservableCollection<Domain>(Domains.FindAll());

                //MetamorphicRelation与Application的中间类集合
                var metamorphicRelationApplications = new ObservableCollection<MetamorphicRelationApplication>();
                for (int i = 0; i < metamorphicRelations.Count; i++)
                {
                    //metamorphicRelation至少有一个Application对应
                    string str = metamorphicRelations[i].ApplicationName;
                    string[] strarray = str.Split(':');
                    for (int j = 0; j < strarray.Length; j++)
                    {
                        var idmr = metamorphicRelations[i].IdMR;
                        var mrapplication = new MetamorphicRelationApplication() { IdMR = idmr, ApplicationName = strarray[j] };
                        metamorphicRelationApplications.Add(mrapplication);
                    }
                }
                //Application与Domain中间类集合
                var applicationDomains = new ObservableCollection<ApplicationDomain>();
                for (int i = 0; i < applications.Count; i++)
                {
                    //application的DomainName可能为null或者为空字符串
                    string str = applications[i].DomainName;
                    if (str != string.Empty && str != null)
                    {
                        string[] strarray = str.Split(':');
                        for (int j = 0; j < strarray.Length; j++)
                        {
                            var applicationName = applications[i].Name;
                            var applicationdomain = new ApplicationDomain() { ApplicationName = applicationName, DomainName = strarray[j] };
                            applicationDomains.Add(applicationdomain);
                        }
                    }
                }

                //保存查询结果集合
                var result = new ObservableCollection<Applications_QueryResultData>();
                ////当applicationDomains这一中间类集合成员为0，三表联查结果没有元素
                if (applicationDomains.Count > 0)
                {
                    var Applications_Query = (
                                   from MetamorphicRelation in metamorphicRelations
                                   join MetamorphicRelationApplication in metamorphicRelationApplications on MetamorphicRelation.IdMR equals MetamorphicRelationApplication.IdMR
                                   join Application in applications on MetamorphicRelationApplication.ApplicationName equals Application.Name
                                   join ApplicationDomain in applicationDomains on Application.Name equals ApplicationDomain.ApplicationName
                                   join Domain in domains on ApplicationDomain.DomainName equals Domain.Name
                                   select new
                                   {
                                       application = Application,
                                       domain = Domain
                                   });
                    foreach (var application_query in Applications_Query)
                    {
                        //两表联合查出的Application
                        var application = application_query.application;
                        var application1 = new Applications_QueryResultData()
                        {
                            IdApplication = application.IdApplication,
                            Name = application.Name,
                            Description = application.Description,
                            ProgrammingLanguage = application.ProgrammingLanguage,
                            LinesOfCode = application.LinesOfCode,
                            Code = application.Code,
                            CodeName = application.CodeName,
                            SourceTestCase = application.SourceTestCase,
                            DOI = application.DOI,
                            Url = application.Url,
                            DomainName = application_query.domain.Name
                        };
                        result.Add(application1);
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>数据表中全部的应用程序的集合</returns>
        public ObservableCollection<Application> GetAll()
        {
            using (var db = new LiteDatabase(_conn))
            {
                Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);
                var result = new ObservableCollection<Application>(Applications.FindAll());
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>对应Id的应用程序</returns>
        public Application Get(int id)
        {
            using (var db = new LiteDatabase(_conn))
            {
                Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);
                var application = Applications.FindById(id);
                return application;
            }
        }


        //还没开发
        public ObservableCollection<Application> Get(Application application)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 添加应用程序
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public bool Add(Application application)
        {
            using (var db = new LiteDatabase(_conn))
            {
                Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);
                //判断application是否已经在库中
                if (Applications.FindOne(x => x.IdApplication == application.IdApplication) == null)
                {
                    var _id = Applications.Insert(application);
                    return _id > 0;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 修改应用程序
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public bool Modify(Application application)
        {
            using (var db = new LiteDatabase(_conn))
            {

                Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);
                if (Applications.FindOne(x => x.IdApplication == application.IdApplication) != null)
                {
                    var result = false;
                    result = Applications.Update(application);
                    return result;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 删除应用程序
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public bool Remove(Application application)
        {
            using (var db = new LiteDatabase(_conn))
            {
                Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);
                MetamorphicRelations = db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key);
                Domains = db.GetCollection<Domain>(_dbConfig.Domains_Collection_Key);
                if (Applications.FindOne(x => x.IdApplication == application.IdApplication) != null)
                {
                    var beforeapplication = Applications.FindById(application.IdApplication);
                    //避免对应用程序的名称进行修改
                    var beforeapplicationname = beforeapplication.Name;
                    var metamorphicRelationid = beforeapplication.MetamorphicRelationId;

                    if ( metamorphicRelationid != null && metamorphicRelationid != string.Empty)
                    {
                        //将对应的mr全部删除
                        //string str = application.MetamorphicRelationId;
                        string str = metamorphicRelationid;

                        List<string> strlist = str.Split(":").ToList();//以冒号为分界符 如果没有:则返回自身
                        for (int i = 0; i < strlist.Count; i++)
                        {

                            //将string类型转换为int类型
                            //int idmr = int.Parse(strlist[i]);
                            int idmr;
                            //要判断字符串为int类型才行
                            if (int.TryParse(strlist[i], out idmr))
                            {
                                idmr = int.Parse(strlist[i]);
                            }
                            else
                            {
                                strlist.Remove(strlist[i]);
                                continue;
                            }
                            //根据Id获取蜕变关系
                            var metamorphicRelation = MetamorphicRelations.FindById(idmr);
                            if (metamorphicRelation == null)
                            {
                                continue;
                            }
                            //蜕变关系对应的应用程序名称
                            string str1 = metamorphicRelation.ApplicationName;
                            //关联的应用程序名称
                            List<string> strlist1 = str1.Split(":").ToList();

                            //保存修改后的关联应用程序名称
                            string applicationnames = "";

                            //关联了多个应用程序
                            if (strlist1.Count > 1)
                            {
                                for (int j = 0; j < strlist1.Count; j++)
                                {
                                    if (strlist1[j] == beforeapplicationname)
                                    {
                                         strlist1.Remove(strlist1[j]);
                                    }
                                }
                                for (int j = 0; j < strlist1.Count; j++)
                                {
                                    if (j == 0)
                                    {
                                        applicationnames += strlist1[j];
                                    }
                                    else
                                    {
                                        applicationnames += ":";
                                        applicationnames += strlist1[j];
                                    }

                                }
                                metamorphicRelation.ApplicationName = applicationnames;
                                MetamorphicRelations.Update(metamorphicRelation);
                            }
                            else
                            {
                                //只关联了一个Application 直接删除该蜕变关系 
                                MetamorphicRelations.Delete(idmr);
                            }
                        }
                    }

                    var result = Applications.Delete(application.IdApplication);
                    return result;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}
