using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<string> FindUsers(Circles circles)
        {
            var users = _Circle.Find(m => m.Circle_ID == circles.Circle_ID).FirstOrDefault();
            return users.users;
        }

        public Circles FindByID(string id)
        {
            return _Circle.Find(m => m.Circle_ID == id).FirstOrDefault();
        }

        public List<Circles> FindByUser(string _user)
        {
            var c = _Circle.Find(m => m.users.Contains(_user)).ToList();
            List<Circles> myCircles = new List<Circles>();
            foreach (var i in c)
            {
                    myCircles.Add(i);
            }
            Console.WriteLine(myCircles.Count);
            return myCircles;
        }

        public string CreateCircle(string user,string name)
        {
            Circles myCircles = new Circles()
            {
                users = new List<string>
                {
                    user
                },
                name = name
            };
            _Circle.InsertOne(myCircles);
            var id = _Circle.Find(m => m.Circle_ID == myCircles.Circle_ID).FirstOrDefault();
            return id.Circle_ID;
        }

        public void DeleteCircle(string id)
        {
            _Circle.DeleteOne(m => m.Circle_ID == id);
        }

        public void AddToCircle(string user, string id)
        {
            var myCircle = _Circle.Find(m => m.Circle_ID == id).FirstOrDefault();
            myCircle.users.Add(user);
            _Circle.ReplaceOne(m => m.Circle_ID == id, myCircle);
        }

        public void RemoveFromCircle(string user, string id)
        {
            var myCircle = _Circle.Find(m => m.Circle_ID == id).FirstOrDefault();
            myCircle.users.Remove(user);
            _Circle.ReplaceOne(m => m.Circle_ID == id, myCircle);
        }

    }
}
