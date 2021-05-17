using WebApi.Persistence.Repositories;

namespace WebApi.Persistence
{
    public interface IUnitOfWork
    {
        IPetRepository Pets { get; }
        IRaceRepository Races { get; }
        IImageRepository Images { get; }
        IAccountRepository Accounts { get; }
        IMessageRepository Messages { get; }

        bool Complete(int? accountId);
        bool Complete();
    }
}
