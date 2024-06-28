using Craftify.Domain.Entities;
using Craftify.Domain.Enums;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.BookingManagement.Commands.UpdateBookingDetails
{
    public record UpdateBookingCommand(
     Guid Id,

     BookingStatus Status

     ) : IRequest<ErrorOr<Guid>>;
}
