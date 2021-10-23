using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;
using PODApiDAL.Entities;

namespace PODApiDAL.DataContext
{
    public class DatabaseContext : IdentityDbContext<IdentityUser>
    {
        public class OptionBuild
        {
            public OptionBuild()
            {
                settings = new AppConfiguration();
                opsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
                opsBuilder.UseSqlServer(settings.sqlConnectionString);
                dbOptions = opsBuilder.Options;
            }

            public DbContextOptionsBuilder<DatabaseContext> opsBuilder { get; set; }
            public DbContextOptions<DatabaseContext> dbOptions { get; set; }
            private AppConfiguration settings { get; set; }
        }
        public static OptionBuild ops = new OptionBuild();
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public DbSet<IdentityUser> ApplicaionUsers {get;set;}
        public DbSet<UserProfile> UserProfiles { get; set;  }
        public DbSet<UserFollowerMapping> UserFollowerMappings {  get; set;  }
        public DbSet<UserFollowingMapping> UserFollowingMappings { get; set;  }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<PostComment> PostComments {  get; set; }
        public DbSet<PostLike> PostLikes { get; set; }
    }
}
