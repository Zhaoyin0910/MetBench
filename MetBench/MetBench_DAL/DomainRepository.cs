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
            _dbConfig = DbConfig.GetInstance();
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
        /// 
        /// </summary>
        /// <returns> 数据表中全部的应用程序的集合</returns>
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
        /// 
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
                if (Domains.FindById(domain.IdDomain) == null)
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
        //修改会出现 减少的情况
        public bool Modify(Domain domain)
        {
            using (var db = new LiteDatabase(_conn))
            {
                Domains = db.GetCollection<Domain>(_dbConfig.Domains_Collection_Key);
                if (Domains.FindById(domain.IdDomain) != null)
                {
                    var result = Domains.Update(domain);
                    return result;
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
        public bool Remove(Domain domain)
        {
            using (var db = new LiteDatabase(_conn))
            {
                Domains = db.GetCollection<Domain>(_dbConfig.Domains_Collection_Key);
                Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);
                if (Domains.FindById(domain.IdDomain) != null)
                {

                    //将关联了的应用程序也修改
                    //获取全部的应用程序
                    var allapplication = Applications.FindAll();
                    //去null 将DomainName为null的去掉
                    var temp = allapplication.Where(x => x.DomainName != null).ToList();
                    //可能包含的修改前的DomainName 的应用程序
                    var applications = temp.Where(x => x.DomainName.Contains(domain.Name)).ToList();
                    for (int i = 0; i < applications.Count; i++)
                    {
                        var application = applications[i];
                        //关联的应用程序名
                        var domainname = application.DomainName;
                        var strarray = domainname.Split(':').ToList();
                        string nowdomainanme = "";
                        for (int j = 0; j < strarray.Count; j++)
                        {
                            var name = strarray[j];
                            if (name == domain.Name)
                            {
                                strarray.Remove(strarray[j]);
                            }
                        }
                        for (int j = 0; j < strarray.Count; j++)
                        {
                            if (j == 0)
                            {
                                nowdomainanme += strarray[j];
                            }
                            else
                            {
                                nowdomainanme += ":";
                                nowdomainanme += strarray[j];
                            }
                        }
                        application.DomainName = nowdomainanme;
                        Applications.Update(application);
                    }

                    var result = Domains.Delete(domain.IdDomain);
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
