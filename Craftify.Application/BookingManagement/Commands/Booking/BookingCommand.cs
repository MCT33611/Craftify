using Craftify.Domain.Entities;
using Craftify.Domain.Enums;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.BookingManagement.Commands.Booking
{
    public record BookingCommand(
     Guid Id,

     int WorkingTime,

     BookingStatus Status,

     DateTime Date,


     string Location,

     string LocationName,

     Guid CustomerId,

     User Customer,

     Guid ProviderId,

     Worker Provider
     ) : IRequest<ErrorOr<Guid>>;
}
