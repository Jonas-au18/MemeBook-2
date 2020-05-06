using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using MemeBook.Models;
using MongoDB.Driver;

namespace MemeBook.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("MemeBook");
            _users = database.GetCollection<User>("User");
        }

        public User GetById(string id)
        {
            return _users.Find(m => m.User_ID == id).FirstOrDefault();
        }

        public string GetByName(string name)
        {
            var user = _users.Find(m => m.Fullname == name).FirstOrDefault();
            return user.User_ID;
        }

        public List<User> Get()
        {
            return _users.Find(m => true).ToList();
        }

        public List<string> SearchByName(string name)
        {   
            List<string> names = new List<string>();
            var users = _users.Find(m => true).ToList();
            foreach (var i in users)
            {
                if (i.Fullname.Contains(name))
                {
                    names.Add(i.User_ID);
                }
            }
            return names;
        }

        public void CreateUser(User user)
        {
            _users.InsertOne(user);
        }

        public void DeleteUser(string id)
        {
            _users.DeleteOne(m => m.User_ID == id);
        }

        public void UpdateUser(User user)
        {
            _users.ReplaceOne(m => m.User_ID == user.User_ID, user);
        }

        public void followUser(string user, string toFollow)
        {
            var _user = _users.Find(m => m.User_ID == user).FirstOrDefault();
            _user.Following.Add(toFollow);
            _users.ReplaceOne(m => m.User_ID == user, _user);
        }

        public void unfollowUser(string me, string toUnfollow)
        {
            var _user = _users.Find(m => m.User_ID == me).FirstOrDefault();
            _user.Following.Remove(toUnfollow);
            _users.ReplaceOne(m => m.User_ID == me, _user);
        }

        public void blockUser(string user, string toBlock)
        {
            var _user = _users.Find(m => m.User_ID == user).FirstOrDefault();
            _user.Blocked.Add(toBlock);
            _users.ReplaceOne(m => m.User_ID == user, _user);
        }
    }
}
