using Craftify.Application.Profile.Queries.GetProfile;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Service.Queries.GetService
{
    public class GetServiceQueryValidator : AbstractValidator<GetServiceQuery>
    {
        public GetServiceQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }

    }
}
