using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetBench_Domain
{
    public static class MultClass
    {
        //获取枚举类型RtType的全部枚举项名称,返回一个第一项为“全部类型”的链表
        public static List<string> GetRtTypeList()
        {
            List<string> rtTypeList = new List<string>() { "全部类型" };
            List<string> list = new List<string>(Enum.GetNames(typeof(RtType)));
            rtTypeList.AddRange(list);
            return rtTypeList;
        }

        //获取OrderOfMR的链表，并返回一个第一项为“全部关系”的链表
        public static List<string> GetOrderOfMR() 
        {
            List<string> orderOfMR = new List<string>() { "全部关系", "二元关系", "三元关系", "多元关系" };
            return orderOfMR;
        }
      
    }
}
