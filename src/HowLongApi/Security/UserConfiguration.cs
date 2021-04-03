using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HowLongApi.Security
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(new User {
                UserName = "admin",
                Email = "admin@example.org",
                PasswordHash = "AQAAAAEAACcQAAAAED0tb8N23CW0B1uLCmdSzL1kfJKD1NqSU6VxzkJ/ATsHW8awVv+bBSmNiACpNR9Iqw==",
            });
        }
    }
}