using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatvaBook.Entities.Models;

namespace TatvaBook.Repository.Interface
{
    public interface ILoginRepository
    {
        public TatvaBookUser GetUserEmail(string Email);
    }
}