using Craftify.Application.Profile.Common;
using Craftify.Application.Service.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Profile.Queries.GetAllProfiles
{
    public record GetAllProfilesQuery() : IRequest<ErrorOr<IEnumerable<ProfileResult>>>;
}
