using MediatR;
using Craftify.Application.Common.Interfaces.Service;
using Craftify.Application.Chat.Common;
using Craftify.Domain.Entities;
using Craftify.Domain.Enums;

namespace Craftify.Application.Chat.Commands.UploadMedia
{
    public class UploadMediaCommandHandler : IRequestHandler<UploadMediaCommand, List<MessageMediaResult>>
    {
        private readonly ICloudinaryService _cloudinaryService;

        public UploadMediaCommandHandler(ICloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }

        public async Task<List<MessageMediaResult>> Handle(UploadMediaCommand request, CancellationToken cancellationToken)
        {
            var results = new List<MessageMediaResult>();

            foreach (var file in request.MediaFiles)
            {
                var cdnUrl = await _cloudinaryService.UploadAsync(file, "chat_media");
                var media = new MessageMedia
                {
                    FileName = file.FileName,
                    ContentType = file.ContentType,
                    FileSize = file.Length,
                    CdnUrl = cdnUrl,
                    Type = DetermineMediaType(file.ContentType)
                };

                results.Add(new MessageMediaResult
                {
                    Id = Guid.NewGuid(), // You might want to generate this differently
                    FileName = media.FileName,
                    ContentType = media.ContentType,
                    FileSize = media.FileSize,
                    CdnUrl = media.CdnUrl,
                    Type = media.Type
                });
            }

            return results;
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