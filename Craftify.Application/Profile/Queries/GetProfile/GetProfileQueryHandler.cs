using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Application.Profile.Common;
using Craftify.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Profile.Queries.GetProfile
{
    public class GetProfileQueryHandler(
        IUnitOfWork _unitOfWork
        ) : IRequestHandler<GetProfileQuery, ErrorOr<ProfileResult>>
    {
        public async Task<ErrorOr<ProfileResult>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var user = _unitOfWork.User.Get(u=>u.Id == request.Id);
            if (user == null)
                return Errors.User.InvaildCredetial;

            return new ProfileResult
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        StreetAddress = user.StreetAddress,
                        City = user.City,
                        State = user.State,
                        PostalCode = user.PostalCode,
                        ProfilePicture = user.ProfilePicture,
                        EmailConfirmed = user.EmailConfirmed
                    };


        }
    }
}
