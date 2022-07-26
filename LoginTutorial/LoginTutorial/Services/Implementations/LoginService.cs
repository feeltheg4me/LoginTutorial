using LoginTutorial.DAO;
using LoginTutorial.Models;
using LoginTutorial.Services.Implementations;
using LoginTutorial.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(LoginService))]
namespace LoginTutorial.Services.Implementations
{
    public class LoginService : ILoginService
    {
        InitDB initDB = DependencyService.Get<InitDB>();
        public LoginService()
        {
            

        }

        // Check login credentials
        public async Task<bool> IsLogedinAsync(string username, string password)
        {
            try
            {
                var searchUser = initDB._database.Table<User>().Where(x => x.Password.Equals(password) && x.UserName.Equals(username.ToLower())).ToListAsync();
                var resultCount = searchUser.Result.Count();
                if (resultCount>0)
                    return true;
                
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        
    }
}
