using FluentValidation;
using MetBench_BLL;
using MetBench_Domain;
using System;
using System.Linq;

namespace MetBench_Client.Helpers
{
    public class ApplicationValidator : AbstractValidator<Application>
    {
        private ApplicationSerive _applicationSerive;

        public ApplicationValidator(ApplicationSerive applicationSerive)
        {
            // 通过依赖注入获取对象实体
            _applicationSerive = applicationSerive;

            RuleFor(t => t.Name).NotEmpty().WithMessage("请填写Name");
            //RuleFor(t => t.Name).Must(IsRepeatedName).WithMessage("填写的Name已存在");
            RuleFor(t => t.Code).NotNull().WithMessage("请上传SoftwareUnderTest");
            RuleFor(t => t.CodeName).NotEmpty().WithMessage("请上传SoftwareUnderTest");
            RuleFor(t => t.ProgrammingLanguage).NotEmpty().WithMessage("请填写ProgrammingLanguage");
            When(t => !string.IsNullOrEmpty(t.ProgrammingLanguage), () =>
            {
                RuleFor(t => t.ProgrammingLanguage).Must(IsValidProgrammingLanguage).WithMessage("ProgrammingLanguage应为“Python”、“Java”、“C”");
            });
            // 添加条件，当 DimensionOfInputPattern 非空时才执行判断数据必须为正整数
            RuleFor(t => t.LinesOfCode).NotEmpty().WithMessage("请填写LinesOfCode");
            RuleFor(t => t.LinesOfCode).NotEmpty().Must(IsValidPositiveInteger).WithMessage("LinesOfCode应为正整数");
        }

        // 检查是否为同一个Application
        public bool IsDuplicate(Application applicationToCheck)
        {
            if (applicationToCheck == null)
            {
                return false;
            }

            // 获取所有应用程序  
            var Apps_Query1 = _applicationSerive.GetApplications();

            bool aa = Apps_Query1.Any(x => (x.SourceTestCaseName == applicationToCheck.SourceTestCaseName || (x.SourceTestCaseName == null && applicationToCheck.SourceTestCaseName == string.Empty)));

            // 判断是否为相同的Application  
            bool isDuplicate = Apps_Query1.Any(x =>
                x.Name == applicationToCheck.Name &&
                x.Description == applicationToCheck.Description &&
                x.ProgrammingLanguage == applicationToCheck.ProgrammingLanguage &&
                x.LinesOfCode == applicationToCheck.LinesOfCode &&
                (x.SourceTestCase == null && applicationToCheck.SourceTestCase == null ) &&
                (x.SourceTestCaseName == applicationToCheck.SourceTestCaseName || (x.SourceTestCaseName == null && applicationToCheck.SourceTestCaseName == string.Empty)) &&
                x.DOI == applicationToCheck.DOI &&
                x.Url == applicationToCheck.Url &&
                (x.DomainName == applicationToCheck.DomainName || (x.DomainName == null && applicationToCheck.DomainName == string.Empty)) // 处理DomainName为null的情况  
            );

            return isDuplicate;
        }

        // 检查是否为一个正数
        private bool IsValidPositiveInteger(int input)
        {
            return input > 0;
        }

        // 检查ProgrammingLanguage是否为规定语言
        private bool IsValidProgrammingLanguage(string input)
        {
            //return input == "java" || input == "python" || input == "c";
            string[] validExtensions = { "c", "java", "python" };

            string fileExtension = input;

            return validExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase);
        }

        //// 检查Name是否为重复
        //private bool IsRepeatedName(string input)
        //{
        //    if (_applicationSerive.GetApplications().Count > 0)
        //    {
        //        //ture 为Name重复 false为不重复
        //        var res = _applicationSerive.GetApplicationId(input) > 0;
        //        return !res;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}
    }
}
