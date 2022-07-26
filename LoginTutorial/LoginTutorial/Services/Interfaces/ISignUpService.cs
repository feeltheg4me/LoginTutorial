using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoginTutorial.Services.Interfaces
{
    public interface ISignUpService
    {
        Task<bool> IsSignedUpAsync(string username, string password, string email, string phone);
    }
}
