using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Contracts.Profile
{
    public record ProfileRequest(
        string FirstName,

        string LastName ,

        string Email,

        bool EmailConfirmed,
        string? StreetAddress ,
        string? City,
        string? State,
        string? PostalCode,


        //latest updation date
        DateTime UpdatedDate ,

        string? ProfilePicture ,

        string Role
        );
}
