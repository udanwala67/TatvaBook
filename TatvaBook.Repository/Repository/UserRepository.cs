using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatvaBook.Entities.Data;
using TatvaBook.Entities.Models;
using TatvaBook.Repository.Interface;

namespace TatvaBook.Repository.Repository
{
    public class UserRepository :IUserRepository
    {
        private readonly TatvaBookContext _tatvaBookContext;

        public UserRepository(TatvaBookContext tatvaBookContext)
        {
            _tatvaBookContext = tatvaBookContext;
        }

   

    }
}
