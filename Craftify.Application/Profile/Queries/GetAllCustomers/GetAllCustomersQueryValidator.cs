using Craftify.Application.Profile.Queries.GetAllProfiles;
using Craftify.Application.Profile.Queries.GetProfile;
using FluentValidation;

namespace Craftify.Application.Profile.Queries.GetAllCustomers
{
    public class GetAllCustomersQueryValidator : AbstractValidator<GetAllCustomersQuery>
    {
        public GetAllCustomersQueryValidator()
        {
        }

    }
}
