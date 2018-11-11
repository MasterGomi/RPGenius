using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RPGenius
{
    /// <summary>
    /// Short for "Extra System". Contains extra, system-like methods
    /// </summary>
    public static class ExSys
    {
        /// <summary>
        /// Reads an integer from the standard input stream
        /// </summary>
        /// <returns>Returns the integer read</returns>
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
                    Console.WriteLine("**Please enter an integer");
                }
            } while (3 != 4);
        }
        /// <summary>
        /// Reads an integer from the standard input stream that fits in the specified range
        /// </summary>
        /// <param name="rangeStart">The lower inclusive bound</param>
        /// <param name="rangeEnd">The upper inclusive bound</param>
        /// <returns>Returns the integer read</returns>
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
                    Console.WriteLine("**Please enter an integer between {0} and {1}", rangeStart, rangeEnd);
                }
                catch (Exception)
                {
                    Console.WriteLine("**Please enter an integer");
                }
            } while (3 != 4);
        }
    }
}
