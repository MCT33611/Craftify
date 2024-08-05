using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Application.Profile.Common;
using Craftify.Application.Profile.Queries.GetAllProfiles;
using Craftify.Domain.Constants;
using ErrorOr;
using MediatR;

namespace Craftify.Application.Profile.Queries.GetAllCustomers
{
    public class GetAllCustomersQueryHandler(
        IUnitOfWork _unitOfWork
        ) : IRequestHandler<GetAllCustomersQuery, ErrorOr<IEnumerable<ProfileResult>>>
    {


        public async Task<ErrorOr<IEnumerable<ProfileResult>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var Customers = _unitOfWork.User.GetAll(u => u.Role == AppConstants.Role_Customer).ToList();

                var ProfileResults = Customers.Select(Customer => new ProfileResult
                {
                    Id = Customer.Id,
                    FirstName = Customer.FirstName,
                    LastName = Customer.LastName,
                    Email = Customer.Email,
                    StreetAddress = Customer.StreetAddress,
                    City = Customer.City,
                    State = Customer.State,
                    PostalCode = Customer.PostalCode,
                    ProfilePicture = Customer.ProfilePicture,
                    Role = Customer.Role,
                    Blocked = Customer.Blocked
                }).ToList();
                await Task.CompletedTask;
                return ProfileResults;
            }
            catch (Exception) {
                return Error.NotFound("Customers not found", "An error occurred while fetching Customers.");
            }
        }
    }
}



