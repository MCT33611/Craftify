using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Contracts.Profile
{
    public record ProfileRequest(
        string? FirstName,

        string? LastName ,

        string? ProfilePicture,
        string? StreetAddress ,
        string? City,
        string? State,
        string? PostalCode


        );
}
