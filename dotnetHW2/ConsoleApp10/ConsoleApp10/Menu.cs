using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp10
{
    class Menu
    {
        public int Print(Type t)
        {
            string[] names = Enum.GetNames(t);
            int[] values = (int[])Enum.GetValues(t);
            int length = values.Length;
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine($"Print {values[i]} for {names[i]}");
            }
            Console.Write("Enter your choice = ");
            return Convert.ToInt32(Console.ReadLine());
        }
    }
}
