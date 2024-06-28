using Craftify.Application.Profile.Queries.GetProfile;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Plan.Queries.GetPlan
{
    public class GetPlanQueryValidator : AbstractValidator<GetPlanQuery>
    {
        public GetPlanQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }

    }
}
