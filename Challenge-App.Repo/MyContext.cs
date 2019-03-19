using Challenge_App.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge_App.Repo
{
    public class MyContext : DbContext
    {

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Challenge> Challenge { get; set; }
        public DbSet<ChallengeItem> ChallengeItem { get; set; }
        public DbSet<ChallengeItemUsers> ChallengeItemUsers { get; set; }
        public DbSet<ChallengeUsers> ChallengeUsers { get; set; }


    
    }
}
