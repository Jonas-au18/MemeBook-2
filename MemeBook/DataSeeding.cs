using System;
using System.Collections.Generic;
using System.ComponentModel;
using MemeBook.Models;
using MemeBook.Services;

namespace MemeBook
{
    public class DataSeeding
    {
        private static CircleService _circle;
        private static UserService _user;
        private static PostService _post;
        private static LoginService _login;

        public static void seed()
        {
            _circle = new CircleService();
            _user = new UserService();
            _post = new PostService(_user);
            _login = new LoginService();
            seedUsers();
            SeedCircle();
            SeedPosts();
            SeedLogins();
            massLogout(); //Seeing server is reset we don't want users to be logged in.
        }

        public static void seedUsers()
        {
            var m = _user.Get();
            if (m.Count == 0)
            {
                var Users = new List<User>();
                var u = new User()
                {
                    Fullname = "Hans Petersen",
                    Age = 42,
                    PersonalCircle = new Circles(),
                    gender = 'M',
                };
                Users.Add(u);
                u = new User()
                {
                    Fullname = "Viggo bjerg af simba",
                    Age = 22,
                    PersonalCircle = new Circles(),
                    gender = 'M'
                };
                Users.Add(u);
                u = new User()
                {
                    Fullname = "Jonas Nielsen",
                    Age = 31,
                    PersonalCircle = new Circles(),
                    gender = 'M'
                };
                Users.Add(u);
                u = new User()
                {
                    Fullname = "Kappa the king",
                    Age = 9001,
                    PersonalCircle = new Circles(),
                    gender = 'F'
                };
                Users.Add(u);
                u = new User()
                {
                    Fullname = "Martin Roland",
                    Age = 20,
                    PersonalCircle = new Circles(),
                    gender = 'M'
                };
                Users.Add(u);
                u = new User()
                {
                    Fullname = "Jonas Hansen",
                    Age = 54,
                    PersonalCircle = new Circles(),
                    gender = 'M'
                };
                Users.Add(u);
                foreach (var i in Users)
                {
                    _user.CreateUser(i);
                    _circle.CreateCircle(i.PersonalCircle);
                    _circle.AddToCircle(i.User_ID,i.PersonalCircle.Circle_ID);
                    _user.UpdateUser(i);
                }
            }

        }

        public static void SeedLogins()
        {
            var myUsers = _user.Get();
            var l = _login.Get();
            if (l.Count == 0)
            {
                List<Login> logins = new List<Login>();
                var log = new Login()
                {
                    Username = "Hans",
                    Password = "SomethingCool4",
                    User_id = myUsers[0].User_ID,
                };
                logins.Add(log);
                log = new Login()
                {
                    Username = "Victor",
                    Password = "Rip402",
                    User_id = myUsers[1].User_ID
                };
                logins.Add(log);
                log = new Login()
                {
                    Username = "Jonas",
                    Password = "Password1",
                    User_id = myUsers[2].User_ID,
                };
                logins.Add(log);
                log = new Login()
                {
                    Username = "Kappa",
                    Password = "Test1",
                    User_id = myUsers[3].User_ID,
                };
                logins.Add(log);
                log = new Login()
                {
                    Username = "Roland",
                    Password = "MemeMasterFlex",
                    User_id = myUsers[4].User_ID
                };
                logins.Add(log);
                log = new Login()
                {
                    Username = "HrHansen",
                    Password = "Jonas42",
                    User_id = myUsers[5].User_ID
                };
                logins.Add(log);
                foreach (var i in logins)
                {
                    _login.CreateUser(i.Username,i.Password,i.User_id);
                }
            }
        }

        public static void SeedCircle()
        {
            var myUsers = _user.Get();
            var m = _circle.Get();
            if (m.Count == myUsers.Count)
            {
                var circles = new List<Circles>();
                var c = new Circles()
                {
                    users = new List<string>(){myUsers[1].User_ID, myUsers[3].User_ID},
                    name = "Wow for beginners"
                };
                circles.Add(c);
                c = new Circles()
                {
                    users = new List<string>() { myUsers[5].User_ID, myUsers[1].User_ID, myUsers[2].User_ID },
                    name = "What you got!"
                };
                circles.Add(c);
                c = new Circles()
                {
                    users = new List<string>() { myUsers[3].User_ID},
                    name = "Cool place"
                };
                circles.Add(c);
                c = new Circles()
                {
                    users = new List<string>() { myUsers[1].User_ID, myUsers[2].User_ID, myUsers[3].User_ID, myUsers[4].User_ID },
                    name = "Secret order"
                };
                circles.Add(c);
                c = new Circles()
                {
                    users = new List<string>(),
                    name = "Private chat",
                    AllowedUser = new List<string>() {myUsers[2].User_ID, myUsers[4].User_ID},
                    isPrivate = true
                };
                circles.Add(c);

                foreach (var i in circles)
                {
                    _circle.CreateCircle(i);
                }

            }
        }

        public static void SeedPosts()
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
                    Circle_ID = myCircles[myUsers.Count].Circle_ID,
                    Content = "#kimjungundead"
                };
                myPost.Add(p);
                p = new Post()
                {
                    Owner_ID = myUsers[1].User_ID,
                    Circle_ID = myCircles[myUsers.Count].Circle_ID,
                    Content = "OH MY GOD!"
                };
                myPost.Add(p);
                p = new Post()
                {
                    Owner_ID = myUsers[1].User_ID,
                    Circle_ID = myCircles[myUsers.Count + 1].Circle_ID,
                    Content = "Son of a *****!"
                };
                myPost.Add(p);

                p = new Post()
                {
                    Owner_ID = myUsers[2].User_ID,
                    Circle_ID = myCircles[myUsers.Count].Circle_ID,
                    Content = "Are we actually even real?"
                };
                myPost.Add(p);
                p = new Post()
                {
                    Owner_ID = myUsers[2].User_ID,
                    Circle_ID = myCircles[myUsers.Count + 1].Circle_ID,
                    Content = "Does this game never end?"
                };
                myPost.Add(p);

                p = new Post()
                {
                    Owner_ID = myUsers[3].User_ID,
                    Circle_ID = myCircles[myUsers.Count + 3].Circle_ID,
                    Content = "Where are my friends?"
                };
                myPost.Add(p);
                p = new Post()
                {
                    Owner_ID = myUsers[2].User_ID,
                    Circle_ID = myUsers[2].PersonalCircle.Circle_ID,
                    Content = "This print only has the purpose of "
                };
                myPost.Add(p);
                p = new Post()
                {
                    Owner_ID = myUsers[2].User_ID,
                    Circle_ID =  myUsers[2].PersonalCircle.Circle_ID,
                    Content = "showing that my feed"
                };
                myPost.Add(p);
                p = new Post()
                {
                    Owner_ID = myUsers[2].User_ID,
                    Circle_ID =  myUsers[2].PersonalCircle.Circle_ID,
                    Content = "is bound within a forever alone circle"
                };
                myPost.Add(p);
                p = new Post()
                {
                    Owner_ID = myUsers[3].User_ID,
                    Circle_ID =  myUsers[3].PersonalCircle.Circle_ID,
                    Content = "cus why not?"
                };
                myPost.Add(p);
                p = new Post()
                {
                    Owner_ID = myUsers[3].User_ID,
                    Circle_ID =  myUsers[3].PersonalCircle.Circle_ID,
                    Content = "Test Test Test Test Test Test Test Test"
                };
                myPost.Add(p);
                foreach (var i in myPost)
                {
                    _post.CreatePost(i);
                }

            }
        }

        public static void massLogout()
        {
            var users = _user.Get();
            foreach (var i in users)
            {
                i.Logged_in = false;
                _user.UpdateUser(i);
            }
        }
    }
}