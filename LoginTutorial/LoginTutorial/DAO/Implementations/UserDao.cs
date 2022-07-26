using LoginTutorial.DAO.Implementations;
using LoginTutorial.DAO.Interfaces;
using LoginTutorial.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(UserDao))]
namespace LoginTutorial.DAO.Implementations
{
    public class UserDao : IUserDao
    {
        public readonly InitDB initDB = DependencyService.Get<InitDB>();
       
        public async Task<bool> AddUserAsync(User user)
        {
            try
            {
                if (user.Id > 0)
                {
                    await initDB._database.UpdateAsync(user);
                }
                else
                {
                    await initDB._database.InsertAsync(user);
                }

                return await Task.FromResult(true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> AddUsersAsync(List<User> users)
        {
            try
            {
                await initDB._database.InsertAllAsync(users);
                return await Task.FromResult(true);

            }
            catch (Exception)
            {

                throw;
            };
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                await initDB._database.DeleteAsync<User>(id);
                return await Task.FromResult(true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteUsersAsync()
        {
            try
            {
                await initDB._database.DeleteAllAsync<User>();
                return await Task.FromResult(true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<User> GetUserAsync(int id)
        {
            try
            {
                return await initDB._database.Table<User>().Where(u => u.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<User>> GetUsersAsync()
        {
            try
            {
                return await Task.FromResult(await initDB._database.Table<User>().ToListAsync());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<bool> UpdateUserAsync(User user)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
