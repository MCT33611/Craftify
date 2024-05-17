using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Craftify.Application.Service.Commands.UpdateService
{
    public class UpdateServiceCommandHandler(
        IUnitOfWork _unitOfWork
        ) : IRequestHandler<UpdateServiceCommand, ErrorOr<Unit>>
    {
        public async Task<ErrorOr<Unit>> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var service = _unitOfWork.Service.Get(s => s.Id == request.Id);
            if (service != null)
            {
                service.Title = request.Title ?? service.Title;
                service.Description = request.Description ?? service.Description;
                service.Category = request.Category ?? service.Category;
                service.Price = request.Price ?? service.Price;
                service.Availability = request.Availability;
                service.ZipCode = request.ZipCode ?? service.ZipCode;

                _unitOfWork.Service.Update(service); // Assuming you have an Update method in your repository
                _unitOfWork.Save(); // Save changes to the database
            }
            else
            {
                return Errors.User.InvaildCredetial;
            }
            return Unit.Value;
        }
    }
}
