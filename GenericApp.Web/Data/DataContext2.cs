﻿using GenericApp.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GenericApp.Web.Data
{
    public class DataContext2 : IdentityDbContext<User>
    {
        public DataContext2(DbContextOptions<DataContext2> options) : base(options)
        {
        }
        public DbSet<Causante> Causantes { get; set; }
    }
}