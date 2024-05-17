namespace Craftify.Application.Common.Interfaces.Persistence.IRepository
{
    public interface IUnitOfWork
    {
        IUserRepository User {  get; }

        IServiceRepository Service { get; }

        ICategoryRepository Category { get; }

        void Save();
    }
}
