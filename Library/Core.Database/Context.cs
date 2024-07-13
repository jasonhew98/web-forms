using Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Core.Database
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
