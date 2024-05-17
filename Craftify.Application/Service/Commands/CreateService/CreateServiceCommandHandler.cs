using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using ErrorOr;
using MediatR;

namespace Craftify.Application.Service.Commands.CreateService
{
    public class CreateServiceCommandHandler(
        IUnitOfWork _unitOfWork
        ) : IRequestHandler<CreateServiceCommand, ErrorOr<Guid>>
    {
        public async Task<ErrorOr<Guid>> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            Domain.Entities.Service service = new ()
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                CategoryId = request.CategoryId,
                Price = request.Price,
                Availability = request.Availability,
                ZipCode = request.ZipCode
            };
            _unitOfWork.Service.Add( service );
            _unitOfWork.Save();
            return service.Id;
        }
    }
}
