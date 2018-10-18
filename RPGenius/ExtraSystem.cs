using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RPGenius
{
    public static class ExSys   //short for extra system. Contains aditional system-like methods
    {
        public static int ReadInt()
        {
            int result;
            do
            {
                string input = Console.ReadLine();
                try
                {
                    result = Convert.ToInt32(input);
                    return result;
                }
                catch (Exception)
                {
                    Console.WriteLine("Please enter an integer");
                }
            } while (3 != 4);
        }
        public static int ReadIntRange(int rangeStart, int rangeEnd)
        {
            int result;
            do
            {
                string input = Console.ReadLine();
                try
                {
                    result = Convert.ToInt32(input);
                    if(result >= rangeStart && result <= rangeEnd) { return result; }
                    Console.WriteLine("Please enter an integer between {0} and {1}", rangeStart, rangeEnd);
                }
                catch (Exception)
                {
                    Console.WriteLine("Please enter an integer");
                }
            } while (3 != 4);
        }
    }
}
