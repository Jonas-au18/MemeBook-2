using System;
using System.Collections.Generic;
using System.ComponentModel;
using MemeBook.Models;
using MemeBook.Services;

namespace MemeBook
{
    public class DataSeeding
    {
        private CircleService _circle;
        private UserService _user;
        private PostService _post;

        public DataSeeding()
        {
            _circle = new CircleService();
            _user = new UserService();
            _post = new PostService();
        }

        public void seedUsers()
        {
            var m = _user.Get();
            if (m.Count == 0)
            {
                var Users = new List<User>();
                var u = new User()
                {
                    Fullname = "Hans Petersen",
                    Age = 42
                };
                Users.Add(u);
                u = new User()
                {
                    Fullname = "Viggo bjerg af simba",
                    Age = 22
                };
                Users.Add(u);
                u = new User()
                {
                    Fullname = "Jonas Nielsen",
                    Age = 31
                };
                Users.Add(u);
                u = new User()
                {
                    Fullname = "Kappa the king",
                    Age = 9001
                };
                Users.Add(u);
                u = new User()
                {
                    Fullname = "Martin Roland",
                    Age = 20
                };
                Users.Add(u);
                foreach (var i in Users)
                {
                    _user.CreateUser(i);
                }
            }

        }

        public void SeedCircle()
        {
            var myUsers = _user.Get();
            var m = _circle.Get();
            if (m.Count == 0)
            {
                var circles = new List<Circles>();
                var c = new Circles()
                {
                    users = {myUsers[1], myUsers[3]}
                };
                circles.Add(c);
                c = new Circles()
                {
                    users = { myUsers[0], myUsers[1], myUsers[1] }
                };
                circles.Add(c);
                c = new Circles()
                {
                    users = { myUsers[3]}
                };
                circles.Add(c);
                c = new Circles()
                {
                    users = { myUsers[1], myUsers[2], myUsers[3], myUsers[4] }
                };
                circles.Add(c);

                foreach (var i in circles)
                {
                    _circle.CreateCircle(myUsers[1]);
                }


                //string id = _circle.CreateCircle(myUsers[1]);
                //_circle.AddToCircle(myUsers.Find(m => m.User_ID == myUsers[1].User_ID),id);
                //_circle.AddToCircle(myUsers.Find(m => m.User_ID == myUsers[3].User_ID),id);

                //id = _circle.CreateCircle(myUsers[3]);
                //_circle.AddToCircle(myUsers.Find(m => m.User_ID == myUsers[0].User_ID),id);
                //_circle.AddToCircle(myUsers.Find(m => m.User_ID == myUsers[1].User_ID),id);
                //_circle.AddToCircle(myUsers.Find(m => m.User_ID == myUsers[2].User_ID),id);

                //id = _circle.CreateCircle(myUsers[3]);

                //id = _circle.CreateCircle(myUsers[0]);
                //_circle.AddToCircle(myUsers.Find(m => m.User_ID == myUsers[1].User_ID),id);
                //_circle.AddToCircle(myUsers.Find(m => m.User_ID == myUsers[2].User_ID),id);
                //_circle.AddToCircle(myUsers.Find(m => m.User_ID == myUsers[3].User_ID),id);
            }
        }

        public void SeedPosts()
        {
            var myUsers = _user.Get();
            var myCircles = _circle.Get();
            var m = _post.Get();
            var myPost = new List<Post>();
            if (m.Count == 0)
            {
                var p = new Post()
                {
                    Owner_ID = myUsers[1].User_ID,
                    Circle = myCircles[0].Circle_ID,
                    Content = "#kimjungundead"
                };
                myPost.Add(p);
                p = new Post()
                {
                    Owner_ID = myUsers[1].User_ID,
                    Circles = myCircles[0],
                    Content = "OH MY GOD!"
                };
                myPost.Add(p);
                p = new Post()
                {
                    Owner_ID = myUsers[1].User_ID,
                    Circles = myCircles[1],
                    Content = "Son of a *****!"
                };
                myPost.Add(p);

                p = new Post()
                {
                    Owner_ID = myUsers[2].User_ID,
                    Circles = myCircles[0],
                    Content = "Are we actually even real?"
                };
                myPost.Add(p);
                p = new Post()
                {
                    Owner_ID = myUsers[2].User_ID,
                    Circles = myCircles[1],
                    Content = "Does this game never end?"
                };
                myPost.Add(p);

                p = new Post()
                {
                    Owner_ID = myUsers[3].User_ID,
                    Circles = myCircles[2],
                    Content = "Where are my friends?"
                };
                myPost.Add(p);
                p = new Post()
                {
                    Owner_ID = myUsers[3].User_ID,
                    Circles = myCircles[2],
                    Content = "Am i ever gonna grow my mustache?"
                };
                myPost.Add(p);
                foreach (var i in myPost)
                {
                    _post.CreatePost(i.Owner_ID,i.Content,i.Circles);
                }

            }
        }
    }
}