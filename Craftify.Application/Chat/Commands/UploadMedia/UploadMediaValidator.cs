using Craftify.Application.Chat.Commands.SendMessage;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Chat.Commands.UploadMedia
{
    internal class UploadMediaValidator: AbstractValidator<UploadMediaCommand>
    {
        public UploadMediaValidator()
        {
            
            RuleFor(x => x.MediaFiles).NotEmpty().WithMessage("media must not be emty");
        }
    }
}
