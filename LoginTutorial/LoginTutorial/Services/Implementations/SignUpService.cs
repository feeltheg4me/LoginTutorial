using LoginTutorial.DAO;
using LoginTutorial.DAO.Interfaces;
using LoginTutorial.Models;
using LoginTutorial.Services.Implementations;
using LoginTutorial.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(SignUpService))]
namespace LoginTutorial.Services.Implementations
{
    public class SignUpService : ISignUpService
    {
        InitDB initDB = DependencyService.Get<InitDB>();
        IUserDao iUserDao = DependencyService.Get<IUserDao>();
        ILoginService loginService = DependencyService.Get<ILoginService>();
        public SignUpService()
        {

        }

        // Check if User is signed up
        public async Task<bool> IsSignedUpAsync(string username, string password, string email, string phone)
        {
            try
            {
                var isLogedin = await loginService.IsLogedinAsync(username, password);
                if(isLogedin)
                {
                    return true;
                }
                
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
