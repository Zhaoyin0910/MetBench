using LiteDB;
using MetBench_Domain;
using MetBench_IDAL;
using System.Collections.ObjectModel;


namespace MetBench_DAL
{
    public class DomainRepository : IDomainRepository
    {
        //数据库连接字符串 
        private string _conn;
        private DbConfig _dbConfig;

        private ILiteCollection<Application> Applications;
        private ILiteCollection<Domain> Domains;
        public DomainRepository()
        {
            //获得DbConfig对象
            _dbConfig = DbConfig.Instance;
            _conn = _dbConfig._conn;
        }

        /// <summary>
        /// 根据应用领域的名称获取Id
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public int Get(string Name)
        {
            using (var db = new LiteDatabase(_conn))
            {
                var id = 0;
                Domains = db.GetCollection<Domain>(_dbConfig.Domains_Collection_Key);
                var domain = Domains.FindOne(t => t.Name == Name);
                if (domain != null)
                {
                    id = domain.IdDomain;
                }
                return id;
            }
        }

        /// <summary>
        /// 获取全部的应用领域实体
        /// </summary>
        /// <returns> 数据表中全部的应用领域的集合</returns>
        public ObservableCollection<Domain> GetAll()
        {
            using (var db = new LiteDatabase(_conn))
            {
                Domains = db.GetCollection<Domain>(_dbConfig.Domains_Collection_Key);
                var result = new ObservableCollection<Domain>(Domains.FindAll());
                return result;

            }
        }

        /// <summary>
        /// 根据Id获取应用领域实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns>对应Id的应用领域</returns>
        public Domain Get(int id)
        {
            using (var db = new LiteDatabase(_conn))
            {
                Domains = db.GetCollection<Domain>(_dbConfig.Domains_Collection_Key);
                var domain = Domains.FindById(id);
                return domain;
            }
        }

        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ObservableCollection<Domain> Get(Domain domain)
        {
            using (var db = new LiteDatabase(_conn))
            {
                Domains = db.GetCollection<Domain>(_dbConfig.Domains_Collection_Key);
                var result = new ObservableCollection<Domain>(
                    Domains.FindAll().Where(t => (t.Name.Contains(domain.Name)) && (t.Description.Contains(domain.Description)))
                    );
                return result;
            }
        }

        /// <summary>
        /// 添加应用领域
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public bool Add(Domain domain)
        {
            using (var db = new LiteDatabase(_conn))
            {
                Domains = db.GetCollection<Domain>(_dbConfig.Domains_Collection_Key);
                if (domain.IdDomain==0)
                {
                    var id = Domains.Insert(domain);
                    return id > 0;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 修改应用领域
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        //public bool Modify(Domain domain)
        //{
        //    using (var db = new LiteDatabase(_conn))
        //    {
        //        Domains = db.GetCollection<Domain>(_dbConfig.Domains_Collection_Key);
        //        if (Domains.FindById(domain.IdDomain) != null)
        //        {
        //            var beforeDomain = Domains.FindById(domain.IdDomain);
        //            //避免对应用领域的名称进行修改
        //            var beforedomainname = beforeDomain.Name;

        //            //外键Name进行修改，对应的Application关联的DomainName也需要进行修改
        //            if (beforedomainname != domain.Name)
        //            {
        //                Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);
        //                //LiteDB将空字符串赋值为null

        //                //将关联了的应用程序也修改
        //                //获取全部的应用程序
        //                var allapplication = Applications.FindAll();
        //                //去null 将DomainName为null的去掉
        //                var temp = allapplication.Where(x => x.DomainName != null).ToList();
        //                //可能包含的修改前的DomainName 的应用程序
        //                var applications = temp.Where(x => x.DomainName.Contains(domain.Name)).ToList();

        //                //var AllApplications = Applications.FindAll().ToList();
        //                //var applications = new List<Application>();
        //                //for (int i = 0; i < AllApplications.Count(); i++) 
        //                //{
        //                //    var app = AllApplications[i];
        //                //    if (app.DomainName == null)
        //                //    {
        //                //        continue;
        //                //    }
        //                //    else 
        //                //    {
        //                //        if (app.DomainName.Contains(beforedomainname)) 
        //                //        {
        //                //            applications.Add(app);
        //                //        }
        //                //    }
        //                //}
        //                //var applications = new List<Application>(Applications.FindAll().Where(x => x.DomainName.Contains(beforedomainname)));

        //                if (applications != null)
        //                {
        //                    var length = applications.Count;
        //                    for (int i = 0; i < length; i++)
        //                    {
        //                        //根据Id获取应用程序
        //                        var idapplication = applications[i].IdApplication;
        //                        var application = Applications.FindById(idapplication);
        //                        if (application == null)
        //                        {
        //                            continue;
        //                        }
        //                        //应用程序对应的应用领域名称
        //                        string strdomainname = application.DomainName;
        //                        //关联的应用领域名称
        //                        List<string> strlist = strdomainname.Split(":").ToList();

        //                        //保存修改后的关联应用领域名称
        //                        string newstrdomainname = "";

        //                        //关联了多个应用领域
        //                        if (strlist.Count > 1)
        //                        {
        //                            // 考虑后期时间效率，可使用字符串修改
        //                            for (int j = 0; j < strlist.Count; j++)
        //                            {
        //                                if (strlist[j] == beforedomainname)
        //                                {
        //                                    //修改为更新后的Application的Name
        //                                    strlist[j] = domain.Name;
        //                                }
        //                            }
        //                            for (int j = 0; j < strlist.Count; j++)
        //                            {
        //                                if (j == 0)
        //                                {
        //                                    newstrdomainname += strlist[j];
        //                                }
        //                                else
        //                                {
        //                                    newstrdomainname += ":";
        //                                    newstrdomainname += strlist[j];
        //                                }

        //                            }
        //                            application.DomainName = newstrdomainname;
        //                            Applications.Update(application);
        //                        }
        //                        else
        //                        {
        //                            //只关联了一个Domain 直接进行更新DomainName 
        //                            application.DomainName = domain.Name;
        //                            Applications.Update(application);
        //                        }
        //                    }
        //                }
        //            }
        //            var result = Domains.Update(domain);
        //            return result;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        public bool Modify(Domain domain)
        {
            using (var db = new LiteDatabase(_conn))
            {
                Domains = db.GetCollection<Domain>(_dbConfig.Domains_Collection_Key);
                var existingDomain = Domains.FindById(domain.IdDomain);
                if (existingDomain != null)
                {
                    var beforeDomainName = existingDomain.Name;

                    // 只有在名称变化时才进行修改  
                    if (beforeDomainName != domain.Name)
                    {
                        Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);

                        // 获取所有应用程序并存储在字典中以提高查找效率  
                        var allApplications = Applications.FindAll().ToDictionary(app => app.IdApplication);

                        // 找到所有需要更新的应用程序  
                        var applicationsToUpdate = allApplications.Values
                            .Where(app => app.DomainName != null && app.DomainName.Contains(beforeDomainName))
                            .ToList();

                        foreach (var application in applicationsToUpdate)
                        {
                            var strDomainName = application.DomainName;
                            var strList = strDomainName.Split(":").ToList();

                            // 更新关联的应用领域名称  
                            for (int j = 0; j < strList.Count(); j++)
                            {
                                if (strList[j] == beforeDomainName)
                                {
                                    strList[j] = domain.Name;
                                }
                            }

                            // 使用 String.Join 来构建新的 DomainName  
                            application.DomainName = string.Join(":", strList);
                            Applications.Update(application);
                        }
                    }

                    // 更新域  
                    return Domains.Update(domain);
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 删除应用领域
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        //public bool Remove(Domain domain)
        //{
        //    using (var db = new LiteDatabase(_conn))
        //    {
        //        Domains = db.GetCollection<Domain>(_dbConfig.Domains_Collection_Key);
        //        Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);
        //        if (Domains.FindById(domain.IdDomain) != null)
        //        {
        //            //将关联了的应用程序也修改
        //            //获取全部的应用程序
        //            var allapplication = Applications.FindAll();
        //            //去null 将DomainName为null的去掉
        //            var temp = allapplication.Where(x => x.DomainName != null).ToList();
        //            //可能包含的修改前的DomainName 的应用程序
        //            var applications = temp.Where(x => x.DomainName.Contains(domain.Name)).ToList();
        //            for (int i = 0; i < applications.Count; i++)
        //            {
        //                var application = applications[i];
        //                //关联的应用程序名
        //                var domainname = application.DomainName;
        //                var strarray = domainname.Split(':').ToList();
        //                string nowdomainanme = "";
        //                for (int j = 0; j < strarray.Count; j++)
        //                {
        //                    var name = strarray[j];
        //                    if (name == domain.Name)
        //                    {
        //                        strarray.Remove(strarray[j]);
        //                    }
        //                }
        //                for (int j = 0; j < strarray.Count; j++)
        //                {
        //                    if (j == 0)
        //                    {
        //                        nowdomainanme += strarray[j];
        //                    }
        //                    else
        //                    {
        //                        nowdomainanme += ":";
        //                        nowdomainanme += strarray[j];
        //                    }
        //                }
        //                application.DomainName = nowdomainanme;
        //                Applications.Update(application);
        //            }
        //            var result = Domains.Delete(domain.IdDomain);
        //            return result;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}

        public bool Remove(Domain domain)
        {
            using (var db = new LiteDatabase(_conn))
            {
                Domains = db.GetCollection<Domain>(_dbConfig.Domains_Collection_Key);
                Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);

                // 检查应用领域是否存在  
                var existingDomain = Domains.FindById(domain.IdDomain);
                if (existingDomain != null)
                {
                    // 获取所有应用程序并存储在内存中  
                    var allApplications = Applications.FindAll().ToList();

                    // 避免修改了应用邻域名称
                    var OldDomainName = Domains.FindById(domain.IdDomain).Name;

                    // 找到所有需要更新的应用程序
                    var applicationsToUpdate = allApplications
                        .Where(app => app.DomainName != null && app.DomainName.Contains(OldDomainName))
                        .ToList();

                    foreach (var application in applicationsToUpdate)
                    {
                        // 处理关联的应用程序名  
                        var domainNames = application.DomainName.Split(':').ToList();

                        // 移除要删除的域名  
                        domainNames.RemoveAll(name => name == domain.Name);

                        // 更新 DomainName  
                        application.DomainName = string.Join(":", domainNames);
                        Applications.Update(application);
                    }

                    // 删除域  
                    return Domains.Delete(domain.IdDomain);
                }
                else
                {
                    return false;
                }
            }
        }

        public ObservableCollection<Domain> GetByName(string Name)
        {
            using (var db = new LiteDatabase(_conn))
            {
                Domains = db.GetCollection<Domain>(_dbConfig.Domains_Collection_Key);
                var domains = Domains.FindAll();
                var result = new ObservableCollection<Domain>( domains.Where(x => x.Name.Contains(Name)));
                return result;
            }
        }
    }
}
