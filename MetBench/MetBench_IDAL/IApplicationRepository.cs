using MetBench_Domain;
using System.Collections.ObjectModel;

namespace MetBench_IDAL
{
    public interface IApplicationRepository : IRepository<Application>
    {
        /// <summary>
        /// 获得应用程序的Id
        /// </summary>
        /// <param name="Name">应用程序的名称</param>
        /// <returns>应用程序的Id</returns>
        //返回id
        int Get(string Name);

        /// <summary>
        ///  三表联合查询
        /// </summary>
        /// <returns>查询结果</returns>
        ObservableCollection<Applications_QueryResultData> GetAll_MIX();
    }
}
