using System;
using System.Collections.Generic;
using System.Text;

namespace MemeBook.Controller
{
    public class LayoutControl
    {
        public void boxLine()
        {
            Console.WriteLine("-------------------------------------------------------------------------------");
        }

        public int Getindex(string msg)
        {
            boxLine();
            Console.WriteLine(msg);
            try
            {
                int index = Convert.ToInt32(Console.ReadLine());
                return index;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}
