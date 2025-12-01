using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinguaBender.Models;

namespace LinguaBender.Services
{
    public interface IUserService
    {
        Task<User?> AuthenticateAsync(string username, string password);
        Task<bool> RegisterAsync(string username, string password);
        Task<User?> GetUserByIdAsync(int id);
        Task UpdateUserAsync(User user);
        Task<User?> GetUserByNameAsync(string name);
        Task<bool> RegisterAsync(string username, string password, int roleId, byte[]? photo);
    }
}
