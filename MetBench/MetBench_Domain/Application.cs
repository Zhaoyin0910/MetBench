using LiteDB;
using System.Collections.ObjectModel;

namespace MetBench_Domain
{
    //Application
    public class Application : IEquatable<Application>
    {
        [BsonId]
        public int IdApplication { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProgrammingLanguage { get; set; } 
        public int LinesOfCode { get; set; }//非空字段
        //程序源码或二进制文件 压缩包转换为字节数组
        public byte[] Code { get; set; }
        #region
        public string CodeName { get; set; }
        #endregion
        //测试用例
        public byte[] SourceTestCase { get; set; }
        public string SourceTestCaseName { get; set; }
        public string DOI { get; set; }
        public string Url { get; set; }
        ////MetamorphicRelation的ID
        public string MetamorphicRelationId { get; set; }=String.Empty;//当MetamorphicRelationId为空字符串时，Litedb不会进行存储  MRId之间用:隔开
        //Domain的Name
        public string DomainName { get; set; } = String.Empty; // DomainName之间用:隔开
        //
        public bool Equals(Application application)
        {
            return IdApplication == application.IdApplication;
        }
    }
}
