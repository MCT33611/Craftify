namespace Craftify.Application.Common.Interfaces.Persistence.IRepository
{
    public interface IUnitOfWork
    {
        IUserRepository User {  get; }

        IPlanRepository Plan { get; }

        IChatRepository Chat { get; }


        IWorkerRepository Worker { get; }

        IBookingRepository Booking { get; }

        Task Save();
    }
}
