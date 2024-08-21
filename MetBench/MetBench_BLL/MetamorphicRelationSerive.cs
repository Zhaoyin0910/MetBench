using MetBench_Domain;
using MetBench_IDAL;
using System.Collections.ObjectModel;

namespace MetBench_BLL
{
    public class MetamorphicRelationSerive
    {
        private IMetamorphicRelationRepository MR_repository ;
        private IApplicationRepository Application_repository ;
        private IDomainRepository Domain_Repository ;

        //依赖注入实体
        public MetamorphicRelationSerive(IMetamorphicRelationRepository metamorphicRelationRepository, IApplicationRepository applicationRepository, IDomainRepository domainRepository) 
        {
            MR_repository = metamorphicRelationRepository;
            Application_repository = applicationRepository;
            Domain_Repository = domainRepository;
        }
        /// <summary>
        /// 获取蜕变关系
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MetamorphicRelation GetMRById(int id)
        {
            var result = MR_repository.Get(id);
            return result;
        }

        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="metamorphicRelation"></param>
        /// <param name="applicationlist">该集合的每个元素的Name为metamorphicRealtion关联的application的Name</param>
        /// <param name="domainlist">该集合的每个application关联的DomainName</param>
        /// <returns></returns>
        //public bool AddService(MetamorphicRelation metamorphicRelation, List<Application> applicationlist, List<Domain> domainlist)
        //{
        //    var _id = MR_repository.Add_Return_ID(metamorphicRelation);

        //    if (_id > 0)
        //    {
        //        //保存domain的Name
        //        var str = string.Empty;
        //        //判断domainlist是否为空
        //        if (domainlist != null)
        //        {
        //            //将每一个Domain的名称都加入到domain的Name中
        //            for (int j = 0; j < domainlist.Count; j++)
        //            {
        //                if (j == 0)
        //                {
        //                    str += domainlist[j].Name;
        //                }
        //                else
        //                {
        //                    str += ":";
        //                    str += domainlist[j].Name;
        //                }
        //            }
        //        }

        //        for (int i = 0; i < applicationlist.Count; i++)
        //        {
        //            var name = applicationlist[i].Name;
        //            var idapplication = Application_repository.Get(name);
        //            //应用程序不存在于表中
        //            if (idapplication == 0)
        //            {
        //                applicationlist[i].MetamorphicRelationId = _id.ToString();
        //                applicationlist[i].DomainName = str;//若str为string.Empty也不会存储在数据库中
        //                Application_repository.Add(applicationlist[i]);
        //            }
        //            else
        //            {
        //                //保存关联的蜕变关系的ID
        //                string str1 = applicationlist[i].MetamorphicRelationId;
        //                //当蜕变关系的ID为null或者为空字符串，直接加入不需要加分界符
        //                if (str1 == null )
        //                {
        //                    str1 = _id.ToString();
        //                }
        //                else
        //                {
        //                    str1 += ":";
        //                    str1 += _id.ToString();
        //                }

        //                applicationlist[i].MetamorphicRelationId = str1;
        //                applicationlist[i].DomainName = str;
        //                Application_repository.Modify(applicationlist[i]);
        //            }
        //        }


        //        for (int i = 0; i < domainlist.Count; i++)
        //        {
        //            var name = domainlist[i].Name;
        //            var iddomain = Domain_Repository.Get(name);
        //            //应用领域不存在于表中
        //            if (iddomain == 0)
        //            {
        //                Domain_Repository.Add(domainlist[i]);
        //            }
        //            else
        //            {
        //                Domain_Repository.Modify(domainlist[i]);
        //            }
        //        }
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="metamorphicRelation"></param>
        /// <returns></returns>
        public bool AddService(MetamorphicRelation metamorphicRelation)
        {
            //var _id = MR_repository.Add_Return_ID(metamorphicRelation);
            var result = MR_repository.Add(metamorphicRelation);
            return result;
        }

        /// <summary>
        /// 删除服务
        /// </summary>
        /// <param name="metamorphicRelation"></param>
        /// <returns></returns>
        public bool DeleteService(MetamorphicRelation metamorphicRelation)
        {
            var result = MR_repository.Remove(metamorphicRelation);
            return result;
        }

        /// <summary>
        /// 查询服务
        /// </summary>
        /// <param name="metamorphicRelation"></param>
        /// <param name="domainNames"></param>
        /// <returns></returns>
        public ObservableCollection<MetamorphicRelations_QueryResultData> QueryService(MetamorphicRelation metamorphicRelation, string domainName)
        {
            var result = MR_repository.Get(metamorphicRelation, domainName);
            return result;
        }

        /// <summary>
        /// 两表联合查询
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<MetamorphicRelations_QueryResultData> showMultTwoTableResult()
        {
            var result = MR_repository.GetAll_MIXTwoTable();
            return result;
        }

        /// <summary>
        /// 三表联合查询
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<MetamorphicRelations_QueryResultData> showMultThreeTableResult()
        {
            var result = MR_repository.GetAll_MIX();
            return result;
        }

        /// <summary>
        /// 更新服务
        /// </summary>
        /// <param name="metamorphicRelation"></param>
        /// <param name="applicationlist">该集合的每个元素的Name为metamorphicRealtion关联的application的Name</param>
        /// <param name="domainlist">该集合的每个application关联的DomainName</param>
        /// <returns></returns>
        //public bool UpdateService(MetamorphicRelation metamorphicRelation, List<Application> applicationlist, List<Domain> domainlist)
        //{
        //    //判断要修改的蜕变关系是否存在表中
        //    var beforemetamorphicRelation = MR_repository.Get(metamorphicRelation.IdMR);
        //    bool exist = beforemetamorphicRelation != null;
        //    if (exist)
        //    {

        //        var idmr = metamorphicRelation.IdMR;
        //        var beforeapplicationNames = beforemetamorphicRelation.ApplicationName;//修改前蜕变关系关联的应用程序
        //        //关联的应用程序
        //        var strarray = beforeapplicationNames.Split(':');

        //        //保存没有改变关联的下标
        //        List<int> sameindexlist = new List<int>();

        //        for (int i = 0; i < strarray.Length; i++)
        //        {
        //            bool isequal = false;
        //            for (int j = 0; j < applicationlist.Count; j++)
        //            {
        //                var applicationname = applicationlist[j].Name;
        //                if (applicationname.Equals(strarray[i]))
        //                {
        //                    isequal = true;
        //                    sameindexlist.Add(j);
        //                }
        //            }
        //            //要进行取消关联
        //            if (!isequal)
        //            {
        //                //该蜕变关系不再关联此应用程序
        //                string temp;
        //                var idbeforeapplication = Application_repository.Get(strarray[i]);
        //                var beforeapplication = Application_repository.Get(idbeforeapplication);
        //                //关联的蜕变关系
        //                var mrid = beforeapplication.MetamorphicRelationId;
        //                var mrstrarray = mrid.Split(":").ToList();

        //                var nowmrstr = "";
        //                for (int j = 0; j < mrstrarray.Count; j++)
        //                {
        //                    if (idmr.ToString() == mrstrarray[j])
        //                    {
        //                        mrstrarray.Remove(mrstrarray[j]);
        //                    }
        //                }
        //                for (int k = 0; k < mrstrarray.Count; k++)
        //                {
        //                    if (k == 0)
        //                    {
        //                        nowmrstr += mrstrarray[k];

        //                    }
        //                    else
        //                    {
        //                        nowmrstr += ":";
        //                        nowmrstr += mrstrarray[k];
        //                    }
        //                }
        //                beforeapplication.MetamorphicRelationId = nowmrstr;
        //                //现在不再关联
        //                Application_repository.Modify(beforeapplication);
        //            }
        //        }

        //        //保存domain的Name
        //        var str = "";
        //        for (int j = 0; j < domainlist.Count; j++)
        //        {
        //            if (j == 0)
        //            {
        //                str += domainlist[j].Name;
        //            }
        //            else
        //            {
        //                str += ":";
        //                str += domainlist[j].Name;
        //            }
        //        }

        //        var _id = metamorphicRelation.IdMR;
        //        //
        //        for (int i = 0; i < applicationlist.Count; i++)
        //        {
        //            //标记，是否为已经关联的
        //            var sign = false;
        //            for (int j = 0; j < sameindexlist.Count; j++)
        //            {
        //                if (i == sameindexlist[j])
        //                {
        //                    sign = true;
        //                }
        //            }
        //            if (sign)
        //            {
        //                //已经关联了
        //                applicationlist[i].DomainName = str;
        //                Application_repository.Modify(applicationlist[i]);
        //            }
        //            else
        //            {
        //                var name = applicationlist[i].Name;
        //                var idapplication = Application_repository.Get(name);
        //                //应用程序不存在于数据表中
        //                if (idapplication == 0)
        //                {
        //                    applicationlist[i].MetamorphicRelationId = _id.ToString();
        //                    applicationlist[i].DomainName = str;
        //                    Application_repository.Add(applicationlist[i]);
        //                }

        //                else
        //                {
        //                    //存在于表中
        //                    string str1 = applicationlist[i].MetamorphicRelationId;
        //                    if (str1 != null && str1 != string.Empty)
        //                    {
        //                        //没有关联的应用程序
        //                        applicationlist[i].MetamorphicRelationId += ":";
        //                        applicationlist[i].MetamorphicRelationId += _id.ToString();
        //                    }
        //                    else
        //                    {
        //                        applicationlist[i].MetamorphicRelationId = _id.ToString();
        //                    }
        //                    applicationlist[i].DomainName = str;
        //                    Application_repository.Modify(applicationlist[i]);

        //                }
        //            }
        //        }
        //        var res = MR_repository.Modify(metamorphicRelation);
        //        return res;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}


        /// <summary>
        /// 更新服务
        /// </summary>
        /// <param name="metamorphicRelation"></param>
        /// <returns></returns>
        public bool UpdateService(MetamorphicRelation metamorphicRelation)
        {
            var result = MR_repository.Modify(metamorphicRelation);
            return result;
        }

        /// <summary>
        /// 获取全部蜕变关系实体
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<MetamorphicRelation> GetAllMRs()
        {
            var result = MR_repository.GetAll();
            return result;

        }
    }
}
