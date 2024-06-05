using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Application.Profile.Common;
using Craftify.Application.Service.Common;
using Craftify.Application.Service.Queries.GetAllService;
using ErrorOr;
using MediatR;

namespace Craftify.Application.Profile.Queries.GetAllProfiles
{
    public class GetAllProfilesQueryHandler(
        IUnitOfWork _unitOfWork
        ) : IRequestHandler<GetAllProfilesQuery, ErrorOr<IEnumerable<ProfileResult>>>
    {

        /*public async Task<ErrorOr<IEnumerable<ProfileResult>>> Handle(GetAllProfilesQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            try
            {
                var profiles = _unitOfWork.User.GetAll().ToList();
                var profileResults = profiles.Select(profile => new ProfileResult
                {
                    Id = profile.Id,
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    Email = profile.Email,
                    StreetAddress = profile.StreetAddress,
                    City = profile.City,
                    State = profile.State,
                    PostalCode = profile.PostalCode,
                    ProfilePicture = profile.ProfilePicture,
                    Role = profile.Role
                }).ToList();

                return profileResults;
            }
            catch (Exception)
            {
                // Return an ErrorOr with the exception
                return Error.NotFound();
            }
        }*/


        public async Task<ErrorOr<IEnumerable<ProfileResult>>> Handle(GetAllProfilesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var profiles = _unitOfWork.User.GetAll().ToList();
                var profileResults = profiles.Select(profile => new ProfileResult
                {
                    Id = profile.Id,
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    Email = profile.Email,
                    StreetAddress = profile.StreetAddress,
                    City = profile.City,
                    State = profile.State,
                    PostalCode = profile.PostalCode,
                    ProfilePicture = profile.ProfilePicture,
                    Role = profile.Role
                }).ToList();
                await Task.CompletedTask;
                return profileResults;
            }
            catch (Exception _)
            {
                // Log the exception (optional)
                // _logger.LogError(ex, "An error occurred while getting profiles.");

                // Return a generic not found error
                return Error.NotFound("Profiles not found", "An error occurred while fetching profiles.");
            }
        }
    }
}


