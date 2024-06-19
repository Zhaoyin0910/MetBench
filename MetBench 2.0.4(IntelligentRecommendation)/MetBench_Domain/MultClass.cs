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
        //获取枚举类型RtType的全部枚举项名称,并加上 "全部类型"
        public static List<string> GetRtTypeList()
        {
            List<string> rtTypeList = new List<string>() { "全部类型" };
            //List<string> rtTypeList = new List<string>() { "All" };
            List<string> list = new List<string>(Enum.GetNames(typeof(RtType)));
            rtTypeList.AddRange(list);
            //rtTypeList.Add(new,Enum.GetNames(typeof(RtType)));
            return rtTypeList;
        }
        //获取OrderOfMR的链表
        public static List<string> GetOrderOfMR() 
        {
            //修改前
            //List<string> orderOfMR = new List<string>() { "全部关系", "二元关系", "三元关系" };
            //修改后
            List<string> orderOfMR = new List<string>() { "全部关系", "二元关系", "三元关系", "多元关系" };
            //List<string> orderOfMR = new List<string>() { "All", "Binary relation", "Ternary relation", "N-ary relation" };
            return orderOfMR;
        }
        //读文件
        public static byte[] ReadBinaryFile(string filePath)
        {
            byte[] code;
            //打开文件 读文件
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                code = new byte[fs.Length];
                fs.Read(code, 0, (int)fs.Length);
            }
            return code;
        }
        //写文件
        public static void WriteBinayFile(string filePath, byte[] code)
        {
            Console.WriteLine(filePath);
            if (!File.Exists(filePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                File.Create(filePath);
            }
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                fs.Write(code, 0, code.Length);
            }
        }
       

    }
}
