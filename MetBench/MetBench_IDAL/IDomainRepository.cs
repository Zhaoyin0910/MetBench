using MetBench_Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetBench_IDAL
{
    public interface IDomainRepository : IRepository<Domain>
    {
        /// <summary>
        /// 获得应用领域的Id
        /// </summary>
        /// <param name="Name">应用领域的名称</param>
        /// <returns>应用领域的Id</returns>
        int Get(string Name);

    }
}
