using Microsoft.EntityFrameworkCore;
using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo.Context
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options):base(options)
        {

        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Profile> Profile { get; set; }
    }
}
