using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catedra1.src.Models;
using Microsoft.EntityFrameworkCore;

namespace Catedra1.src.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Gender> Genders {get; set;}
        public DbSet<User> Users {get; set;}
        public DataContext(DbContextOptions options) : base(options){}
    }
}