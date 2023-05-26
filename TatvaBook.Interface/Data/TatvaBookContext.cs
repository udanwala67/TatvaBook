using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TatvaBook.Entities.Models;

namespace TatvaBook.Entities.Data
{
    public partial class TatvaBookContext : IdentityDbContext
    {

        public TatvaBookContext() 
        {

        }
        public TatvaBookContext(DbContextOptions<TatvaBookContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TatvaBookUser> TatvaBookUsers { get; set; } = null!;

      
    }
}

       