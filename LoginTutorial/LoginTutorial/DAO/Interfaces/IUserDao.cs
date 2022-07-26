using LoginTutorial.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoginTutorial.DAO.Interfaces
{
    public interface IUserDao
    {
        Task<bool> AddUserAsync(User user);
        Task<bool> AddUsersAsync(List<User> users);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
        Task<User> GetUserAsync(int id);
        Task<List<User>> GetUsersAsync();
        Task<bool> DeleteUsersAsync();
    }
}
