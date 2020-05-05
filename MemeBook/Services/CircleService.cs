using System;
using System.Collections.Generic;
using System.Text;
using MemeBook.Models;
using MongoDB.Driver;

namespace MemeBook.Services
{
    public class CircleService
    {
        private readonly IMongoCollection<Circles> _Circle;

        public CircleService()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("MemeBook");
            _Circle = database.GetCollection<Circles>("Circle");
        }

        public List<Circles> Get()
        {
            return _Circle.Find(m => true).ToList();
        }

        public List<User> FindUsers(Circles circles)
        {
            var users = _Circle.Find(m => m.Circle_ID == circles.Circle_ID).FirstOrDefault();
            return users.users;
        }

        public Circles FindByID(string id)
        {
            return _Circle.Find(m => m.Circle_ID == id).FirstOrDefault();
        }

        public List<Circles> FindByUser(User _user)
        {
            List<Circles> myCircles = new List<Circles>();
            var C = Get();
            foreach (var i in C)
            {
                if (i.users.Find(x => x.User_ID == _user.User_ID) != null)
                {
                    myCircles.Add(i);
                }
            }

            return myCircles;
        }

        public string CreateCircle(User user)
        {
            Circles myCircles = new Circles
            {
                users = new List<User>
                {
                    user
                }
            };
            _Circle.InsertOne(myCircles);
            var id = _Circle.Find(m => m.Circle_ID == myCircles.Circle_ID).FirstOrDefault();
            return id.Circle_ID;
        }

        public void DeleteCircle(string id)
        {
            _Circle.DeleteOne(m => m.Circle_ID == id);
        }

        public void AddToCircle(User user, string id)
        {
            var myCircle = _Circle.Find(m => m.Circle_ID == id).FirstOrDefault();
            myCircle.users.Add(user);
            _Circle.ReplaceOne(m => m.Circle_ID == id, myCircle);
        }

        public void RemoveFromCircle(User user, string id)
        {
            var myCircle = _Circle.Find(m => m.Circle_ID == id).FirstOrDefault();
            myCircle.users.Remove(user);
            _Circle.ReplaceOne(m => m.Circle_ID == id, myCircle);
        }

    }
}
