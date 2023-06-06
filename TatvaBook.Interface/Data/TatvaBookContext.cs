using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TatvaBook.Entities.Models;

namespace TatvaBook.Entities.Data
{
    public partial class TatvaBookContext : IdentityDbContext
    {
        private readonly TatvaBookContext _tatvaBookContext;

        public TatvaBookContext()
        {
           

        }
        public TatvaBookContext(DbContextOptions<TatvaBookContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TatvaBookUser> TatvaBookUsers { get; set; } = null!;
        public virtual DbSet<Story> Stories { get; set; } = null!;
        public virtual DbSet<FriendRequest> FriendRequests { get; set; } = null!;

/*        public List<TatvaBookUser> SearchUsers(string UserName)
        {
            SqlParameter pContactName = new SqlParameter("@Full_Name", UserName);
            var users = _tatvaBookContext.TatvaBookUsers.FromSqlRaw("EXECUTE Friends_SearchFriends @Full_Name", pContactName).AsEnumerable();
            if (users != null)
            {
                users.Select(x => new TatvaBookUser
                {
                    Full_Name = x.Full_Name,
                }).ToList();
            }
            return users.ToList();
        }*/

    }
}

