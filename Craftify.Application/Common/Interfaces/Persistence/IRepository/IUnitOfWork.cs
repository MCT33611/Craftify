namespace Craftify.Application.Common.Interfaces.Persistence.IRepository
{
    public interface IUnitOfWork
    {
        IUserRepository User {  get; }

        IPlanRepository Plan { get; }

        IWorkerRepository Worker { get; }

        IBookingRepository Booking { get; }

        void Save();
    }
}
