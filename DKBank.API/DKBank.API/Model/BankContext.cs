using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DKBank.API.Model
{
    public class BankContext : DbContext

    {

        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {

        }
        public DbSet<Customers> Customers { get; set; }
    }
}
