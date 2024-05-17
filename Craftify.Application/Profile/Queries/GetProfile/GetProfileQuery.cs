using Craftify.Application.Profile.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Profile.Queries.GetProfile
{
    public record GetProfileQuery(
        Guid Id
        ):IRequest<ErrorOr<ProfileResult>>;
}
