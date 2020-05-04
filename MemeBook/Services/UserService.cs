﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using MemeBook.Models;
using MongoDB.Driver;

namespace MemeBook.Services
{
    class UserService
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

        public List<string> GetPostById(string id)
        {
            var user = _users.Find(m => m.User_ID == id).FirstOrDefault();

            List<string> ids = new List<string>();
            ids = user.Post_ID;
            return ids;
        }

        public List<string> SearchByName(string name)
        {   
            List<string> names = new List<string>();
            var users = _users.Find(m => true).ToList();
            foreach (var i in users)
            {
                
            }
        }

        public void CreateUser(User user)
        {
            _users.InsertOne(user);
        }

        public void DeleteUser(string id)
        {
            _users.DeleteOne(m => m.User_ID == id);
        }

        public void KillAllUsers()
        {
            _users.DeleteMany(m => m.User_ID != null);
        }

        public void UpdateUser(User user)
        {
            _users.ReplaceOne(m => m.User_ID == user.User_ID, user);
        }
    }
}
