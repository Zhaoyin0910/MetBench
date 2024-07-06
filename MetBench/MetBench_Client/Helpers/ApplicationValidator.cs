using FluentValidation;
using FluentValidation.Validators;
using MetBench_Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetBench_Client.Helpers
{
    public class ApplicationValidator : AbstractValidator<Application>
    {
        public ApplicationValidator()
        {
            RuleFor(t => t.Name).NotEmpty().WithMessage("请填写Name");
            RuleFor(t => t.Code).NotNull().WithMessage("请上传Code");
            RuleFor(t => t.ProgrammingLanguage).NotEmpty().WithMessage("请填写ProgrammingLanguage");
            // 添加条件，当 ExecutNumber 空时才执行判断
            When(t => !string.IsNullOrEmpty(t.ProgrammingLanguage), () =>
            {
                RuleFor(t => t.ProgrammingLanguage).Must(BeValidProgrammingLanguage).WithMessage("ProgrammingLanguage必须为Python、Java、C++或C");
            });
        }
        private bool BeValidProgrammingLanguage(string programmingLanguage)
        {
            // 检查是否为Python、Java、C++或C，可以根据实际需求扩展
            string[] validLanguages = { "Python", "Java", "C++", "C" };
            return validLanguages.Contains(programmingLanguage);
        }
    }
}
