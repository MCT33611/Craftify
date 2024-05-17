using Craftify.Application.Category.Queries.GetCategory;
using Craftify.Application.Profile.Queries.GetProfile;
using Craftify.Application.Service.Queries.GetService;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Service.Queries.GetAllService
{
    public class GetAllServiceQueryValidator : AbstractValidator<GetServiceQuery>
    {
        public GetAllServiceQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }

    }
}
