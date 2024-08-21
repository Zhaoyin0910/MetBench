using LiteDB;
using MetBench_Domain;
using MetBench_IDAL;
using System.Collections.ObjectModel;
using System.Text;


namespace MetBench_DAL
{
    public class ApplicationRepository : IApplicationRepository
    {
        //数据库连接字符串 
        private string _conn;
        private DbConfig _dbConfig;

        //映射实体集合
        private ILiteCollection<MetamorphicRelation> MetamorphicRelations;
        private ILiteCollection<Application> Applications;
        private ILiteCollection<Domain> Domains;

        public ApplicationRepository()
        {
            _dbConfig = DbConfig.Instance;
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
                var name = Name.Trim();
                var id = 0;
                Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);
                var applications = Applications.FindAll().ToList();
                var applicationDictionary = applications.ToDictionary(app => app.Name);

                if (applicationDictionary.TryGetValue(Name, out var app))
                {
                    if (app != null) 
                    {
                        id = app.IdApplication;
                    }
                }

                ////通过应用程序的名称获取应用程序
                //var application = Applications.FindOne(x => x.Name.Equals(Name));
                //if (application != null)
                //{
                //    id = application.IdApplication;
                //}
                return id;
            }
        }

        /// <summary>
        ///  三表联合查询
        /// </summary>
        /// <returns>查询结果 返回 集合数量为0 result.Count()==0 则证明查询失败</returns>
        //public ObservableCollection<Applications_QueryResultData> GetAll_MIX()
        //{
        //    using (var db = new LiteDatabase(_conn))
        //    {
        //        //数据集合，相当于数据表
        //        MetamorphicRelations = db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key);
        //        Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);
        //        Domains = db.GetCollection<Domain>(_dbConfig.Domains_Collection_Key);

        //        //将数据表的全部记录保存到集合中
        //        var metamorphicRelations = new ObservableCollection<MetamorphicRelation>(MetamorphicRelations.FindAll());
        //        var applications = new ObservableCollection<Application>(Applications.FindAll());
        //        var domains = new ObservableCollection<Domain>(Domains.FindAll());

        //        //MetamorphicRelation与Application的中间类集合
        //        var metamorphicRelationApplications = new ObservableCollection<MetamorphicRelationApplication>();
        //        for (int i = 0; i < metamorphicRelations.Count; i++)
        //        {
        //            //metamorphicRelation至少有一个Application对应
        //            string str = metamorphicRelations[i].ApplicationName;
        //            if (str == string.Empty)
        //            {
        //                // 蜕变关系至少对应着一个应用程序 
        //                return new ObservableCollection<Applications_QueryResultData>();
        //            }
        //            //-------------------------------------
        //            //// 后续改成异常处理
        //            //try
        //            //{
        //            //    if (string.IsNullOrEmpty(str))
        //            //    {
        //            //        throw new ArgumentNullException("应用程序名称不能为空", "一条蜕变关系至少对应一个程序");
        //            //    }
        //            //}
        //            //catch (ArgumentNullException ex)
        //            //{
        //            //    Console.WriteLine("Exception caught: " + ex.Message);
        //            //    // Handle the exception here
        //            //}
        //            //-------------------------------------
        //            string[] strarray = str.Split(':');
        //            for (int j = 0; j < strarray.Length; j++)
        //            {
        //                var idmr = metamorphicRelations[i].IdMR;
        //                var mrapplication = new MetamorphicRelationApplication() { IdMR = idmr, ApplicationName = strarray[j] };
        //                metamorphicRelationApplications.Add(mrapplication);
        //            }
        //        }

        //        //Application与Domain中间类集合
        //        var applicationDomains = new ObservableCollection<ApplicationDomain>();
        //        for (int i = 0; i < applications.Count; i++)
        //        {
        //            //application的DomainName可能为null或者为空字符串
        //            string str = applications[i].DomainName;
        //            if (str != string.Empty && str != null)
        //            {
        //                string[] strarray = str.Split(':');
        //                for (int j = 0; j < strarray.Length; j++)
        //                {
        //                    var applicationName = applications[i].Name;
        //                    var applicationdomain = new ApplicationDomain() { ApplicationName = applicationName, DomainName = strarray[j] };
        //                    applicationDomains.Add(applicationdomain);
        //                }
        //            }
        //        }

        //        //保存查询结果集合
        //        var result = new ObservableCollection<Applications_QueryResultData>();
        //        ////当applicationDomains这一中间类集合成员为0，三表联查结果没有元素
        //        if (applicationDomains.Count > 0)
        //        {
        //            var Applications_Query = (
        //                           from MetamorphicRelation in metamorphicRelations
        //                           join MetamorphicRelationApplication in metamorphicRelationApplications on MetamorphicRelation.IdMR equals MetamorphicRelationApplication.IdMR
        //                           join Application in applications on MetamorphicRelationApplication.ApplicationName equals Application.Name
        //                           join ApplicationDomain in applicationDomains on Application.Name equals ApplicationDomain.ApplicationName
        //                           join Domain in domains on ApplicationDomain.DomainName equals Domain.Name
        //                           select new
        //                           {
        //                               application = Application,
        //                               domain = Domain
        //                           });

        //            foreach (var application_query in Applications_Query)
        //            {
        //                //两表联合查出的Application
        //                var application = application_query.application;
        //                var application1 = new Applications_QueryResultData()
        //                {
        //                    IdApplication = application.IdApplication,
        //                    Name = application.Name,
        //                    Description = application.Description,
        //                    ProgrammingLanguage = application.ProgrammingLanguage,
        //                    LinesOfCode = application.LinesOfCode,
        //                    Code = application.Code,
        //                    CodeName = application.CodeName,
        //                    SourceTestCase = application.SourceTestCase,
        //                    DOI = application.DOI,
        //                    Url = application.Url,
        //                    DomainName = application_query.domain.Name
        //                };
        //                result.Add(application1);
        //            }
        //        }
        //        return result;
        //    }
        //}

        public ObservableCollection<Applications_QueryResultData> GetAll_MIX()
        {
            using (var db = new LiteDatabase(_conn))
            {
                // 数据集合，相当于数据表  
                MetamorphicRelations = db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key);
                Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);
                Domains = db.GetCollection<Domain>(_dbConfig.Domains_Collection_Key);

                // 将数据表的全部记录保存到集合中  
                var metamorphicRelations = MetamorphicRelations.FindAll().ToList();
                var applications = Applications.FindAll().ToList();
                var domains = Domains.FindAll().ToList();

                // MetamorphicRelation与Application的中间类集合  
                var metamorphicRelationApplications = new List<MetamorphicRelationApplication>();
                foreach (var relation in metamorphicRelations)
                {
                    string str = relation.ApplicationName;
                    if (string.IsNullOrEmpty(str))
                    {
                        // 蜕变关系至少对应着一个应用程序   
                        return new ObservableCollection<Applications_QueryResultData>();
                    }

                    string[] strarray = str.Split(':');
                    foreach (var name in strarray)
                    {
                        metamorphicRelationApplications.Add(new MetamorphicRelationApplication
                        {
                            IdMR = relation.IdMR,
                            ApplicationName = name.Trim() // 去除多余空格  
                        });
                    }
                }

                // Application与Domain中间类集合  
                var applicationDomains = new List<ApplicationDomain>();
                foreach (var application in applications)
                {
                    string str = application.DomainName;
                    if (!string.IsNullOrEmpty(str))
                    {
                        string[] strarray = str.Split(':');
                        foreach (var domainName in strarray)
                        {
                            applicationDomains.Add(new ApplicationDomain
                            {
                                ApplicationName = application.Name,
                                DomainName = domainName.Trim() // 去除多余空格  
                            });
                        }
                    }
                }

                // 保存查询结果集合  
                var result = new List<Applications_QueryResultData>();

                // 当 applicationDomains 这一中间类集合成员为 0，三表联查结果没有元素  
                if (applicationDomains.Count > 0)
                {
                    var Applications_Query = from relation in metamorphicRelations
                                             join relationApp in metamorphicRelationApplications on relation.IdMR equals relationApp.IdMR
                                             join app in applications on relationApp.ApplicationName equals app.Name
                                             join appDomain in applicationDomains on app.Name equals appDomain.ApplicationName
                                             join domain in domains on appDomain.DomainName equals domain.Name
                                             select new
                                             {
                                                 application = app,
                                                 domain = domain
                                             };

                    foreach (var application_query in Applications_Query)
                    {
                        var application = application_query.application;
                        var applicationResult = new Applications_QueryResultData
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
                        result.Add(applicationResult);
                    }
                }

                return new ObservableCollection<Applications_QueryResultData>(result);
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
        ///  通过Name进行模糊查询应用程序
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public ObservableCollection<Applications_QueryResultData> GetByName(string Name)
        {
            var applications_queryResultData = GetAll_MIX();

            using (var db = new LiteDatabase(_conn))
            {
                //Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);
                var app_Querys = applications_queryResultData.Where(x => x.Name.Contains(Name)).ToList();
                //var result = (ObservableCollection<Application>(){ applications});
                var result = new ObservableCollection<Applications_QueryResultData>(app_Querys);
                return result;
            }
        }

        /// <summary>
        /// 添加应用程序
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        //public bool Add(Application application)
        //{
        //    using (var db = new LiteDatabase(_conn))
        //    {
        //        Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);
        //        Domains = db.GetCollection<Domain>(_dbConfig.Domains_Collection_Key);
        //        //判断application是否已经在库中
        //        if (application.IdApplication == 0)
        //        {
        //            //application.DomainName以:为分割符 
        //            //保存关联的应用程序的名称
        //            var str = application.DomainName;
        //            if (!string.IsNullOrEmpty(str))
        //            {
        //                //对应用程序名称进行分割 分割符为:
        //                var strarray = str.Split(":").ToList();
        //                var newstr = "";
        //                for (int i = 0; i < strarray.Count; i++)
        //                {
        //                    var name = strarray[i];
        //                    var domain = Domains.FindOne(x => x.Name == name);
        //                    if (domain != null)
        //                    {
        //                        if (i == 0)
        //                        {
        //                            newstr += strarray[i];
        //                        }
        //                        else
        //                        {
        //                            newstr += ":";
        //                            newstr += strarray[i];
        //                        }
        //                    }
        //                }
        //                application.DomainName = newstr;
        //            }

        //            var _id = Applications.Insert(application);
        //            return _id > 0;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        public bool Add(Application application)
        {
            using (var db = new LiteDatabase(_conn))
            {
                Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);
                Domains = db.GetCollection<Domain>(_dbConfig.Domains_Collection_Key);

                // 判断 application 是否已经在库中  
                if (application.IdApplication == 0)
                {
                    // application.DomainName 以 : 为分割符   
                    var str = application.DomainName;
                    if (!string.IsNullOrEmpty(str))
                    {
                        // 对应用程序名称进行分割，分割符为 :  
                        var strarray = str.Split(':');
                        var newstr = new StringBuilder();
                        var domainDictionary = Domains.FindAll().ToDictionary(domain => domain.Name);

                        for (int i = 0; i < strarray.Length; i++)
                        {
                            var name = strarray[i].Trim(); // 去除多余空格  
                                                           // 检查域名是否存在  
                            if (domainDictionary.ContainsKey(name))
                            {
                                if (newstr.Length > 0)
                                {
                                    newstr.Append(":");
                                }
                                newstr.Append(name);
                            }
                        }

                        application.DomainName = newstr.ToString();
                    }

                    var _id = Applications.Insert(application);
                    return _id != null; // 如果插入成功，返回 true  
                }
                else
                {
                    return false; // 如果 IdApplication 不为 0，返回 false  
                }
            }
        }


        /// <summary>
        /// 修改应用程序
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        //public bool Modify(Application application)
        //{
        //    using (var db = new LiteDatabase(_conn))
        //    {

        //        Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);
        //        Domains = db.GetCollection<Domain>(_dbConfig.Domains_Collection_Key);

        //        if (Applications.FindOne(x => x.IdApplication == application.IdApplication) != null)
        //        {
        //            var result = false;
        //            var beforeapplication = Applications.FindById(application.IdApplication);
        //            //避免对应用程序的名称进行修改
        //            var beforeapplicationname = beforeapplication.Name;

        //            //外键Name进行修改，对应的MR关联的ApplicationName也需要进行修改
        //            if (beforeapplicationname != application.Name)
        //            {
        //                MetamorphicRelations = db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key);
        //                var metamorphicRelations = new List<MetamorphicRelation>(MetamorphicRelations.FindAll().Where(x => x.ApplicationName.Contains(beforeapplicationname)));

        //                if (metamorphicRelations != null)
        //                {
        //                    var length = metamorphicRelations.Count;
        //                    for (int i = 0; i < length; i++)
        //                    {
        //                        //根据Id获取蜕变关系
        //                        var idmr = metamorphicRelations[i].IdMR;
        //                        var metamorphicRelation = MetamorphicRelations.FindById(idmr);
        //                        if (metamorphicRelation == null)
        //                        {
        //                            continue;
        //                        }
        //                        //蜕变关系对应的应用程序名称
        //                        string strappname = metamorphicRelation.ApplicationName;
        //                        //关联的应用程序名称
        //                        List<string> strlist = strappname.Split(":").ToList();

        //                        //保存修改后的关联应用程序名称
        //                        string newstrappname = "";

        //                        //关联了多个应用程序
        //                        if (strlist.Count > 1)
        //                        {
        //                            for (int j = 0; j < strlist.Count; j++)
        //                            {
        //                                if (strlist[j] == beforeapplicationname)
        //                                {
        //                                    //修改为更新后的Application的Name
        //                                    strlist[j] = application.Name;
        //                                }
        //                            }
        //                            for (int j = 0; j < strlist.Count; j++)
        //                            {
        //                                if (j == 0)
        //                                {
        //                                    newstrappname += strlist[j];
        //                                }
        //                                else
        //                                {
        //                                    newstrappname += ":";
        //                                    newstrappname += strlist[j];
        //                                }

        //                            }
        //                            metamorphicRelation.ApplicationName = newstrappname;
        //                            MetamorphicRelations.Update(metamorphicRelation);
        //                        }
        //                        else
        //                        {
        //                            //只关联了一个Application 直接进行更新ApplicationName 
        //                            metamorphicRelation.ApplicationName = application.Name;
        //                            MetamorphicRelations.Update(metamorphicRelation);
        //                        }
        //                    }
        //                }
        //            }

        //            //application.DomainName以:为分割符 
        //            //保存关联的应用程序的名称
        //            var str = application.DomainName;
        //            if (str != string.Empty && str != null)
        //            {
        //                //对应用程序名称进行分割 分割符为:
        //                var strarray = str.Split(":").ToList();
        //                var newstr = "";
        //                for (int i = 0; i < strarray.Count; i++)
        //                {
        //                    var name = strarray[i];
        //                    var domain = Domains.FindOne(x => x.Name == name);
        //                    if (domain != null)
        //                    {
        //                        if (i == 0)
        //                        {
        //                            newstr += strarray[i];
        //                        }
        //                        else
        //                        {
        //                            newstr += ":";
        //                            newstr += strarray[i];
        //                        }
        //                    }
        //                }
        //                application.DomainName = newstr;
        //            }
        //            result = Applications.Update(application);
        //            return result;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        public bool Modify(Application application)
        {
            using (var db = new LiteDatabase(_conn))
            {
                Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);
                Domains = db.GetCollection<Domain>(_dbConfig.Domains_Collection_Key);

                // 判断 application 是否在库中  
                var existingApplication = Applications.FindOne(x => x.IdApplication == application.IdApplication);
                if (existingApplication == null)
                {
                    return false;
                }

                var beforeApplicationName = existingApplication.Name;

                // 外键 Name 进行修改，对应的 MR 关联的 ApplicationName 也需要进行修改  
                if (beforeApplicationName != application.Name)
                {
                    MetamorphicRelations = db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key);
                    var metamorphicRelations = MetamorphicRelations.FindAll()
                        .Where(x => x.ApplicationName.Contains(beforeApplicationName))
                        .ToList();

                    foreach (var metamorphicRelation in metamorphicRelations)
                    {
                        // 更新蜕变关系中的应用程序名称  
                        var strAppNames = metamorphicRelation.ApplicationName.Split(':').ToList();
                        for (int j = 0; j < strAppNames.Count; j++)
                        {
                            if (strAppNames[j] == beforeApplicationName)
                            {
                                strAppNames[j] = application.Name; // 修改为更新后的 Application 的 Name  
                            }
                        }

                        metamorphicRelation.ApplicationName = string.Join(":", strAppNames);
                        MetamorphicRelations.Update(metamorphicRelation);
                    }
                }

                // 处理 application.DomainName  
                var domainNameStr = application.DomainName;
                if (!string.IsNullOrEmpty(domainNameStr))
                {
                    var domainDictionary = Domains.FindAll().ToDictionary(domain => domain.Name);
                    var strArray = domainNameStr.Split(':');
                    var newDomainNames = new StringBuilder();

                    for (int i = 0; i < strArray.Length; i++)
                    {
                        var domainName = strArray[i].Trim(); // 去除多余空格  
                        if (domainDictionary.ContainsKey(domainName))
                        {
                            if (newDomainNames.Length > 0)
                            {
                                newDomainNames.Append(":");
                            }
                            newDomainNames.Append(domainName);
                        }
                    }

                    application.DomainName = newDomainNames.ToString();
                }

                return Applications.Update(application); // 更新应用程序并返回结果  
            }
        }


        /// <summary>
        /// 删除应用程序
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        //public bool Remove(Application application)
        //{
        //    using (var db = new LiteDatabase(_conn))
        //    {
        //        Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);
        //        MetamorphicRelations = db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key);

        //        if (Applications.FindOne(x => x.IdApplication == application.IdApplication) != null)
        //        {
        //            var beforeapplication = Applications.FindById(application.IdApplication);
        //            //避免对应用程序的名称进行修改
        //            var beforeapplicationname = beforeapplication.Name;

        //            var metamorphicRelations = new List<MetamorphicRelation>(MetamorphicRelations.FindAll().Where(x => x.ApplicationName.Contains(beforeapplicationname)));
        //            //if (metamorphicRelations != null)
        //            //{
        //            //    var length = metamorphicRelations.Count;
        //            //    for (int i = 0; i < length; i++)
        //            //    {
        //            //        var metamorphicRelation = metamorphicRelations[i];
        //            //        var applicationName = metamorphicRelation.ApplicationName;
        //            //        var strarray = applicationName.Split(":").ToList();
        //            //        var newstr = "";
        //            //        for (int j = 0; j < strarray.Count; j++) 
        //            //        {
        //            //            var name = strarray[j];
        //            //            if (name != beforeapplicationname) 
        //            //            {

        //            //            }
        //            //            //var app = Applications.FindOne(x => x.Name == name);
        //            //        }
        //            //    }
        //            //}


        //            if (metamorphicRelations != null)
        //            //if ( metamorphicRelationid != null && metamorphicRelationid != string.Empty)
        //            {
        //                //将对应的mr全部删除
        //                //string str = application.MetamorphicRelationId;

        //                //string str = metamorphicRelationid;
        //                var length = metamorphicRelations.Count;
        //                //List<string> strlist = str.Split(":").ToList();//以冒号为分界符 如果没有:则返回自身
        //                for (int i = 0; i < length; i++)
        //                {

        //                    ////将string类型转换为int类型
        //                    ////int idmr = int.Parse(strlist[i]);
        //                    //int idmr;
        //                    ////要判断字符串为int类型才行
        //                    //if (int.TryParse(strlist[i], out idmr))
        //                    //{
        //                    //    idmr = int.Parse(strlist[i]);
        //                    //}
        //                    //else
        //                    //{
        //                    //    strlist.Remove(strlist[i]);
        //                    //    continue;
        //                    //}

        //                    //根据Id获取蜕变关系
        //                    var idmr = metamorphicRelations[i].IdMR;
        //                    var metamorphicRelation = MetamorphicRelations.FindById(idmr);
        //                    if (metamorphicRelation == null)
        //                    {
        //                        continue;
        //                    }
        //                    //蜕变关系对应的应用程序名称
        //                    string str = metamorphicRelation.ApplicationName;
        //                    //关联的应用程序名称
        //                    List<string> strarray = str.Split(":").ToList();

        //                    //保存修改后的关联应用程序名称
        //                    string newstr = "";

        //                    //关联了多个应用程序
        //                    if (strarray.Count > 1)
        //                    {
        //                        for (int j = 0; j < strarray.Count; j++)
        //                        {
        //                            if (strarray[j] == beforeapplicationname)
        //                            {
        //                                strarray.Remove(strarray[j]);
        //                            }
        //                        }
        //                        for (int j = 0; j < strarray.Count; j++)
        //                        {
        //                            if (j == 0)
        //                            {
        //                                newstr += strarray[j];
        //                            }
        //                            else
        //                            {
        //                                newstr += ":";
        //                                newstr += strarray[j];
        //                            }

        //                        }
        //                        metamorphicRelation.ApplicationName = newstr;
        //                        MetamorphicRelations.Update(metamorphicRelation);
        //                    }
        //                    else
        //                    {
        //                        //只关联了一个Application 直接删除该蜕变关系 
        //                        MetamorphicRelations.Delete(idmr);
        //                    }
        //                }
        //            }

        //            var result = Applications.Delete(application.IdApplication);
        //            return result;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        public bool Remove(Application application)
        {
            using (var db = new LiteDatabase(_conn))
            {
                Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);
                MetamorphicRelations = db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key);

                // 查找要删除的应用程序  
                var existingApplication = Applications.FindOne(x => x.IdApplication == application.IdApplication);
                if (existingApplication == null)
                {
                    return false; // 应用程序不存在  
                }

                var beforeApplicationName = existingApplication.Name;

                // 获取所有关联的蜕变关系  
                var metamorphicRelations = MetamorphicRelations.FindAll()
                    .Where(x => x.ApplicationName.Contains(beforeApplicationName))
                    .ToList();

                // 使用字典进行快速查找  
                var metamorphicRelationDict = metamorphicRelations.ToDictionary(mr => mr.IdMR);

                foreach (var metamorphicRelation in metamorphicRelations)
                {
                    var strArray = metamorphicRelation.ApplicationName.Split(':').ToList();

                    // 移除要删除的应用程序名称  
                    strArray.RemoveAll(name => name == beforeApplicationName);

                    if (strArray.Count > 0)
                    {
                        // 更新蜕变关系中的应用程序名称  
                        metamorphicRelation.ApplicationName = string.Join(":", strArray);
                        MetamorphicRelations.Update(metamorphicRelation);
                    }
                    else
                    {
                        // 删除蜕变关系  
                        MetamorphicRelations.Delete(metamorphicRelation.IdMR);
                    }
                }

                // 删除应用程序  
                return Applications.Delete(application.IdApplication);
            }
        }

    }
}
