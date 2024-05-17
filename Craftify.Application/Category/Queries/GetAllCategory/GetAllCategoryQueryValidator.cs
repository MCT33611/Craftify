using Craftify.Application.Profile.Queries.GetProfile;
using Craftify.Application.Service.Queries.GetService;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Category.Queries.GetAllCategory
{
    public class GetAllCategoryQueryValidator : AbstractValidator<GetAllCategoryQuery>
    {
        public GetAllCategoryQueryValidator()
        {
        }

    }
}
