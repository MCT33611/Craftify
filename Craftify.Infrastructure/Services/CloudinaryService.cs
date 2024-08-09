using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Craftify.Application.Common.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Craftify.Domain.Enums;

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
            var mediaType = DetermineMediaType(file.ContentType);
            var uploadParams = GetUploadParams(file, folder, mediaType);
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult?.SecureUri?.AbsoluteUri;
        }
        private RawUploadParams GetUploadParams(IFormFile file, string folder, MediaType mediaType)
        {
            switch (mediaType)
            {
                case MediaType.Image:
                    return new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, file.OpenReadStream()),
                        Folder = folder,
                        UseFilename = true,
                        UniqueFilename = true,
                        Transformation = new Transformation().Width(300).Height(300).Crop("fill")
                    };
                case MediaType.Video:
                    return new VideoUploadParams
                    {
                        File = new FileDescription(file.FileName, file.OpenReadStream()),
                        Folder = folder,
                        UseFilename = true,
                        UniqueFilename = true,
                        Transformation = new Transformation().Width(640).Height(360).Crop("fill")
                    };
                case MediaType.Audio:
                    return new RawUploadParams
                    {
                        File = new FileDescription(file.FileName, file.OpenReadStream()),
                        Folder = folder,
                        UseFilename = true,
                        UniqueFilename = true
                    };
                case MediaType.Document:
                    return new RawUploadParams
                    {
                        File = new FileDescription(file.FileName, file.OpenReadStream()),
                        Folder = folder,
                        UseFilename = true,
                        UniqueFilename = true
                    };
                default:
                    throw new ArgumentException($"Unsupported media type: {mediaType}");
            }
        }
        private MediaType DetermineMediaType(string contentType)
        {
            if (contentType.StartsWith("image/"))
                return MediaType.Image;
            if (contentType.StartsWith("video/"))
                return MediaType.Video;
            if (contentType.StartsWith("audio/"))
                return MediaType.Audio;
            return MediaType.Document;
        }
    }
}