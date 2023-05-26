using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TatvaBook.Entities.Data;
using TatvaBook.Entities.Models;
using TatvaBook.Repository.Interface;

namespace TatvaBook.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly TatvaBookContext _tatvaBookContext;

        public LoginRepository(TatvaBookContext TatvaBookContext)
        {
            _tatvaBookContext = TatvaBookContext;
        }
        public TatvaBookUser GetUserEmail(string Email)
        {
            return _tatvaBookContext.TatvaBookUsers.FirstOrDefault(u => u.Email == Email);
        }

    }
}
