using LiteDB;
using MetBench_Domain;
using System.Configuration;
using System.Reflection;

namespace MetBench_DAL
{
    //数据库配置单例类
    public sealed class DbConfig
    {
        //对象集合的名称
        //key of  collection
        public readonly string MetamorphicRelations_Collection_Key = "MetamorphicRelations";
        public readonly string Applications_Collection_Key = "Applications";
        public readonly string Domains_Collection_Key = "Domains";
        /// <summary>
        /// 连接字符串
        /// database connection string
        /// </summary>
        //配置文件读取数据库连接字符串
        public string _conn
        {
            get
            {
                //读取数据库连接字符串
                //read connectionstring.

                Assembly assembly = Assembly.GetEntryAssembly();
                // 获取执行程序集的文件路径
                string assemblyPath = assembly.Location;

                // 获取解决方案的目录路径
                string solutionDirPath = Path.GetDirectoryName(assemblyPath);

                // 循环向上查找解决方案文件（.sln）
                while (!Directory.GetFiles(solutionDirPath, "*.sln").Any())
                {
                    // 获取上级目录路径
                    string parentDirPath = Directory.GetParent(solutionDirPath)?.FullName;

                    // 如果已经到达根目录，则返回空字符串
                    if (parentDirPath == null)
                    {
                        return string.Empty;
                    }

                    solutionDirPath = parentDirPath;
                }
                var db_file = ConfigurationManager.ConnectionStrings["litedb"].ConnectionString;
                //string appName = Assembly.GetEntryAssembly().GetName().Name;//获取应用程序名称
                string appPath = $"{solutionDirPath}\\MetBench_DataBase";//获取应用程序的路径
                var conn = db_file.Replace("|DataDirectory|", appPath);
                return conn;
            }
        }
        //DbConfig实例
         private static DbConfig instance;
        //使用单例模式 完成实体映射数据表
        private DbConfig()
        {
            //实体映射数据表
            using (var db = new LiteDatabase(_conn))
            {
                var mapper = BsonMapper.Global;
                //建立引用
                
                if (!db.CollectionExists(MetamorphicRelations_Collection_Key)) 
                {
                    //配置MetamorphicRelations
                    mapper.Entity<MetamorphicRelation>()
                   .Id(x => x.IdMR);
                }
                if (!db.CollectionExists(Applications_Collection_Key))
                {
                    //配置Applications
                    mapper.Entity<Application>()
                    .Id(x => x.IdApplication)
                    .Field(x => x.DomainName, "DomainName"); // 映射属性到字段;
                }
                if (!db.CollectionExists(Domains_Collection_Key))
                {
                    //配置Domains
                    mapper.Entity<Domain>()
                    .Id(x => x.IdDomain);
                }
            }
        }
        public static DbConfig GetInstance() 
        {
            if (instance == null) 
            {
                instance = new DbConfig();
            }
            return instance;
        }
    }
}
