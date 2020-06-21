using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace WebLocadora.Models
{
    public class ApiDbContext: DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext>options):base(options)
        {

        }
        public DbSet<Cliente> Clientes { get; set; }
        
        public DbSet<Filme> Filmes { get; set; }

        public DbSet<Locacao> Locacoes { get; set; }

        public DbSet<Ticket> Tickets { get; set; }
    }
}
