using FluentValidation;
using MetBench_BLL;
using MetBench_Domain;
using System.Linq;

namespace MetBench_Client.Helpers
{
    public class DomainValidator : AbstractValidator<Domain>
    {
        private DomainSerive _domainSerive;
        public DomainValidator(DomainSerive domainSerive)
        {
            _domainSerive = domainSerive;

            RuleFor(t => t.Name).NotEmpty().WithMessage("请填写Name");
            //RuleFor(t => t.Name).Must(IsRepeatedName).WithMessage("填写的Name已存在");
        }

        //检查是否为同一个Domain
        public bool IsDuplicate(Domain domainToCheck)
        {

            if (domainToCheck != null)
            {
                var Domain_Query1 = _domainSerive.GetAllDomians();
                //判断是否为相同的MR
                var Domain_Query = Domain_Query1.Where(x =>
               (x.Name == domainToCheck.Name)
               && (string.IsNullOrEmpty(domainToCheck.Description) || (x.Description == domainToCheck.Description))
);
                var length = Domain_Query.Count();
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

        ////检查Name是否为重复
        //private bool IsRepeatedName(string input)
        //{
        //    if (_domainSerive.GetAllDomians().Count > 0)
        //    {
        //        //ture 为Name重复 false为不重复
        //        var res = _domainSerive.GetDomainId(input) > 0;
        //        return !res;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}
    }
}
