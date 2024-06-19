using FluentValidation;
using LiteDB;
using MetBench_DAL;
using MetBench_Domain;
using MetBench_IDAL;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;


namespace MetBench_Client.Helpers
{
    public class MetamorphicRelationValidator : AbstractValidator<MetamorphicRelation>
    {        //数据库连接字符串 
        private string _conn;
        private DbConfig _dbConfig;
        private ILiteCollection<MetamorphicRelation> MetamorphicRelations;
        private ILiteCollection<Application> Applications;
        private ILiteCollection<Domain> Domains;
        public MetamorphicRelationValidator()
        {
            //获得DbConfig对象
            _dbConfig = DbConfig.GetInstance();
            _conn = _dbConfig._conn;

            RuleFor(t => t.InputPattern).NotEmpty().WithMessage("请填写InputPattern");
            RuleFor(t => t.OutputPattern).NotEmpty().WithMessage("请填写OutputPattern");
            RuleFor(t => t.DimensionOfInputPattern).NotEmpty().WithMessage("请填写DimensionOfInputPattern");
            RuleFor(t => t.DimensionOfOutputPattern).NotEmpty().WithMessage("请填写DimensionOfOutputPattern");
            RuleFor(t => t.ApplicationName).NotEqual(string.Empty).WithMessage("请填写ApplicationName");

            //修改后
            // UTF-16编码的字符串
            //string utf16String = "全部关系";
            // 将UTF-16编码的字符串转换为UTF-8编码的字节数组
            //byte[] utf8Bytes = Encoding.UTF8.GetBytes(utf16String);
            // 将UTF-8编码的字节数组转换为字符串（仅用于演示，实际应用中可能需要指定编码）
            //string utf8String = Encoding.UTF8.GetString(utf8Bytes);
            RuleFor(t => t.OrderOfMR).NotEqual("全部关系").WithMessage("请选择OrderOfMR");
            RuleFor(t => (t.RepresentationType).ToString()).NotEqual("-1").WithMessage("请选择RepresentationType");
            // 添加条件，当 InputPattern 非空时才执行判断latex是否合法
            When(t => !string.IsNullOrEmpty(t.InputPattern), () =>
            {
                RuleFor(t => t.InputPattern).Must(IsValidLatexCharacters).WithMessage("请填写标准LaTeX格式的InputPattern");
            });
            // 添加条件，当 OutputPattern 非空时才执行判断latex是否合法
            When(t => !string.IsNullOrEmpty(t.OutputPattern), () =>
            {
                RuleFor(t => t.OutputPattern).Must(IsValidLatexCharacters).WithMessage("请填写标准LaTeX格式的OutputPattern");
            });
            // 添加条件，当 DimensionOfInputPattern 非空时才执行判断数据必须为正整数
            When(t => !string.IsNullOrEmpty(t.DimensionOfInputPattern), () =>
            {
                RuleFor(t => t.DimensionOfInputPattern).Must(IsValidPositiveInteger).WithMessage("DimensionOfInputPattern应为正整数");
            });
            // 添加条件，当 DimensionOfOutputPattern 非空时才执行判断数据必须为正整数
            When(t => !string.IsNullOrEmpty(t.DimensionOfOutputPattern), () =>
            {
                RuleFor(t => t.DimensionOfOutputPattern).Must(IsValidPositiveInteger).WithMessage("DimensionOfOutputPattern应为正整数");
            });
        }
        private bool IsValidLatexCharacters(string input)
        {
           // 检查是否包含非法字符
            string invalidCharactersPattern = @"[^a-zA-Z0-9\\{}\^_+\-*\/.,()=<>[\]\s]";

            //检查是否有相邻的反斜杠，因为 LaTeX 中通常是一个命令
            string adjacentBackslashPattern = @"\\{2,}";

            //检查是否有相邻的下划线，因为 LaTeX 中通常是一个命令
            string adjacentUnderscorePattern = @"_{2,}";

            //检查逗号后是否有空格
            string commaWithoutSpacePattern = @",(?!\s)";
            //检查连续的井号
            string consecutiveHashPattern = @"#{2,}";

            //使用正则表达式匹配输入字符串
            if (Regex.IsMatch(input, invalidCharactersPattern) ||
                Regex.IsMatch(input, adjacentBackslashPattern) ||
                Regex.IsMatch(input, adjacentUnderscorePattern) ||
                Regex.IsMatch(input, commaWithoutSpacePattern) ||
                Regex.IsMatch(input, consecutiveHashPattern))
            {
                return false;
            }

            return true;
        }
        private bool IsValidPositiveInteger(string input)
        {
            return int.TryParse(input, out int result) && result > 0;
        }
        public bool IsDuplicate(MetamorphicRelation relationToCheck, string domainName)
        {
            using (var db = new LiteDatabase(_conn))
            {
                var result = new ObservableCollection<MetamorphicRelations_QueryResultData>();

                if (relationToCheck != null)
                {
                    //metamorphicRelation.Application.Domains.Add(new Domain(){Name = Domain的Name})   domain(保存了Domain的Name)
                    //使用linq语句查询 两表联合
                    MetamorphicRelations = db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key);
                    Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);
                    Domains = db.GetCollection<Domain>(_dbConfig.Domains_Collection_Key);


                    var metamorphicRelations = new ObservableCollection<MetamorphicRelation>(MetamorphicRelations.FindAll());
                    var applictaions = new ObservableCollection<Application>(Applications.FindAll());
                    var domains = new ObservableCollection<Domain>(Domains.FindAll());
                    //中间类集合
                    var metamorphicRelationApplications = new ObservableCollection<MetamorphicRelationApplication>();
                    for (int i = 0; i < metamorphicRelations.Count; i++)
                    {
                        string str = metamorphicRelations[i].ApplicationName;
                        string[] strarray = str.Split(':');
                        for (int j = 0; j < strarray.Length; j++)
                        {
                            var idmr = metamorphicRelations[i].IdMR;
                            var mrapplication = new MetamorphicRelationApplication() { IdMR = idmr, ApplicationName = strarray[j] };
                            metamorphicRelationApplications.Add(mrapplication);
                        }
                    }
                    //修改后
                    var MRs_Query1 = (
                             from MetamorphicRelation in metamorphicRelations
                             join MetamorphicRelationApplication in metamorphicRelationApplications on MetamorphicRelation.IdMR equals MetamorphicRelationApplication.IdMR
                             join Application in applictaions on MetamorphicRelationApplication.ApplicationName equals Application.Name
                             select new
                             {
                                 metamorphicRelation = MetamorphicRelation,
                                 application = Application,
                             }
                        );
                    //两表联合查询 MetamorphicRealtions 与 Applications表联合查询
                    var MRs_Query2 = MRs_Query1.ToList();

                    for (int i = 0; i < MRs_Query2.Count; i++)
                    {
                        //应用程序对应的应用领域可能为null
                        var domainname = MRs_Query2[i].application.DomainName;
                        if (domainname == null)
                        {
                            //用空字符串占位
                            MRs_Query2[i].application.DomainName = "";
                        }
                        if (MRs_Query2[i].metamorphicRelation.Constraint == null|| MRs_Query2[i].metamorphicRelation.Constraint == string.Empty|| MRs_Query2[i].metamorphicRelation.Constraint == "")
                        {
                            MRs_Query2[i].metamorphicRelation.Constraint = "";
                        }

                        if (MRs_Query2[i].metamorphicRelation.Context == null || MRs_Query2[i].metamorphicRelation.Constraint == string.Empty || MRs_Query2[i].metamorphicRelation.Constraint == "")
                        {
                            MRs_Query2[i].metamorphicRelation.Context = "";
                        }

                        if (MRs_Query2[i].metamorphicRelation.Description == null || MRs_Query2[i].metamorphicRelation.Constraint == string.Empty || MRs_Query2[i].metamorphicRelation.Constraint == "")
                        {
                            MRs_Query2[i].metamorphicRelation.Description = "";
                        }
                    }
                    var MRs_Query = MRs_Query2.Where(x =>
                   (string.IsNullOrEmpty(relationToCheck.OrderOfMR) || x.metamorphicRelation.OrderOfMR == relationToCheck.OrderOfMR)
                   && (x.metamorphicRelation.RepresentationType == relationToCheck.RepresentationType || (int)relationToCheck.RepresentationType == -1)
                   && (string.IsNullOrEmpty(relationToCheck.Context) || x.metamorphicRelation.Context == relationToCheck.Context)
                   && (string.IsNullOrEmpty(relationToCheck.Constraint) || x.metamorphicRelation.Constraint == relationToCheck.Constraint)
                   && (string.IsNullOrEmpty(relationToCheck.Description) || x.metamorphicRelation.Description == relationToCheck.Description)
                   && (string.IsNullOrEmpty(relationToCheck.InputPattern) || x.metamorphicRelation.InputPattern == relationToCheck.InputPattern)
                   && (string.IsNullOrEmpty(relationToCheck.OutputPattern) || x.metamorphicRelation.OutputPattern == relationToCheck.OutputPattern)
                   && (string.IsNullOrEmpty(relationToCheck.DimensionOfInputPattern) || x.metamorphicRelation.DimensionOfInputPattern == relationToCheck.DimensionOfInputPattern)
                   && (string.IsNullOrEmpty(relationToCheck.DimensionOfOutputPattern) || x.metamorphicRelation.DimensionOfOutputPattern == relationToCheck.DimensionOfOutputPattern)
                   && (string.IsNullOrEmpty(relationToCheck.ApplicationName) || x.application.Name == relationToCheck.ApplicationName)
                   && (string.IsNullOrEmpty(domainName) || x.application.DomainName == domainName)
);
                    foreach (var mr_query in MRs_Query)
                    {
                        //两表联合查出的MR
                        var mr = mr_query.metamorphicRelation;
                        var app = mr_query.application;

                        var mr1 = new MetamorphicRelations_QueryResultData()
                        {
                            IdMR = mr.IdMR,
                            Description = mr.Description,
                            Context = mr.Context,
                            Constraint = mr.Constraint,
                            OrderOfMR = mr.OrderOfMR,
                            RepresentationType = mr.RepresentationType,
                            InputPattern = mr.InputPattern,
                            OutputPattern = mr.OutputPattern,
                            InputPatternImageData = mr.InputPatternImageData,
                            OutputPatternImageData = mr.OutputPatternImageData,//图片数据
                            DimensionOfInputPattern = mr.DimensionOfInputPattern,
                            DimensionOfOutputPattern = mr.DimensionOfOutputPattern,
                            ApplicationName = app.Name,
                            CodeName = mr_query.application.CodeName
                        };
                        mr1.DomainName = mr_query.application.DomainName;//已经进行了非空处理
                        result.Add(mr1);
                    }
                }
                return result.Count > 0;
            }
        }
    }
}
