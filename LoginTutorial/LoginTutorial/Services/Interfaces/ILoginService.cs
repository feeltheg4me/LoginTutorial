using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoginTutorial.Services.Interfaces
{
    public interface ILoginService
    {
        Task<bool> IsLogedinAsync(string username, string password);
        
    }
}
