using LiteDB;
using MetBench_Domain;
using MetBench_IDAL;
using System.Collections.ObjectModel;
using System.Text;


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
            _dbConfig = DbConfig.Instance;
            _conn = _dbConfig._conn;
        }

        /// <summary>
        /// 三表连接查询
        /// </summary>
        /// <returns>返回 集合数量为0 result.Count()==0 则证明查询失败</returns>
        //public ObservableCollection<MetamorphicRelations_QueryResultData> GetAll_MIX()
        //{
        //    using (var db = new LiteDatabase(_conn))
        //    {
        //        //获取数据集合
        //        MetamorphicRelations = db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key);
        //        Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);
        //        Domains = db.GetCollection<Domain>(_dbConfig.Domains_Collection_Key);

        //        //获取实体集合
        //        var metamorphicRelations = new ObservableCollection<MetamorphicRelation>(MetamorphicRelations.FindAll());
        //        var applications = new ObservableCollection<Application>(Applications.FindAll());
        //        var domains = new ObservableCollection<Domain>(Domains.FindAll());

        //        //MetamorphicRelation与Application中间类集合 相当于字典
        //        var metamorphicRelationApplications = new ObservableCollection<MetamorphicRelationApplication>();
        //        //MetamorphicRealation与Application的关系为多对多，因此MetamorphicRelation与MetamorphicRelationApplication的关系为1对多，同理Application也是1对多关系
        //        for (int i = 0; i < metamorphicRelations.Count; i++)
        //        {
        //            string str = metamorphicRelations[i].ApplicationName;//一条蜕变关系至少对应一个应用程序
        //            if (str == string.Empty || str == null)
        //            {
        //                // 蜕变关系至少对应着一个应用程序 
        //                return new ObservableCollection<MetamorphicRelations_QueryResultData>();
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
        //            string[] strarray = str.Split(':');//以冒号为分界符 如果没有:则返回自身
        //            //遍历metamorphicRelations[i]关联的Application
        //            for (int j = 0; j < strarray.Length; j++)
        //            {
        //                var idmr = metamorphicRelations[i].IdMR;
        //                // 一对多
        //                var mrapplication = new MetamorphicRelationApplication() { IdMR = idmr, ApplicationName = strarray[j] };
        //                metamorphicRelationApplications.Add(mrapplication);
        //            }
        //        }

        //        //Application与Domain中间类集合
        //        var applicationDomains = new ObservableCollection<ApplicationDomain>();
        //        for (int i = 0; i < applications.Count; i++)
        //        {
        //            string str = applications[i].DomainName;//一个应用程序可以不对应应用领域，也可以对应多个应用领域
        //            //去除DomainName为null或为空字符串 LiteDB不存储空字符串，即为null
        //            if (str != string.Empty && str != null)
        //            {
        //                string[] strarray = str.Split(':');
        //                for (int j = 0; j < strarray.Length; j++)
        //                {
        //                    var applicationName = applications[i].Name;
        //                    // 一对多
        //                    var applicationdomain = new ApplicationDomain() { ApplicationName = applicationName, DomainName = strarray[j] };
        //                    applicationDomains.Add(applicationdomain);
        //                }
        //            }
        //        }

        //        //查询结果 result.Count()>0 为联查成功
        //        var result = new ObservableCollection<MetamorphicRelations_QueryResultData>();

        //        //如果Applications表与Domains表没有数据关联，即Applications表中没有一条记录的信息包含Domain 
        //        if (applicationDomains.Count > 0)
        //        {
        //            //使用Linq语句来查询
        //            var MRs_Query = (
        //         from MetamorphicRelation in metamorphicRelations
        //         join MetamorphicRelationApplication in metamorphicRelationApplications on MetamorphicRelation.IdMR equals MetamorphicRelationApplication.IdMR
        //         join Application in applications on MetamorphicRelationApplication.ApplicationName equals Application.Name
        //         join ApplicationDomain in applicationDomains on Application.Name equals ApplicationDomain.ApplicationName
        //         join Domain in domains on ApplicationDomain.DomainName equals Domain.Name
        //         select new
        //         {
        //             metamorphicRelation = MetamorphicRelation,
        //             application = Application
        //         });

        //            foreach (var mr_query in MRs_Query)
        //            {
        //                //三表联合查出的MR
        //                var mr = mr_query.metamorphicRelation;
        //                var app = mr_query.application;
        //                var mrQuery = new MetamorphicRelations_QueryResultData()
        //                {
        //                    IdMR = mr.IdMR,
        //                    Description = mr.Description,
        //                    Context = mr.Context,
        //                    Constraint = mr.Constraint,
        //                    OrderOfMR = mr.OrderOfMR,
        //                    RepresentationType = mr.RepresentationType,
        //                    InputPatternImageData = mr.InputPatternImageData,
        //                    OutputPatternImageData = mr.OutputPatternImageData,
        //                    InputPattern = mr.InputPattern,
        //                    OutputPattern = mr.OutputPattern,
        //                    DimensionOfInputPattern = mr.DimensionOfInputPattern,
        //                    DimensionOfOutputPattern = mr.DimensionOfOutputPattern,
        //                    ApplicationName = app.Name,
        //                    CodeName = mr_query.application.CodeName //Code的Name 

        //                };
        //                //mrQuery.DomainName = mr_query.application.DomainName;
        //                if (mr_query.application.DomainName != null && mr_query.application.DomainName != string.Empty)
        //                {
        //                    mrQuery.DomainName = mr_query.application.DomainName;
        //                }
        //                else
        //                {
        //                    //用空字符串占位
        //                    mrQuery.DomainName = string.Empty;
        //                }
        //                //将图片保存到本地 并返回图片路径
        //                var dt = new DatatoImage();
        //                mrQuery.InputPatternImagepath = dt.ConvertByteArrayToImage(mr.InputPatternImageData);
        //                mrQuery.OutputPatternImagepath = dt.ConvertByteArrayToImage(mr.OutputPatternImageData);
        //                result.Add(mrQuery);
        //            }
        //        }
        //        return result;
        //    }
        //}
        public ObservableCollection<MetamorphicRelations_QueryResultData> GetAll_MIX()
        {
            using (var db = new LiteDatabase(_conn))
            {
                // 获取数据集合  
                var metamorphicRelations = db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key).FindAll().ToList();
                var applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key).FindAll().ToList();
                var domains = db.GetCollection<Domain>(_dbConfig.Domains_Collection_Key).FindAll().ToList();

                // 如果没有蜕变关系或应用程序，返回空结果  
                if (!metamorphicRelations.Any() || !applications.Any())
                {
                    return new ObservableCollection<MetamorphicRelations_QueryResultData>();
                }

                // 中间类集合  
                var metamorphicRelationApplications = new List<MetamorphicRelationApplication>();
                foreach (var relation in metamorphicRelations)
                {
                    if (string.IsNullOrEmpty(relation.ApplicationName))
                    {
                        // 蜕变关系至少对应着一个应用程序  
                        return new ObservableCollection<MetamorphicRelations_QueryResultData>();
                    }

                    var applicationNames = relation.ApplicationName.Split(':');
                    foreach (var appName in applicationNames)
                    {
                        metamorphicRelationApplications.Add(new MetamorphicRelationApplication { IdMR = relation.IdMR, ApplicationName = appName });
                    }
                }

                // Application与Domain中间类集合  
                var applicationDomains = new List<ApplicationDomain>();
                foreach (var app in applications)
                {
                    if (!string.IsNullOrEmpty(app.DomainName))
                    {
                        var domainNames = app.DomainName.Split(':');
                        foreach (var domainName in domainNames)
                        {
                            applicationDomains.Add(new ApplicationDomain { ApplicationName = app.Name, DomainName = domainName });
                        }
                    }
                }

                // 查询结果集合  
                var result = new List<MetamorphicRelations_QueryResultData>();

                if (applicationDomains.Any())
                {
                    var MRs_Query = from relation in metamorphicRelations
                                    join relationApp in metamorphicRelationApplications on relation.IdMR equals relationApp.IdMR
                                    join app in applications on relationApp.ApplicationName equals app.Name
                                    join appDomain in applicationDomains on app.Name equals appDomain.ApplicationName
                                    join domain in domains on appDomain.DomainName equals domain.Name
                                    select new
                                    {
                                        MetamorphicRelation = relation,
                                        Application = app,
                                        DomainName = appDomain.DomainName
                                    };

                    foreach (var mr_query in MRs_Query)
                    {
                        var mr = mr_query.MetamorphicRelation;
                        var app = mr_query.Application;

                        var mrQuery = new MetamorphicRelations_QueryResultData
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
                            CodeName = app.CodeName,
                            DomainName = mr_query.DomainName ?? string.Empty,
                            InputPatternImagepath = new DatatoImage().ConvertByteArrayToImage(mr.InputPatternImageData),
                            OutputPatternImagepath = new DatatoImage().ConvertByteArrayToImage(mr.OutputPatternImageData)
                        };

                        result.Add(mrQuery);
                    }
                }

                return new ObservableCollection<MetamorphicRelations_QueryResultData>(result);
            }
        }



        /// <summary>
        /// 两表连接查询
        /// </summary>
        /// <returns></returns>
        //public ObservableCollection<MetamorphicRelations_QueryResultData> GetAll_MIXTwoTable()
        //{
        //    using (var db = new LiteDatabase(_conn))
        //    {
        //        //获取数据集合
        //        MetamorphicRelations = db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key);
        //        Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);

        //        //获取实体集合
        //        var metamorphicRelations = new ObservableCollection<MetamorphicRelation>(MetamorphicRelations.FindAll());
        //        var applications = new ObservableCollection<Application>(Applications.FindAll());

        //        //MetamorphicRelation与Application中间类集合
        //        var metamorphicRelationApplications = new ObservableCollection<MetamorphicRelationApplication>();
        //        for (int i = 0; i < metamorphicRelations.Count; i++)
        //        {
        //            string str = metamorphicRelations[i].ApplicationName;
        //            if (str == string.Empty || str == null)
        //            {
        //                // 蜕变关系至少对应着一个应用程序 
        //                return new ObservableCollection<MetamorphicRelations_QueryResultData>();
        //            }
        //            //后续改成异常处理
        //            string[] strarray = str.Split(':');
        //            for (int j = 0; j < strarray.Length; j++)
        //            {
        //                var idmr = metamorphicRelations[i].IdMR;
        //                // 一对多
        //                var mrapplication = new MetamorphicRelationApplication() { IdMR = idmr, ApplicationName = strarray[j] };
        //                metamorphicRelationApplications.Add(mrapplication);
        //            }
        //        }

        //        // 查询结果 result.Count()>0 为联查成功
        //        var result = new ObservableCollection<MetamorphicRelations_QueryResultData>();

        //        // 使用Linq语句来查询
        //        var MRs_Query = (
        //         from MetamorphicRelation in metamorphicRelations
        //         join MetamorphicRelationApplication in metamorphicRelationApplications on MetamorphicRelation.IdMR equals MetamorphicRelationApplication.IdMR
        //         join Application in applications on MetamorphicRelationApplication.ApplicationName equals Application.Name
        //         select new
        //         {
        //             metamorphicRelation = MetamorphicRelation,
        //             application = Application
        //         });

        //        //将每一条查询结果保存到查询结果集合
        //        foreach (var mr_query in MRs_Query)
        //        {
        //            //两表联合查出的MR
        //            var mr = mr_query.metamorphicRelation;
        //            var app = mr_query.application;

        //            // 一条记录为一个蜕变关系对应着一个应用程序
        //            var mrQuery = new MetamorphicRelations_QueryResultData()
        //            {
        //                IdMR = mr.IdMR,
        //                Description = mr.Description,
        //                Context = mr.Context,
        //                Constraint = mr.Constraint,
        //                OrderOfMR = mr.OrderOfMR,
        //                RepresentationType = mr.RepresentationType,
        //                InputPatternImageData = mr.InputPatternImageData,
        //                OutputPatternImageData = mr.OutputPatternImageData,
        //                InputPattern = mr.InputPattern,
        //                OutputPattern = mr.OutputPattern,
        //                DimensionOfInputPattern = mr.DimensionOfInputPattern,
        //                DimensionOfOutputPattern = mr.DimensionOfOutputPattern,
        //                ApplicationName = app.Name,
        //                CodeName = mr_query.application.CodeName
        //            };
        //            //mrQuery.DomainName = mr_query.application.DomainName;
        //            if (mr_query.application.DomainName != null && mr_query.application.DomainName != string.Empty)
        //            {
        //                mrQuery.DomainName = mr_query.application.DomainName;
        //            }
        //            else
        //            {
        //                //用空字符串占位
        //                mrQuery.DomainName = string.Empty;
        //            }
        //            //将图片保存到本地 并返回图片路径
        //            var dt = new DatatoImage();
        //            mrQuery.InputPatternImagepath = dt.ConvertByteArrayToImage(mr.InputPatternImageData);
        //            mrQuery.OutputPatternImagepath = dt.ConvertByteArrayToImage(mr.OutputPatternImageData);

        //            result.Add(mrQuery);
        //        }
        //        return result;
        //    }
        //}
        public ObservableCollection<MetamorphicRelations_QueryResultData> GetAll_MIXTwoTable()
        {
            using (var db = new LiteDatabase(_conn))
            {
                // 获取数据集合  
                var metamorphicRelations = db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key).FindAll().ToList();
                var applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key).FindAll().ToList();

                // 如果没有蜕变关系或应用程序，返回空结果  
                if (!metamorphicRelations.Any() || !applications.Any())
                {
                    return new ObservableCollection<MetamorphicRelations_QueryResultData>();
                }

                // 使用字典来快速查找应用程序  
                var applicationDictionary = applications.ToDictionary(app => app.Name);

                // 中间类集合  
                var metamorphicRelationApplications = new List<MetamorphicRelationApplication>();
                foreach (var relation in metamorphicRelations)
                {
                    if (string.IsNullOrEmpty(relation.ApplicationName))
                    {
                        // 蜕变关系至少对应着一个应用程序  
                        return new ObservableCollection<MetamorphicRelations_QueryResultData>();
                    }

                    var applicationNames = relation.ApplicationName.Split(':');
                    foreach (var appName in applicationNames)
                    {
                        metamorphicRelationApplications.Add(new MetamorphicRelationApplication { IdMR = relation.IdMR, ApplicationName = appName });
                    }
                }

                // 查询结果集合  
                var result = new List<MetamorphicRelations_QueryResultData>();

                // 使用LINQ查询  
                var MRs_Query = from relation in metamorphicRelations
                                join relationApp in metamorphicRelationApplications on relation.IdMR equals relationApp.IdMR
                                join app in applicationDictionary on relationApp.ApplicationName equals app.Key
                                select new
                                {
                                    MetamorphicRelation = relation,
                                    Application = app.Value
                                };

                // 将每一条查询结果保存到查询结果集合  
                foreach (var mr_query in MRs_Query)
                {
                    var mr = mr_query.MetamorphicRelation;
                    var app = mr_query.Application;

                    var mrQuery = new MetamorphicRelations_QueryResultData
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
                        CodeName = app.CodeName,
                        DomainName = string.IsNullOrEmpty(app.DomainName) ? string.Empty : app.DomainName,
                        InputPatternImagepath = new DatatoImage().ConvertByteArrayToImage(mr.InputPatternImageData),
                        OutputPatternImagepath = new DatatoImage().ConvertByteArrayToImage(mr.OutputPatternImageData)
                    };

                    result.Add(mrQuery);
                }

                return new ObservableCollection<MetamorphicRelations_QueryResultData>(result);
            }
        }


        ////3.根据复合条件查询  查询条件包括了“OrderOfMR、 RepresentationType、DimensionOfInputPattern、DimensionOfOutputPattern、Application的Name、Domain的Name”
        //public ObservableCollection<MetamorphicRelations_QueryResultData> Get(MetamorphicRelation metamorphicRelation, string domainName)
        //{
        //    using (var db = new LiteDatabase(_conn))
        //    {
        //        if (metamorphicRelation != null)
        //        {
        //            //metamorphicRelation.Application.Domains.Add(new Domain(){Name = Domain的Name})   domain(保存了Domain的Name)
        //            //使用linq语句查询 两表联合
        //            MetamorphicRelations = db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key);
        //            Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);
        //            Domains = db.GetCollection<Domain>(_dbConfig.Domains_Collection_Key);

        //            var metamorphicRelations = new ObservableCollection<MetamorphicRelation>(MetamorphicRelations.FindAll());
        //            var applictaions = new ObservableCollection<Application>(Applications.FindAll());
        //            //var domains = new ObservableCollection<Domain>(Domains.FindAll());

        //            //中间类集合
        //            var metamorphicRelationApplications = new ObservableCollection<MetamorphicRelationApplication>();
        //            for (int i = 0; i < metamorphicRelations.Count; i++)
        //            {
        //                string str = metamorphicRelations[i].ApplicationName;
        //                if (str == string.Empty)
        //                {
        //                    return null;
        //                }
        //                //-------------------------------------
        //                //// 后续改成异常处理
        //                //try
        //                //{
        //                //    if (string.IsNullOrEmpty(str))
        //                //    {
        //                //        throw new ArgumentNullException("应用程序名称不能为空", "一条蜕变关系至少对应一个程序");
        //                //    }
        //                //}
        //                //catch (ArgumentNullException ex)
        //                //{
        //                //    Console.WriteLine("Exception caught: " + ex.Message);
        //                //    // Handle the exception here
        //                //}
        //                //-------------------------------------
        //                string[] strarray = str.Split(':');
        //                for (int j = 0; j < strarray.Length; j++)
        //                {
        //                    var idmr = metamorphicRelations[i].IdMR;
        //                    var mrapplication = new MetamorphicRelationApplication() { IdMR = idmr, ApplicationName = strarray[j] };
        //                    metamorphicRelationApplications.Add(mrapplication);
        //                }
        //            }

        //            //metamorphicRelation保存了OrderOfMR、 RepresentationType、DimensionOfInputPattern、DimensionOfOutputPattern
        //            var orderofMR = metamorphicRelation.OrderOfMR; //可为空字符串
        //            var representationType = metamorphicRelation.RepresentationType; //可能为-1
        //            var dimensionOfInputPattern = metamorphicRelation.DimensionOfInputPattern; //可为空字符串
        //            var dimensionOfOutputPattern = metamorphicRelation.DimensionOfOutputPattern; //可为空字符串
        //            var applicationName = metamorphicRelation.ApplicationName; //可为空字符串
        //            var domainName1 = domainName; //可能为空字符串

        //            //修改后
        //            var MRs_Query1 = (
        //                     from MetamorphicRelation in metamorphicRelations
        //                     join MetamorphicRelationApplication in metamorphicRelationApplications on MetamorphicRelation.IdMR equals MetamorphicRelationApplication.IdMR
        //                     join Application in applictaions on MetamorphicRelationApplication.ApplicationName equals Application.Name
        //                     select new
        //                     {
        //                         metamorphicRelation = MetamorphicRelation,
        //                         application = Application,
        //                     }
        //            );

        //            //两表联合查询 MetamorphicRealtions 与 Applications表联合查询
        //            var MRs_Query_List = MRs_Query1.ToList();
        //            //var MRs_Query_List = MRs_Query1.ToList();

        //            for (int i = 0; i < MRs_Query_List.Count; i++)
        //            {
        //                //应用程序对应的应用领域可能为null
        //                var domainname = MRs_Query_List[i].application.DomainName;
        //                if (domainname == null)
        //                {
        //                    //用空字符串占位
        //                    MRs_Query_List[i].application.DomainName = string.Empty;
        //                }
        //            }

        //            //使用Linq语句进行多条件组合查询
        //            var MRs_Query = MRs_Query_List.Where(x =>
        //            ((x.metamorphicRelation.OrderOfMR.Contains(metamorphicRelation.OrderOfMR)) || x.metamorphicRelation.OrderOfMR == string.Empty)
        //            && (x.metamorphicRelation.RepresentationType.ToString().Contains(metamorphicRelation.RepresentationType.ToString()) || (int)representationType == -1)
        //            && ((x.metamorphicRelation.DimensionOfInputPattern.Contains(metamorphicRelation.DimensionOfInputPattern)) || x.metamorphicRelation.DimensionOfInputPattern == string.Empty)
        //            && ((x.metamorphicRelation.DimensionOfOutputPattern.Contains(metamorphicRelation.DimensionOfOutputPattern)) || x.metamorphicRelation.DimensionOfOutputPattern == string.Empty)
        //            && ((x.application.Name.Contains(applicationName)) || x.application.Name == string.Empty)
        //            && ((x.application.DomainName.Contains(domainName1)) || x.application.DomainName == string.Empty)
        //            );

        //            // 查询结果 result.Count()>0 为联查成功
        //            var result = new ObservableCollection<MetamorphicRelations_QueryResultData>();
        //            foreach (var mr_query in MRs_Query)
        //            {
        //                //两表联合查出的MR
        //                var mr = mr_query.metamorphicRelation;
        //                var app = mr_query.application;

        //                var mr1 = new MetamorphicRelations_QueryResultData()
        //                {
        //                    IdMR = mr.IdMR,
        //                    Description = mr.Description,
        //                    Context = mr.Context,
        //                    Constraint = mr.Constraint,
        //                    OrderOfMR = mr.OrderOfMR,
        //                    RepresentationType = mr.RepresentationType,
        //                    InputPattern = mr.InputPattern,
        //                    OutputPattern = mr.OutputPattern,
        //                    InputPatternImageData = mr.InputPatternImageData,
        //                    OutputPatternImageData = mr.OutputPatternImageData,//图片数据
        //                    DimensionOfInputPattern = mr.DimensionOfInputPattern,
        //                    DimensionOfOutputPattern = mr.DimensionOfOutputPattern,
        //                    ApplicationName = app.Name,
        //                    CodeName = mr_query.application.CodeName
        //                };

        //                mr1.DomainName = mr_query.application.DomainName;

        //                //绑定Latex渲染图片 
        //                var dt = new DatatoImage();
        //                mr1.InputPatternImagepath = dt.ConvertByteArrayToImage(mr.InputPatternImageData);
        //                mr1.OutputPatternImagepath = dt.ConvertByteArrayToImage(mr.OutputPatternImageData);
        //                result.Add(mr1);
        //            }
        //            return result;
        //        }
        //        else
        //        {
        //            return null;
        //        }

        //    }
        //}

        //3.根据复合条件查询  查询条件包括了“OrderOfMR、 RepresentationType、DimensionOfInputPattern、DimensionOfOutputPattern、Application的Name、Domain的Name”
        public ObservableCollection<MetamorphicRelations_QueryResultData> Get(MetamorphicRelation metamorphicRelation, string domainName)
        {
            if (metamorphicRelation == null)
            {
                return null;
            }

            using (var db = new LiteDatabase(_conn))
            {
                MetamorphicRelations = db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key);
                Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);

                // 直接加载所有数据  
                var metamorphicRelations = MetamorphicRelations.FindAll().ToList();
                var applications = Applications.FindAll().ToList();

                // 中间类集合  
                var metamorphicRelationApplications = new List<MetamorphicRelationApplication>();

                foreach (var relation in metamorphicRelations)
                {
                    if (string.IsNullOrEmpty(relation.ApplicationName))
                    {
                        return new ObservableCollection<MetamorphicRelations_QueryResultData>();
                    }

                    var applicationNames = relation.ApplicationName.Split(':');
                    foreach (var appName in applicationNames)
                    {
                        metamorphicRelationApplications.Add(new MetamorphicRelationApplication { IdMR = relation.IdMR, ApplicationName = appName });
                    }
                }
                foreach (var application in applications) 
                {
                    if (application.DomainName == null) 
                    {
                        //占位
                        application.DomainName = string.Empty;
                    }
                }

                // 使用LINQ查询  
                var MRs_Query = from relation in metamorphicRelations
                                join relationApp in metamorphicRelationApplications on relation.IdMR equals relationApp.IdMR
                                join app in applications on relationApp.ApplicationName equals app.Name
                                where (relation.OrderOfMR.Contains(metamorphicRelation.OrderOfMR) || string.IsNullOrEmpty(relation.OrderOfMR)) &&
                                      (relation.RepresentationType == metamorphicRelation.RepresentationType || (int)metamorphicRelation.RepresentationType == -1) &&
                                      (relation.DimensionOfInputPattern.Contains(metamorphicRelation.DimensionOfInputPattern) || string.IsNullOrEmpty(relation.DimensionOfInputPattern)) &&
                                      (relation.DimensionOfOutputPattern.Contains(metamorphicRelation.DimensionOfOutputPattern) || string.IsNullOrEmpty(relation.DimensionOfOutputPattern)) &&
                                      (app.Name.Contains(metamorphicRelation.ApplicationName) || string.IsNullOrEmpty(metamorphicRelation.ApplicationName)) &&
                                      (app.DomainName.Contains(domainName) || string.IsNullOrEmpty(domainName) || (app.DomainName == null))
                                select new MetamorphicRelations_QueryResultData
                                {
                                    IdMR = relation.IdMR,
                                    Description = relation.Description,
                                    Context = relation.Context,
                                    Constraint = relation.Constraint,
                                    OrderOfMR = relation.OrderOfMR,
                                    RepresentationType = relation.RepresentationType,
                                    InputPattern = relation.InputPattern,
                                    OutputPattern = relation.OutputPattern,
                                    InputPatternImageData = relation.InputPatternImageData,
                                    OutputPatternImageData = relation.OutputPatternImageData,
                                    DimensionOfInputPattern = relation.DimensionOfInputPattern,
                                    DimensionOfOutputPattern = relation.DimensionOfOutputPattern,
                                    ApplicationName = app.Name,
                                    CodeName = app.CodeName,
                                    DomainName = app.DomainName ?? string.Empty,
                                    InputPatternImagepath = new DatatoImage().ConvertByteArrayToImage(relation.InputPatternImageData),
                                    OutputPatternImagepath = new DatatoImage().ConvertByteArrayToImage(relation.OutputPatternImageData)
                                };

                return new ObservableCollection<MetamorphicRelations_QueryResultData>(MRs_Query.ToList());
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

        //public bool Add(MetamorphicRelation metamorphicRelation)
        //{
        //    //标识： MetamorphicRelation.Application没有的话 ApplicationName为String.Empty;
        //    using (var db = new LiteDatabase(_conn))
        //    {
        //        //if (db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key).FindById(metamorphicRelation.IdMR) == null)
        //        if (metamorphicRelation.IdMR == 0)
        //        {
        //            MetamorphicRelations = db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key);
        //            Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);

        //            //metamorphicRelations.ApplicationName以:为分割符 
        //            //保存关联的应用程序的名称
        //            var str = metamorphicRelation.ApplicationName;
        //            //对应用程序名称进行分割 分割符为:
        //            var strarray = str.Split(":").ToList();
        //            var newstr = "";
        //            for (int i = 0; i < strarray.Count; i++)
        //            {
        //                var name = strarray[i];
        //                var application = Applications.FindOne(x => x.Name == name);
        //                if (application != null)
        //                {
        //                    if (i == 0)
        //                    {
        //                        newstr += strarray[i];
        //                    }
        //                    else
        //                    {
        //                        newstr += ":";
        //                        newstr += strarray[i];
        //                    }
        //                }
        //                else
        //                {
        //                    // 应用程序不存在，需先将其加入表中
        //                    return false;
        //                }
        //            }
        //            metamorphicRelation.ApplicationName = newstr;
        //            var _id = MetamorphicRelations.Insert(metamorphicRelation);
        //            return _id > 0;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        public bool Add(MetamorphicRelation metamorphicRelation)
        {
            // 标识： MetamorphicRelation.Application没有的话 ApplicationName为String.Empty;  
            using (var db = new LiteDatabase(_conn))
            {
                // 如果 IdMR 已存在，直接返回 false  
                if (metamorphicRelation.IdMR != 0)
                {
                    return false;
                }

                MetamorphicRelations = db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key);
                Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);

                // 将 Applications 转换为字典，以便快速查找  通过索引为Name 通过Name可以找到对应的应用程序
                var applicationDictionary = Applications.FindAll().ToDictionary(app => app.Name);

                // 保存关联的应用程序的名称  
                var str = metamorphicRelation.ApplicationName;
                // 对应用程序名称进行分割，分割符为 ":"  
                var strarray = str.Split(":");
                var newstr = new StringBuilder();

                for (int i = 0; i < strarray.Length; i++)
                {
                    var name = strarray[i];
                    // 检查应用程序是否存在  
                    if (applicationDictionary.ContainsKey(name))
                    {
                        if (i > 0)
                        {
                            newstr.Append(":");
                        }
                        newstr.Append(name);
                    }
                    else
                    {
                        // 应用程序不存在，需先将其加入表中  
                        return false;
                    }
                }

                // 更新 ApplicationName  
                metamorphicRelation.ApplicationName = newstr.ToString();
                var _id = MetamorphicRelations.Insert(metamorphicRelation);
                return _id > 0;
            }
        }


        //public bool Modify(MetamorphicRelation metamorphicRelation)
        //{
        //    using (var db = new LiteDatabase(_conn))
        //    {
        //        if (db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key).FindById(metamorphicRelation.IdMR) != null)
        //        {
        //            var result = false;
        //            Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);
        //            MetamorphicRelations = db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key);

        //            //metamorphicRelations.ApplicationName以:为分割符 
        //            //保存关联的应用程序的名称
        //            var str = metamorphicRelation.ApplicationName;
        //            //对应用程序名称进行分割 分割符为:
        //            var strarray = str.Split(":").ToList();
        //            var newstr = "";
        //            for (int i = 0; i < strarray.Count; i++)
        //            {
        //                var name = strarray[i];
        //                var application = Applications.FindOne(x => x.Name == name);
        //                if (application != null)
        //                {
        //                    if (i == 0)
        //                    {
        //                        newstr += strarray[i];
        //                    }
        //                    else
        //                    {
        //                        newstr += ":";
        //                        newstr += strarray[i];
        //                    }
        //                }
        //                else
        //                {
        //                    // 应用程序不存在，需先将其加入表中
        //                    return false;
        //                }
        //            }
        //            metamorphicRelation.ApplicationName = newstr;
        //            //直接改就行了
        //            result = MetamorphicRelations.Update(metamorphicRelation);
        //            return result;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        public bool Modify(MetamorphicRelation metamorphicRelation)
        {
            using (var db = new LiteDatabase(_conn))
            {
                // 检查 IdMR 是否存在  
                var existingRelation = db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key).FindById(metamorphicRelation.IdMR);
                if (existingRelation == null)
                {
                    return false; // IdMR 不存在，返回 false  
                }

                Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);
                MetamorphicRelations = db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key);

                // 将 Applications 转换为字典，以便快速查找  
                var applicationDictionary = Applications.FindAll().ToDictionary(app => app.Name);

                // 保存关联的应用程序的名称  
                var str = metamorphicRelation.ApplicationName;
                if (str == string.Empty || str == null) 
                {
                    return false;
                }
                // 对应用程序名称进行分割，分割符为 ":"  
                var strarray = str.Split(":");
                var newstr = new StringBuilder();

                for (int i = 0; i < strarray.Length; i++)
                {
                    var name = strarray[i];
                    // 检查应用程序是否存在  
                    if (applicationDictionary.ContainsKey(name))
                    {
                        if (i > 0)
                        {
                            newstr.Append(":");
                        }
                        newstr.Append(name);
                    }
                    else
                    {
                        // 应用程序不存在，需先将其加入表中  
                        return false;
                    }
                }

                // 更新 ApplicationName  
                metamorphicRelation.ApplicationName = newstr.ToString();
                // 直接更新  
                return MetamorphicRelations.Update(metamorphicRelation);
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
        //public int Add_Return_ID(MetamorphicRelation metamorphicRelation)
        //{
        //    using (var db = new LiteDatabase(_conn))
        //    {
        //        if (db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key).FindById(metamorphicRelation.IdMR) == null)
        //        {

        //            var result = false;
        //            MetamorphicRelations = db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key);

        //            //metamorphicRelations.ApplicationName以:为分割符 
        //            //保存关联的应用程序的名称
        //            var str = metamorphicRelation.ApplicationName;
        //            //对应用程序名称进行分割 分割符为:
        //            var strarray = str.Split(":").ToList();
        //            var newstr = "";
        //            for (int i = 0; i < strarray.Count; i++)
        //            {
        //                var name = strarray[i];
        //                var application = Applications.FindOne(x => x.Name == name);
        //                if (application != null)
        //                {
        //                    if (i == 0)
        //                    {
        //                        newstr += strarray[i];
        //                    }
        //                    else
        //                    {
        //                        newstr += ":";
        //                        newstr += strarray[i];
        //                    }
        //                }
        //                else
        //                {
        //                    // 应用程序不存在，需先将其加入表中
        //                    return 0;
        //                }
        //            }
        //            metamorphicRelation.ApplicationName = newstr;
        //            var _id = MetamorphicRelations.Insert(metamorphicRelation);
        //            var id = MetamorphicRelations.FindById(_id).IdMR;
        //            return id;
        //        }
        //        else
        //        {
        //            //返回0 表示添加失败！
        //            return 0;
        //        }
        //    }
        //}
        public int Add_Return_ID(MetamorphicRelation metamorphicRelation)
        {
            using (var db = new LiteDatabase(_conn))
            {
                // 检查 IdMR 是否存在  
                if (db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key).FindById(metamorphicRelation.IdMR) != null)
                {
                    return 0; // 返回 0 表示添加失败  
                }

                MetamorphicRelations = db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key);
                Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);

                // 将 Applications 转换为字典，以便快速查找  
                var applicationDictionary = Applications.FindAll().ToDictionary(app => app.Name);

                // 保存关联的应用程序的名称  
                var str = metamorphicRelation.ApplicationName;
                if (str == string.Empty || str == null) 
                {
                    return 0;
                }
                // 对应用程序名称进行分割，分割符为 ":"  
                var strarray = str.Split(":");
                var newstr = new StringBuilder();

                for (int i = 0; i < strarray.Length; i++)
                {
                    var name = strarray[i];
                    // 检查应用程序是否存在  
                    if (applicationDictionary.ContainsKey(name))
                    {
                        if (i > 0)
                        {
                            newstr.Append(":");
                        }
                        newstr.Append(name);
                    }
                    else
                    {
                        // 应用程序不存在，需先将其加入表中  
                        return 0;
                    }
                }

                // 更新 ApplicationName  
                metamorphicRelation.ApplicationName = newstr.ToString();
                // 插入并返回插入的 ID  
                var _id = MetamorphicRelations.Insert(metamorphicRelation);
                return _id > 0 ? _id : 0; // 如果插入成功，返回 ID，否则返回 0  
            }
        }
    }
}