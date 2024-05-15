namespace Craftify.Application.Common.Interfaces.Persistence.IRepository
{
    public interface IUnitOfWork
    {
        IUserRepository User {  get; }

        void Save();
    }
}
