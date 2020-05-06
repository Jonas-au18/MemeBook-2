using System;
using System.Collections.Generic;
using System.Diagnostics;
using MemeBook.Controller;
using MemeBook.Models;
using MemeBook.Queries;
using MemeBook.Services;

namespace MemeBook
{
    class Program
    {
        private static MemeBookController _control;
        private static LayoutControl _layout;
        static void Main(string[] args)
        {
            DataSeeding.seed();
            _layout = new LayoutControl();
            _control = new MemeBookController(_layout);

            _layout.boxLine();
            Console.WriteLine("Welcome to memebook, the place where consoles meet");
            _layout.boxLine();

            while (true)
            {
                _control.CheckLoginState();
            }
        }
    }
}
