using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Category.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler(
        IUnitOfWork _unitOfWork
        ) : IRequestHandler<DeleteCategoryCommand, ErrorOr<Unit>>
    {
        public async Task<ErrorOr<Unit>> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            if (_unitOfWork.Category.Get(s => s.Id == command.Id) is not Domain.Entities.Category category)
            {
                return Errors.User.InvaildCredetial;
            }
            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
