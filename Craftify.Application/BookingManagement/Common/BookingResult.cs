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

namespace Craftify.Application.BookingManagement.Common
{
    public record BookingResult(
     Guid Id,

     int WorkingTime ,

     BookingStatus Status ,

     DateTime Date,

     DateTime BookedAt,

     string Location,

     string LocationName,

     Guid CustomerId ,

     User Customer,

     Guid ProviderId ,

     Worker Provider 
     );
}
