using FluentValidation;

namespace Craftify.Application.Profile.Queries.GetWorker
{
    public class GetWorkerQueryValidator : AbstractValidator<GetWorkerQuery>
    {
        public GetWorkerQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }

    }
}
