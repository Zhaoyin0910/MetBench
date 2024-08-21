using MetBench_Domain;
using MetBench_IDAL;
using System.Collections.ObjectModel;

namespace MetBench_BLL
{
    //应用程序管理服务类
    public class ApplicationSerive
    {
        //数据仓库（实体集合的仓库）
        private IApplicationRepository Application_repository;


        //依赖注入实体
        public ApplicationSerive( IApplicationRepository applicationRepository)
        {
            Application_repository = applicationRepository;
        }

        /// <summary>
        ///获取应用程序的Id 
        /// </summary>
        /// <param name="name">应用程序的名称</param>
        /// <returns>应用程序的Id</returns>
        public int GetApplicationId(string name)
        {
            var id = Application_repository.Get(name);
            return id;
        }

        /// <summary>
        /// 根据Id获取应用程序对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Application GetApplication(int id)
        {
            var application = Application_repository.Get(id);
            return application;
        }

        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="application"></param>
        /// <param name="metamorphicRelationlist"></param>
        /// <param name="domainlist">该集合的每个application关联的DomainName</param>
        /// <returns></returns>
        //public bool AddService(Application application, List<MetamorphicRelation> metamorphicRelationlist, List<Domain> domainlist)
        //{
        //    var isexist = Application_repository.Get(application.Name);
        //    if (isexist == 0)
        //    {
        //        //保存MetamorphicRelation的ID
        //        var str = "";
        //        for (int i = 0; i < metamorphicRelationlist.Count; i++)
        //        {

        //            bool b = MR_repository.Get(metamorphicRelationlist[i].IdMR) == null;
        //            var id = 0;//保存metamorphicRelation的Id
        //            if (b)
        //            {
        //                metamorphicRelationlist[i].ApplicationName = application.Name;
        //                id = MR_repository.Add_Return_ID(metamorphicRelationlist[i]);
        //            }
        //            else
        //            {
        //                //存在数据表中
        //                id = metamorphicRelationlist[i].IdMR;
        //                //var metamorphicRelation = MR_repository.Get(id);
        //                var metamorphicRelation = metamorphicRelationlist[i];

        //                int index = metamorphicRelation.ApplicationName.IndexOf(":");//ApplicationName为非空的
        //                if (metamorphicRelation.ApplicationName != null)
        //                {
        //                    metamorphicRelation.ApplicationName += ":";
        //                    metamorphicRelation.ApplicationName += application.Name;
        //                }
        //                else
        //                {
        //                    metamorphicRelation.ApplicationName += application.Name;
        //                }
        //                //metamorphicRelation.ApplicationName += ":";
        //                //metamorphicRelation.ApplicationName += application.Name;
        //                MR_repository.Modify(metamorphicRelation);
        //            }
        //            if (i == 0)
        //            {
        //                str += id.ToString();
        //            }
        //            else
        //            {
        //                str += ":";
        //                str += id.ToString();
        //            }

        //        }
        //        application.MetamorphicRelationId = str;
        //        //保存Domain的Name
        //        var str1 = "";

        //        for (int i = 0; i < domainlist.Count; i++)
        //        {
        //            var name = domainlist[i].Name;
        //            var iddomain = Domain_Repository.Get(name);

        //            if (iddomain == 0)
        //            {
        //                Domain_Repository.Add(domainlist[i]);
        //            }
        //            else
        //            {
        //                Domain_Repository.Modify(domainlist[i]);
        //            }

        //            if (str1 == string.Empty)
        //            {
        //                str += domainlist[i].Name;
        //            }
        //            else
        //            {
        //                str += ":";
        //                str += domainlist[i].Name;
        //            }
        //            //Domain_Repository.Modify(domainlist[i]);

        //        }
        //        application.DomainName = str1;
        //        var result = Application_repository.Add(application);
        //        return result;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public bool AddService(Application application)
        {
            var result = Application_repository.Add(application);
            return result;
        }

        /// <summary>
        /// 删除服务
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public bool DeleteService(Application application)
        {
            var result = Application_repository.Remove(application);
            return result;
        }

        /// <summary>
        /// 获取全部的应用程序实体
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Application> GetApplications()
        {
            var result = Application_repository.GetAll();
            return result;
        }

        /// <summary>
        /// 三表联查
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Applications_QueryResultData> showMultResult()
        {
            var result = Application_repository.GetAll_MIX();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="application">包含了DomainName 和 MetamorphicRelationId</param>
        /// <param name="metamorphicRelationlist"></param>
        /// <param name="domainlist"></param>
        /// <returns></returns>
        //public bool UpdateService(Application application, List<MetamorphicRelation> metamorphicRelationlist, List<Domain> domainlist)
        //{
        //    //以前关联的mr现在不关联了
        //    //mr有没有多个关联的application 如果有多个关联就不需要删除mr  没有则需要删除
        //    //双重循环来找相同没有取消关联的
        //    //修改后
        //    var isexist = Application_repository.Get(application.IdApplication).IdApplication;
        //    //用来保存mrid
        //    var strmrid = string.Empty;
        //    if (isexist > 0)
        //    {
        //        var beforeapplication = Application_repository.Get(application.IdApplication);
        //        var beforeMetamorphicRelationId = beforeapplication.MetamorphicRelationId;//修改前应用程序关联的蜕变关系
        //        var beforeapplicationname = beforeapplication.Name;

        //        if (beforeMetamorphicRelationId != null )
        //        {
        //            var strarray = beforeMetamorphicRelationId.Split(':');

        //            //保存没有改变关联的下标
        //            List<int> sameindexlist = new List<int>();

        //            for (int i = 0; i < strarray.Length; i++)
        //            {
        //                //判断是否进行关联的修改
        //                bool isequal = false;
        //                for (int j = 0; j < metamorphicRelationlist.Count; j++)
        //                {
        //                    var metamorphicRelationIdstr = metamorphicRelationlist[j].IdMR.ToString();
        //                    if (metamorphicRelationIdstr==strarray[i])
        //                    {
        //                        isequal = true;
        //                        sameindexlist.Add(j);
        //                    }
        //                }
        //                if (!isequal)
        //                {
        //                    //修改后的应用程序不再关联此蜕变关系
        //                    int mrId = int.Parse(strarray[i]);
        //                    var beforemeatmorphicRelation = MR_repository.Get(mrId);//获取蜕变关系

        //                    var applicationame = beforemeatmorphicRelation.ApplicationName;
        //                    var applicationnamestrarray = applicationame.Split(":").ToList();
        //                    var nowapplicationname = "";
        //                    //判断是否有多个关联
        //                    //多个关联进行修改 单个关联删除该蜕变关系
        //                    if (applicationnamestrarray.Count > 1)
        //                    {
        //                        for (int j = 0; j < applicationnamestrarray.Count; j++)
        //                        {
        //                            if (beforeapplicationname == applicationnamestrarray[j])
        //                            {
        //                                applicationnamestrarray.Remove(applicationnamestrarray[j]);
        //                            }
        //                        }
        //                        for (int j = 0; j < applicationnamestrarray.Count; j++)
        //                        {
        //                            if (j == 0)
        //                            {
        //                                nowapplicationname += applicationnamestrarray[j];
        //                            }
        //                            else
        //                            {
        //                                nowapplicationname += ":";
        //                                nowapplicationname += applicationnamestrarray[j];
        //                            }
        //                        }
        //                        beforemeatmorphicRelation.ApplicationName = nowapplicationname;
        //                        MR_repository.Modify(beforemeatmorphicRelation);
        //                    }
        //                    else 
        //                    {
        //                        //不再关联的
        //                        MR_repository.Remove(beforemeatmorphicRelation);
        //                    }
        //                }
        //            }
        //            for (int i = 0; i < metamorphicRelationlist.Count; i++)
        //            {
        //                //保存蜕变关系的Id
        //                int id = 0;
        //                //标记，是否为已经关联的
        //                var sign = false;
        //                for (int j = 0; j < sameindexlist.Count; j++)
        //                {
        //                    if (i == sameindexlist[j])
        //                    {
        //                        sign = true;
        //                    }
        //                }
        //                if (sign)
        //                {
        //                    //已关联了
        //                    var mr = metamorphicRelationlist[i];
        //                    MR_repository.Modify(mr);
        //                    id = mr.IdMR;
        //                }
        //                else
        //                {
        //                    //没有关联的
        //                    var idmr = metamorphicRelationlist[i].IdMR;
        //                    var mr = MR_repository.Get(idmr);
        //                    if (mr == null)
        //                    {
        //                        //不存在于数据表中
        //                        metamorphicRelationlist[i].ApplicationName = application.Name;
        //                        id = MR_repository.Add_Return_ID(metamorphicRelationlist[i]);

        //                    }
        //                    else
        //                    {
        //                        id = idmr;
        //                        //存在于数据表中
        //                        if ( metamorphicRelationlist[i].ApplicationName != null)
        //                        {
        //                            metamorphicRelationlist[i].ApplicationName += ":";
        //                            metamorphicRelationlist[i].ApplicationName += application.Name;

        //                        }
        //                        else
        //                        {
        //                            metamorphicRelationlist[i].ApplicationName = application.Name;

        //                        }
        //                        MR_repository.Modify(metamorphicRelationlist[i]);
        //                    }
        //                }
        //                if (id != 0)
        //                {
        //                    if (strmrid != string.Empty)
        //                    {
        //                        strmrid += ":";
        //                        strmrid += id.ToString();
        //                    }
        //                    else
        //                    {
        //                        strmrid = id.ToString();
        //                    }
        //                }
        //            }
        //            application.MetamorphicRelationId = strmrid;
        //        }

        //        //保存Domain的Name
        //        var str1 = "";

        //        for (int i = 0; i < domainlist.Count; i++)
        //        {
        //            var name = domainlist[i].Name;
        //            var iddomain = Domain_Repository.Get(name);

        //            if (str1 == string.Empty)
        //            {
        //                str1 = domainlist[i].Name;
        //            }
        //            else
        //            {
        //                str1 += ":";
        //                str1 += domainlist[i].Name;
        //            }
        //            if (iddomain == 0)
        //            {
        //                //domainlist[i].ApplicationNames = application.Name;
        //                Domain_Repository.Add(domainlist[i]);
        //            }
        //            else
        //            {
        //                Domain_Repository.Modify(domainlist[i]);
        //            }

        //        }
        //        application.DomainName = str1;
        //        var result = Application_repository.Modify(application);
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
        /// <param name="application"></param>
        /// <returns></returns>
        public bool UpdateService(Application application)
        {
            var result = Application_repository.Modify(application);
            return result;
        }

        /// <summary>
        /// 上传程序压缩文件
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public bool UpdateCode(Application application, byte[] codeData, string CodeName)
        {
            if (Application_repository.Get(application.IdApplication) != null)
            {
                application.Code = codeData;
                application.CodeName = CodeName;
                var result = Application_repository.Modify(application);
                return result;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 撤回上传程序压缩文件
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public bool BackUpdateCode(Application application)
        {
            if (Application_repository.Get(application.IdApplication) != null)
            {
                application.Code = null;
                application.CodeName = string.Empty;
                var result = Application_repository.Modify(application);
                return result;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取程序压缩文件
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public byte[] GetZipCodeFileData(Application application)
        {
            var existingApplication = Application_repository.Get(application.IdApplication);
            if (Application_repository.Get(application.IdApplication) != null)
            {
                byte[] ZipCodeFile = existingApplication.Code;
                Console.WriteLine(ZipCodeFile);
                return ZipCodeFile;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取程序压缩文件名称
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public string GetZipCodeFileName(Application application)
        {
            if (Application_repository.Get(application.IdApplication) != null)
            {
                string ZipCodeName = application.CodeName;
                return ZipCodeName;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据Id获取程序压缩文件
        /// </summary>
        /// <param name="applicationid"></param>
        /// <returns></returns>
        public byte[] GetZipCodeFileDataById(int applicationid)
        {
            var existingApplication = Application_repository.Get(applicationid);
            if (Application_repository.Get(applicationid) != null)
            {
                byte[] ZipCodeFile = existingApplication.Code;
                Console.WriteLine(ZipCodeFile);
                return ZipCodeFile;
            }
            else
            {
                return null;
            }

        }
    }





}
