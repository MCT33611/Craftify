using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Common.Interfaces.Service
{
    public interface ICloudinaryService
    {
        Task<string> UploadAsync(IFormFile file, string folder);
    }
}
