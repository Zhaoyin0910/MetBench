 using FluentValidation;
using MetBench_BLL;
using MetBench_Domain;
using System.Linq;
using System.Text.RegularExpressions;

namespace MetBench_Client.Helpers
{
    public class MetamorphicRelationValidator : AbstractValidator<MetamorphicRelation>
    {
        private MetamorphicRelationSerive _metamorphicRelationSerive;

        public MetamorphicRelationValidator(MetamorphicRelationSerive metamorphicRelationSerive)
        {
            _metamorphicRelationSerive = metamorphicRelationSerive ;

            RuleFor(t => t.InputPattern).NotEmpty().WithMessage("请填写InputPattern");
            RuleFor(t => t.OutputPattern).NotEmpty().WithMessage("请填写OutputPattern");
            RuleFor(t => t.DimensionOfInputPattern).NotEmpty().WithMessage("请填写DimensionOfInputPattern");
            RuleFor(t => t.DimensionOfOutputPattern).NotEmpty().WithMessage("请填写DimensionOfOutputPattern");
            RuleFor(t => t.ApplicationName).NotEqual(string.Empty).WithMessage("请填写ApplicationName");
            RuleFor(t => t.OrderOfMR).NotEqual("全部关系").WithMessage("请选择OrderOfMR");
            RuleFor(t => (t.RepresentationType).ToString()).NotEqual("-1").WithMessage("请选择RepresentationType");

            //// 添加条件，当 InputPattern 非空时才执行判断latex是否合法
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

        // 正确的Latex形式的规范
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
                Regex.IsMatch(input, adjacentUnderscorePattern)||
                Regex.IsMatch(input, commaWithoutSpacePattern) ||
                Regex.IsMatch(input, consecutiveHashPattern))
            {
                return false;
            }

            return true;
        }

        // 正数的规范
        private bool IsValidPositiveInteger(string input)
        {
            return int.TryParse(input, out int result) && result > 0;
        }

        // 检查是否为同一个MR
        public bool IsDuplicate(MetamorphicRelation relationToCheck)
        {
            if (relationToCheck != null)
            {
                ////使用linq语句查询 两表联合
                //MetamorphicRelations = db.GetCollection<MetamorphicRelation>(_dbConfig.MetamorphicRelations_Collection_Key);
                //Applications = db.GetCollection<Application>(_dbConfig.Applications_Collection_Key);

                //var metamorphicRelations = new ObservableCollection<MetamorphicRelation>(MetamorphicRelations.FindAll());
                //var applictaions = new ObservableCollection<Application>(Applications.FindAll());

                ////中间类集合
                //var metamorphicRelationApplications = new ObservableCollection<MetamorphicRelationApplication>();
                //for (int i = 0; i < metamorphicRelations.Count; i++)
                //{
                //    string str = metamorphicRelations[i].ApplicationName;
                //    string[] strarray = str.Split(':');
                //    for (int j = 0; j < strarray.Length; j++)
                //    {
                //        var idmr = metamorphicRelations[i].IdMR;
                //        var mrapplication = new MetamorphicRelationApplication() { IdMR = idmr, ApplicationName = strarray[j] };
                //        metamorphicRelationApplications.Add(mrapplication);
                //    }
                //}
                ////修改后
                //var MRs_Query1 = (
                //         from MetamorphicRelation in metamorphicRelations
                //         join MetamorphicRelationApplication in metamorphicRelationApplications on MetamorphicRelation.IdMR equals MetamorphicRelationApplication.IdMR
                //         join Application in applictaions on MetamorphicRelationApplication.ApplicationName equals Application.Name
                //         select new
                //         {
                //             metamorphicRelation = MetamorphicRelation,
                //             application = Application,
                //         }
                //    );
                // 获取MetamorphicRelations的全部查询结果

                var MRs_Query1 = _metamorphicRelationSerive.GetAllMRs();

                // 判断是否为相同的MR
                var MRs_Query = MRs_Query1.Where(x =>
               (string.IsNullOrEmpty(relationToCheck.OrderOfMR) || x.OrderOfMR == relationToCheck.OrderOfMR)
               && (x.RepresentationType == relationToCheck.RepresentationType || (int)relationToCheck.RepresentationType == -1)
               && (string.IsNullOrEmpty(relationToCheck.Context) || x.Context == relationToCheck.Context)
               && (string.IsNullOrEmpty(relationToCheck.Constraint) || x.Constraint == relationToCheck.Constraint)
               && (string.IsNullOrEmpty(relationToCheck.Description) || x.Description == relationToCheck.Description)
               && (string.IsNullOrEmpty(relationToCheck.InputPattern) || x.InputPattern == relationToCheck.InputPattern)
               && (string.IsNullOrEmpty(relationToCheck.OutputPattern) || x.OutputPattern == relationToCheck.OutputPattern)
               && (string.IsNullOrEmpty(relationToCheck.DimensionOfInputPattern) || x.DimensionOfInputPattern == relationToCheck.DimensionOfInputPattern)
               && (string.IsNullOrEmpty(relationToCheck.DimensionOfOutputPattern) || x.DimensionOfOutputPattern == relationToCheck.DimensionOfOutputPattern)
               && (string.IsNullOrEmpty(relationToCheck.ApplicationName) || x.ApplicationName == relationToCheck.ApplicationName)
);
                var length = MRs_Query.Count();
                if (length > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }
    }
}
