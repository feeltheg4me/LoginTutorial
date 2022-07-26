using LoginTutorial.DAO;
using LoginTutorial.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(InitDB))]
namespace LoginTutorial.DAO
{
    public class InitDB
    {
        public List<User> users;
        public SQLiteAsyncConnection _database;
        public static bool dbInitialized { get; set; }

        public InitDB()
        { 
        }

        public void initDataBase()
        {
            try
            {
                if (!dbInitialized)
                {
                    _database = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "User.db3"));
                    _database.CreateTableAsync<User>().Wait();
                    dbInitialized = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        

    }
}
