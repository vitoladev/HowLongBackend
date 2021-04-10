using System.Threading.Tasks;
using System;
using HowLongApi.Infrastructure.Data;
using HowLongApi.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using static System.Environment;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.AspNetCore.Builder;

namespace HowLongApi.Security
{
    public static class SeedUser
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AuthDbContext>();

                var userName = GetEnvironmentVariable("email");

                var user = new User
                {
                    Email = $"{userName}@email.com",
                    NormalizedEmail = $"{userName}@email.com".ToUpper(),
                    UserName = userName,
                    NormalizedUserName = userName.ToUpper(),
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                if (!context.Users.Any(u => u.UserName == user.UserName))
                {
                    var hasher = new PasswordHasher<User>();
                    var password = GetEnvironmentVariable("password");
                    var hashed = hasher.HashPassword(user, password);
                    user.PasswordHash = hashed;

                    var userStore = new UserStore<User>(context);
                    var result = userStore.CreateAsync(user);
                }

                context.SaveChangesAsync();
            }
        }
    }
}
