using WebApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace WebApi.Persistence.Repositories
{
    public interface IAccountRepository
    {
        void Add(Account account);
        void Delete(Account account);
        bool ExistsByDisplayName(string displayName);
        bool ExistsByEmail(string email);
        DbSet<Account> GetAll();
        Account GetByEmail(string email);
        Task<Account> GetById(int id);
        Account GetByRefreshToken(string token);
        Account GetByResetToken(string token);
        Account GetByVerificationToken(string token);
        void Update(Account account);
    }
}
