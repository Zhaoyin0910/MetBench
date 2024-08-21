using MetBench_Domain;
using MetBench_IDAL;
using System.Collections.ObjectModel;


namespace MetBench_BLL
{
    public class DomainSerive
    {

        private IDomainRepository Domain_Repository;

        //依赖注入实体
        public DomainSerive(IDomainRepository domain_Repository)
        {
            Domain_Repository = domain_Repository;
        }

        /// <summary>
        /// 根据应用领域名称获取应用领域
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetDomainId(string name)
        {
            var id = Domain_Repository.Get(name);
            return id;
        }

        /// <summary>
        /// 根据应用领域Id获取应用领域
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain GetDomain(int id)
        {
            var domain = Domain_Repository.Get(id);
            return domain;
        }

        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public bool AddService(Domain domain)
        {
            var result = Domain_Repository.Add(domain);
            return result;

            //var isexist = Domain_Repository.Get(domain.Name);
            //if (isexist == 0)
            //{
            //    var result = Domain_Repository.Add(domain);
            //    return result;
            //}
            //else
            //{
            //    return false;
            //}
        }

        /// <summary>
        /// 删除服务
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public bool DeleteService(Domain domain)
        {
            var result = Domain_Repository.Remove(domain);
            return result;
        }

        /// <summary>
        /// 获取全部的应用领域
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Domain> showAllResult()
        {
            var result = Domain_Repository.GetAll();
            return result;
        }
        //public bool UpdateService(Domain domain)
        //{
        //    var isexist = Domain_Repository.Get(domain.IdDomain);
        //    if (isexist != null)
        //    {
        //        //修改前的domainName
        //        var beforedomain = isexist;
        //        var beforename = isexist.Name;
        //        //将关联了的应用程序也修改
        //        //获取全部的应用程序
        //        var allapplication = Application_repository.GetAll();
        //        //去null 将DomainName为null的去掉
        //        var temp = allapplication.Where(x => x.DomainName != null).ToList();
        //        //可能包含的修改前的DomainName 的应用程序
        //        var applications = temp.Where(x => x.DomainName.Contains(beforename)).ToList();
        //        if (applications != null)
        //        {
        //            for (int i = 0; i < applications.Count; i++)
        //            {
        //                var application = applications[i];
        //                var domainname = application.DomainName;
        //                string nowdomainname = "";
        //                if (domainname != null)
        //                {
        //                    //转换为List
        //                    var strarray = domainname.Split(':').ToList();
        //                    for (int j = 0; j < strarray.Count; j++)
        //                    {
        //                        var name = strarray[j];
        //                        if (name == beforename)
        //                        {
        //                            //strarray.Remove(strarray[j]);
        //                            strarray[j] = domain.Name;
        //                        }
        //                    }

        //                    for (int j = 0; j < strarray.Count; j++)
        //                    {
        //                        if (j == 0)
        //                        {
        //                            nowdomainname += strarray[j];
        //                        }
        //                        else
        //                        {
        //                            nowdomainname += ":";
        //                            nowdomainname += strarray[j];
        //                        }
        //                    }
        //                }
        //                application.DomainName = nowdomainname;
        //                Application_repository.Modify(application);
        //            }
        //        }


        //        var result = Domain_Repository.Modify(domain);
        //        return result;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        /// <summary>
        /// 更新服务
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public bool UpdateService(Domain domain) 
        {
            var result = Domain_Repository.Modify(domain); 
            return result;
        }

        /// <summary>
        /// 获取全部的应用领域实体
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ObservableCollection<Domain> GetAllDomians() 
        {
            var result = Domain_Repository.GetAll();
            return result;
        }
    }
}
