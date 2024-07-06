using LiteDB;
using MetBench_Domain;
using MetBench_IDAL;
using System.Collections.ObjectModel;
using MetBench_DAL;
using System.Security.Cryptography;


namespace MetBench_DAL
{
    public class MetamorphicRelationRepository : IMetamorphicRelationRepository
    {

        //数据库连接字符串 
        private string _conn;
        private DbConfig _dbConfig;
        //对象集合
        private ILiteCollection<MetamorphicRelation> MetamorphicRelations;
        private ILiteCollection<Application> Applications;
        private ILiteCollection<Domain> Domains;
        public MetamorphicRelationRepository()
        {
            //获得DbConfig对象
            _dbConfig = DbConfig.GetInstance();
            _conn = _dbConfig._conn;
        }

        /// <summary>
        /// 三表连接查询
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<MetamorphicRelations_QueryResultData> GetAll_MIX()
        {
            using (var db = new LiteDatabase(_conn))
            {

                MetamorphicRelations = db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key);
                Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);
                Domains = db.GetCollection<Domain>(_dbConfig.Domains_Collection_Key);


                var metamorphicRelations = new ObservableCollection<MetamorphicRelation>(MetamorphicRelations.FindAll());
                var applications = new ObservableCollection<Application>(Applications.FindAll());
                var domains = new ObservableCollection<Domain>(Domains.FindAll());

                //MetamorphicRelation与Application中间类集合 相当于字典
                var metamorphicRelationApplications = new ObservableCollection<MetamorphicRelationApplication>();
                for (int i = 0; i < metamorphicRelations.Count; i++)
                {
                    string str = metamorphicRelations[i].ApplicationName;//一条蜕变关系至少对应一个应用程序
                    string[] strarray = str.Split(':');//以冒号为分界符 如果没有:则返回自身
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
                    string str = applications[i].DomainName;//一个应用程序可以不对应应用领域，也可以对应多个应用领域
                    //去除DomainName为null或为空字符串
                    if (str != string.Empty || str != null)
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
                //查询结果
                var result = new ObservableCollection<MetamorphicRelations_QueryResultData>();
                //如果Applications表与Domains表没有数据关联，即Applications表中没有一条记录的信息包含Domain 
                if (applicationDomains.Count > 0)
                {
                    //使用Linq语句来查询
                    var MRs_Query = (
                 from MetamorphicRelation in metamorphicRelations
                 join MetamorphicRelationApplication in metamorphicRelationApplications on MetamorphicRelation.IdMR equals MetamorphicRelationApplication.IdMR
                 join Application in applications on MetamorphicRelationApplication.ApplicationName equals Application.Name
                 join ApplicationDomain in applicationDomains on Application.Name equals ApplicationDomain.ApplicationName
                 join Domain in domains on ApplicationDomain.DomainName equals Domain.Name
                 select new
                 {
                     metamorphicRelation = MetamorphicRelation,
                     application = Application
                 });

                    foreach (var mr_query in MRs_Query)
                    {
                        //三表联合查出的MR
                        var mr = mr_query.metamorphicRelation;
                        var app = mr_query.application;
                        var mr1 = new MetamorphicRelations_QueryResultData()
                        {
                            IdMR = mr.IdMR,
                            Description = mr.Description,
                            Context = mr.Context,
                            Constraint = mr.Constraint,
                            OrderOfMR = mr.OrderOfMR,
                            RepresentationType = mr.RepresentationType,
                            InputPatternImageData = mr.InputPatternImageData,
                            OutputPatternImageData = mr.OutputPatternImageData,
                            InputPattern = mr.InputPattern,
                            OutputPattern = mr.OutputPattern,
                            DimensionOfInputPattern = mr.DimensionOfInputPattern,
                            DimensionOfOutputPattern = mr.DimensionOfOutputPattern,
                            ApplicationName = app.Name,
                            DomainName = mr_query.application.DomainName,
                            CodeName = mr_query.application.CodeName //Code的Name 

                        };
                        //将图片保存到本地 并返回图片路径
                        var dt = new DatatoImage();
                        mr1.InputPatternImagepath = dt.ConvertByteArrayToImage(mr.InputPatternImageData);
                        mr1.OutputPatternImagepath = dt.ConvertByteArrayToImage(mr.OutputPatternImageData);
                        result.Add(mr1);
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// 两表连接查询
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<MetamorphicRelations_QueryResultData> GetAll_MIXTwoTable()
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
                //MetamorphicRelation与Application中间类集合

                var metamorphicRelationApplications = new ObservableCollection<MetamorphicRelationApplication>();
                for (int i = 0; i < metamorphicRelations.Count; i++)
                {
                    string str = metamorphicRelations[i].ApplicationName;
                    string[] strarray = str.Split(':');
                    for (int j = 0; j < strarray.Length; j++)
                    {
                        var idmr = metamorphicRelations[i].IdMR;
                        var mrapplication = new MetamorphicRelationApplication() { IdMR = idmr, ApplicationName = strarray[j] };
                        metamorphicRelationApplications.Add(mrapplication);
                    }
                }

                var result = new ObservableCollection<MetamorphicRelations_QueryResultData>();
                //使用Linq语句来查询
                var MRs_Query = (
                 from MetamorphicRelation in metamorphicRelations
                 join MetamorphicRelationApplication in metamorphicRelationApplications on MetamorphicRelation.IdMR equals MetamorphicRelationApplication.IdMR
                 join Application in applications on MetamorphicRelationApplication.ApplicationName equals Application.Name
                 select new
                 {
                     metamorphicRelation = MetamorphicRelation,
                     application = Application
                 });

                //将每一条查询结果保存到查询结果集合
                foreach (var mr_query in MRs_Query)
                {
                    //两表联合查出的MR
                    var mr = mr_query.metamorphicRelation;
                    var app = mr_query.application;
                    var mr1 = new MetamorphicRelations_QueryResultData()
                    {
                        IdMR = mr.IdMR,
                        Description = mr.Description,
                        Context = mr.Context,
                        Constraint = mr.Constraint,
                        OrderOfMR = mr.OrderOfMR,
                        RepresentationType = mr.RepresentationType,
                        InputPatternImageData = mr.InputPatternImageData,
                        OutputPatternImageData = mr.OutputPatternImageData,
                        InputPattern = mr.InputPattern,
                        OutputPattern = mr.OutputPattern,
                        DimensionOfInputPattern = mr.DimensionOfInputPattern,
                        DimensionOfOutputPattern = mr.DimensionOfOutputPattern,
                        ApplicationName = app.Name,
                        //DomainName = mr_query.application.DomainName,
                        CodeName = mr_query.application.CodeName
                    };
                    if (mr_query.application.DomainName != null && mr_query.application.DomainName != string.Empty)
                    {
                        mr1.DomainName = mr_query.application.DomainName;
                    }
                    else
                    {
                        //用空字符串占位
                        mr1.DomainName = "";
                    }
                    var dt = new DatatoImage();
                    mr1.InputPatternImagepath = dt.ConvertByteArrayToImage(mr.InputPatternImageData);
                    mr1.OutputPatternImagepath = dt.ConvertByteArrayToImage(mr.OutputPatternImageData);

                    result.Add(mr1);
                }
                return result;
            }
        }

        //3.根据复合条件查询  OrderOfMR、 RepresentationType、DimensionOfInputPattern、DimensionOfOutputPattern、Application的Name、Domain的Name
        //public ObservableCollection<MetamorphicRelation> MultiQuery(MetamorphicRelation metamorphicRelation)
        public ObservableCollection<MetamorphicRelations_QueryResultData> Get(MetamorphicRelation metamorphicRelation, string domainName)
        {
            using (var db = new LiteDatabase(_conn))
            {
                if (metamorphicRelation != null)
                {
                    //metamorphicRelation.Application.Domains.Add(new Domain(){Name = Domain的Name})   domain(保存了Domain的Name)
                    //使用linq语句查询 两表联合
                    MetamorphicRelations = db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key);
                    Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);
                    Domains = db.GetCollection<Domain>(_dbConfig.Domains_Collection_Key);


                    var metamorphicRelations = new ObservableCollection<MetamorphicRelation>(MetamorphicRelations.FindAll());
                    var applictaions = new ObservableCollection<Application>(Applications.FindAll());
                    var domains = new ObservableCollection<Domain>(Domains.FindAll());
                    //中间类集合
                    var metamorphicRelationApplications = new ObservableCollection<MetamorphicRelationApplication>();
                    for (int i = 0; i < metamorphicRelations.Count; i++)
                    {
                        string str = metamorphicRelations[i].ApplicationName;
                        string[] strarray = str.Split(':');
                        for (int j = 0; j < strarray.Length; j++)
                        {
                            var idmr = metamorphicRelations[i].IdMR;
                            var mrapplication = new MetamorphicRelationApplication() { IdMR = idmr, ApplicationName = strarray[j] };
                            metamorphicRelationApplications.Add(mrapplication);
                        }
                    }
                    //metamorphicRelation保存了OrderOfMR、 RepresentationType、DimensionOfInputPattern、DimensionOfOutputPattern
                    var orderofMR = metamorphicRelation.OrderOfMR; //可能为空字符串
                    var representationType = metamorphicRelation.RepresentationType; //可能为-1
                    var dimensionOfInputPattern = metamorphicRelation.DimensionOfInputPattern; //可能为空字符串
                    var dimensionOfOutputPattern = metamorphicRelation.DimensionOfOutputPattern; //可能为空字符串
                    //获取Application的Name  metamorphicRelation.Application.Name 保存 Application的Name
                    var applicationName = metamorphicRelation.ApplicationName; //可能为空字符串
                    //获取Domain的Name metamorphicRelation.Application.Domains.Add(new Domain(){Name = Domain的Name})   domain(保存了Domain的Name) 只输入一个domainname
                    var domainName1 = domainName; //可能为空字符串



                    //修改后
                    var MRs_Query1 = (
                             from MetamorphicRelation in metamorphicRelations
                             join MetamorphicRelationApplication in metamorphicRelationApplications on MetamorphicRelation.IdMR equals MetamorphicRelationApplication.IdMR
                             join Application in applictaions on MetamorphicRelationApplication.ApplicationName equals Application.Name
                             select new
                             {
                                 metamorphicRelation = MetamorphicRelation,
                                 application = Application,
                             }
                        );
                    //两表联合查询 MetamorphicRealtions 与 Applications表联合查询
                    var MRs_Query2 = MRs_Query1.ToList();

                    for (int i = 0; i < MRs_Query2.Count; i++)
                    {
                        //应用程序对应的应用领域可能为null
                        var domainname = MRs_Query2[i].application.DomainName;
                        if (domainname == null)
                        {
                            //用空字符串占位
                            MRs_Query2[i].application.DomainName = "";
                        }
                    }

                    //使用Linq语句进行查询
                    var MRs_Query = MRs_Query2.Where(x =>
                    (x.metamorphicRelation.OrderOfMR.Contains(metamorphicRelation.OrderOfMR))
                    && (x.metamorphicRelation.RepresentationType.ToString().Contains(metamorphicRelation.RepresentationType.ToString()) || (int)representationType == -1)
                    && (x.metamorphicRelation.DimensionOfInputPattern.Contains(metamorphicRelation.DimensionOfInputPattern))
                    && (x.metamorphicRelation.DimensionOfOutputPattern.Contains(metamorphicRelation.DimensionOfOutputPattern)
                    &&(x.application.Name.Contains(applicationName))
                    && (x.application.DomainName.Contains(domainName1)))); //当选了全部关系的话，在UI层将OrderOfMR 赋值为"" 即空字符串
                    var result = new ObservableCollection<MetamorphicRelations_QueryResultData>();
                    foreach (var mr_query in MRs_Query)
                    {
                        //两表联合查出的MR
                        var mr = mr_query.metamorphicRelation;
                        var app = mr_query.application;

                        var mr1 = new MetamorphicRelations_QueryResultData()
                        {
                            IdMR = mr.IdMR,
                            Description = mr.Description,
                            Context = mr.Context,
                            Constraint = mr.Constraint,
                            OrderOfMR = mr.OrderOfMR,
                            RepresentationType = mr.RepresentationType,
                            InputPattern = mr.InputPattern,
                            OutputPattern = mr.OutputPattern,
                            InputPatternImageData = mr.InputPatternImageData,
                            OutputPatternImageData = mr.OutputPatternImageData,//图片数据
                            DimensionOfInputPattern = mr.DimensionOfInputPattern,
                            DimensionOfOutputPattern = mr.DimensionOfOutputPattern,
                            ApplicationName = app.Name,
                            CodeName = mr_query.application.CodeName
                        };
                        mr1.DomainName = mr_query.application.DomainName;//已经进行了非空处理

                        //绑定Latex渲染图片
                        var dt = new DatatoImage();
                        mr1.InputPatternImagepath = dt.ConvertByteArrayToImage(mr.InputPatternImageData);
                        mr1.OutputPatternImagepath = dt.ConvertByteArrayToImage(mr.OutputPatternImageData);
                        result.Add(mr1);
                    }
                    return result;
                }
                else
                {
                    return null;
                }

            }
        }
      
        public ObservableCollection<MetamorphicRelation> GetAll()
        {
            using (var db = new LiteDatabase(_conn))
            {
                MetamorphicRelations = db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key);
                var result = new ObservableCollection<MetamorphicRelation>(MetamorphicRelations.FindAll());
                return result;
            }
        }
        public MetamorphicRelation Get(int id)
        {
            using (var db = new LiteDatabase(_conn))
            {
                MetamorphicRelations = db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key);
                var metamorphicRelation = MetamorphicRelations.FindById(id);
                return metamorphicRelation;
            }
        }


        public bool Add(MetamorphicRelation metamorphicRelation)
        {
            //标识： MetamorphicRelation.Application没有的话 ApplicationName为String.Empty;
            using (var db = new LiteDatabase(_conn))
            {
                if (db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key).FindById(metamorphicRelation.IdMR) == null)
                {
                    MetamorphicRelations = db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key);
                    Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);

                    var _id = MetamorphicRelations.Insert(metamorphicRelation);

                    return _id > 0;

                }
                else
                {
                    return false;
                }
            }
        }

        public bool Modify(MetamorphicRelation metamorphicRelation)
        {
            using (var db = new LiteDatabase(_conn))
            {
                if (db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key).FindById(metamorphicRelation.IdMR) != null)
                {
                    var result = false;
                    //var result1 = false;
                    Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);
                    MetamorphicRelations = db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key);

                    //直接改就行了
                    result = MetamorphicRelations.Update(metamorphicRelation);
                    return result;
                }
                else
                {
                    return false;

                }
            }
        }

        public bool Remove(MetamorphicRelation metamorphicRelation)
        {
            using (var db = new LiteDatabase(_conn))
            {
                if (db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key).FindById(metamorphicRelation.IdMR) != null)
                {


                    MetamorphicRelations = db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key);
                    Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);

                    //避免对应用程序进行了修改
                    var beforemetamorphicRelation = MetamorphicRelations.FindById(metamorphicRelation.IdMR);
                    string str = beforemetamorphicRelation.ApplicationName;
                    //string str = metamorphicRelation.ApplicationName;//一条蜕变关系至少关联一个或多个应用程序 ， 即蜕变关系的ApplicationName非空
                    List<String> strlist = metamorphicRelation.ApplicationName.Split(":").ToList();
                    //取消对应的应用程序的关联
                    for (int i = 0; i < strlist.Count; i++)
                    {

                        var name = strlist[i];
                        var application = Applications.FindOne(x => x.Name == name);
                        //保存关联的蜕变关系的ID
                        var str1 = application.MetamorphicRelationId;
                        //字符串进行操作 删除指定的MetamorphicRelation的ID
                        var mrIdstr = metamorphicRelation.IdMR.ToString();
                        //将关联的蜕变关系的ID，以:拆分
                        var strarray = str1.Split(":").ToList();
                        //保存修改后的蜕变关系ID
                        var nowstr = "";
                        for(int j = 0; j < strarray.Count; j++) 
                        {
                            if (strarray[j] == mrIdstr)
                            {
                                strarray.Remove(strarray[j]);
                            }
                   
                        }
                        for (int j = 0; j < strarray.Count; j++) 
                        {
                            if (j == 0)
                            {
                                nowstr += strarray[j];

                            }
                            else 
                            {
                                nowstr += ":";
                                nowstr += strarray[j];
                            }
                        }
                        application.MetamorphicRelationId = nowstr;
                        Applications.Update(application);
                    }

                    var result = MetamorphicRelations.Delete(metamorphicRelation.IdMR);

                    return result;

                }
                else
                {
                    return false;

                }
            }
        }



        //暂时不需要
        public ObservableCollection<MetamorphicRelation> Get(MetamorphicRelation entity)
        {
            throw new NotImplementedException();
        }

        //添加蜕变关系并且返回
        public int Add_Return_ID(MetamorphicRelation metamorphicRelation)
        {
            using (var db = new LiteDatabase(_conn))
            {
                if (db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key).FindById(metamorphicRelation.IdMR) == null)
                {

                    var result = false;
                    MetamorphicRelations = db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key);

                    var _id = MetamorphicRelations.Insert(metamorphicRelation);
                    var id = MetamorphicRelations.FindById(_id).IdMR;
                    return id;
                }
                else
                {
                    //返回0 表示添加失败！
                    return 0;
                }
            }
        }
    }
}