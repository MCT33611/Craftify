using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Craftify.Application.Common.Interfaces.Service;
using Microsoft.AspNetCore.Http;

namespace Craftify.Infrastructure.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(string cloudName, string apiKey, string apiSecret)
        {
            Account cloudinaryAccount = new(cloudName, apiKey, apiSecret);
            _cloudinary = new Cloudinary(cloudinaryAccount);
        }

        public async Task<string> UploadAsync(IFormFile file, string folder)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                Folder = folder
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            return uploadResult.SecureUri.AbsoluteUri ?? "";
        }
    }
}
