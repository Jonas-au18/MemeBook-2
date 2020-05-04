using MemeBook.Models;
using MongoDB.Driver;

namespace MemeBook.Services
{
    public class LoginService
    {
        private readonly IMongoCollection<Login> _login;

        public LoginService()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("MemeBook");
            _login = database.GetCollection<Login>("login");
        }

        public string GetByName(string username)
        {
            return "";
        }

        public bool CheckUser(string username)
        {
            return true;
        }

        public bool validate(string password, string userid)
        {
            return true;
        }

        public void CreateUser(string username, string password, User user)
        {

        }
    }
}