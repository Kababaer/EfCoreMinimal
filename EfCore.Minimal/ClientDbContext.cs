using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Minimal
{
    public class ClientDbContext : DbContext
    {
        public ClientDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Document> Documents { get; set; }
    }
}
