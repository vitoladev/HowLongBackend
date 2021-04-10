using HowLongApi.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static System.Environment;

namespace HowLongApi.Infrastructure.Data
{
    public class AuthDbContext : IdentityDbContext<User>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var hasher = new PasswordHasher<User>();
            var myEmail = GetEnvironmentVariable("email");
            var myPassword = GetEnvironmentVariable("password");
            builder.Entity<User>().HasData(
                new User
                {
                    Id = "1", // primary key
                    UserName = myEmail,
                    NormalizedUserName = myEmail,
                    PasswordHash = hasher.HashPassword(null, myPassword),
                }
            );
            builder.ApplyConfiguration<User>(new UserConfiguration());

        }
    }
}