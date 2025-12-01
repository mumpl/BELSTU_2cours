using Microsoft.EntityFrameworkCore;
using LinguaBender.Data;
using LinguaBender.Models;
using Microsoft.Extensions.DependencyInjection;

namespace LinguaBender.Services
{
    public class UserService : IUserService
    {
        private readonly IServiceProvider _provider;

        public UserService(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            using var db = _provider.GetRequiredService<AppDbContext>();
            return await db.Users.Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Name == username && u.Password == password);
        }

        public async Task<bool> RegisterAsync(string username, string password)
        {
            using var db = _provider.GetRequiredService<AppDbContext>();
            if (await db.Users.AnyAsync(u => u.Name == username)) return false;

            var role = await db.Roles.FirstOrDefaultAsync(r => r.RoleName == "user");

            db.Users.Add(new User
            {
                Name = username,
                Password = password,
                RoleId = role?.RoleId ?? 2
            });

            await db.SaveChangesAsync();
            return true;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            using var db = _provider.GetRequiredService<AppDbContext>();
            return await db.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task UpdateUserAsync(User user)
        {
            using var db = _provider.GetRequiredService<AppDbContext>();
            db.Users.Update(user);
            await db.SaveChangesAsync();
        }
        public async Task<bool> RegisterAsync(string username, string password, int roleId, byte[]? photo)
        {
            using var db = _provider.GetRequiredService<AppDbContext>();
            if (await db.Users.AnyAsync(u => u.Name == username))
                return false;

            db.Users.Add(new User
            {
                Name = username,
                Password = password,
                RoleId = roleId,
                Photo = photo
            });

            await db.SaveChangesAsync();
            return true;
        }

        public async Task<User?> GetUserByNameAsync(string name)
        {
            using var db = _provider.GetRequiredService<AppDbContext>();
            return await db.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Name == name);
        }


    }
}
