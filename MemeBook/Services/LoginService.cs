using System.Collections.Generic;
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

        public List<Login> Get()
        {
            return _login.Find(m => true).ToList();
        }

        public string GetByName(string username)
        {
            var user = _login.Find(m=>m.Username == username).FirstOrDefault(); 
            return user.User_id;
        }

        public bool CheckUser(string username)
        {
            var user = _login.Find(m => m.Username == username).FirstOrDefault();
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public bool validate(string password, string userid)
        {
            var isValid = _login.Find(m => m.User_id == userid).FirstOrDefault();
            if (isValid.Password == password)
            {
                return true;
            }
            return false;
        }

        public void CreateUser(string username, string password, string user)
        {
            Login myLogin = new Login()
            {
                Username = username,
                Password = password,
                User_id = user
            };
            _login.InsertOne(myLogin);
        }
    }
}