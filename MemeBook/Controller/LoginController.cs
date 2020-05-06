using System;
using System.Security.Cryptography.X509Certificates;
using MemeBook.Models;
using MemeBook.Services;

namespace MemeBook.Controller
{
    public class LoginController
    {
        private LayoutControl _layout;
        private LoginService _lService;
        private UserService _uService;

        public LoginController(LayoutControl layout, LoginService lServ, UserService uServ)
        {
            _layout = layout;
            _lService = lServ;
            _uService = uServ;
        }

        public User Login()
        {
            _layout.boxLine();
            Console.WriteLine("Enter username");
            _layout.boxLine();
            var refid = _lService.GetByName(Console.ReadLine());
            if (refid != null)
            {
                _layout.boxLine();
                Console.WriteLine("Enter password");
                _layout.boxLine();
                bool valid = _lService.validate(Console.ReadLine(), refid);
                if (valid)
                {
                    var _user = _uService.GetById(refid);
                    _layout.boxLine();
                    Console.WriteLine("Welcome " + _user.Fullname);
                    _layout.boxLine();
                    _user.Logged_in = true;
                    _uService.UpdateUser(_user);

                    return _user;
                }
                else
                {
                    Console.WriteLine("Wrong password");
                    return null;
                }
            }
            
            Console.WriteLine("User doesn't exit");
            return null;
        }

        public User logout(User _user)
        {
            _user.Logged_in = false;
            _uService.UpdateUser(_user);
            return null;
        }
    }
}