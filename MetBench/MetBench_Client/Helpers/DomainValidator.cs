using FluentValidation;
using MetBench_Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetBench_Client.Helpers
{
    public class DomainValidator: AbstractValidator<Domain>
    {
        public DomainValidator()
        {
            RuleFor(t => t.Name).NotEmpty().WithMessage("请填写Name");
        }
    }
}
