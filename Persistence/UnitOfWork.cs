using WebApi.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public IPetRepository Pets { get; }

        public IRaceRepository Races { get; }

        public IImageRepository Images { get; }

        public IAccountRepository Accounts { get; }

        public IMessageRepository Messages { get; }

        public UnitOfWork(DataContext context,
            IPetRepository petRepository,
            IRaceRepository races,
            IImageRepository images,
            IAccountRepository accounts,
            IMessageRepository messages)
        {
            _context = context;
            Pets = petRepository;
            Races = races;
            Images = images;
            Accounts = accounts;
            Messages = messages;
        }

        public bool Complete()
        {
            return (_context.SaveChanges() >= 0);
        }

        public bool Complete(int? accountId)
        {
            _context.EnsureAutoHistory(() => new CustomAutoHistory()
            {
                AccountId = accountId
            });
            return (_context.SaveChanges() >= 0);
        }
    }
}
