using Craftify.Domain.Entities;
using Craftify.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Contracts.Booking
{
    public record BookingRequest(

     int WorkingTime,

     BookingStatus Status,

     DateTime Date,

     string Location,

     string LocationName,

     Guid CustomerId,


     Guid ProviderId

     );
}
