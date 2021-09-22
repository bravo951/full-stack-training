using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Sol1
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public int factorial(int a)
        {
            if (a <= 1) return 1;
            return a * factorial(a - 1);
        }
        public bool isPrime(int a)
        {
            if (a < 2) return false;
            int up = (int)Math.Sqrt(a);
            for (int i = 2; i <= up; i++)
            {
                if (a % i == 0) return true;
            }
            return false;
        }
        public bool isLeap(int year)
        {
            return (year - 2008) % 4 == 0;
        }
        public int LCM(int a,int b)
        {
            int c;
            if (a < b)
            {
                int temp = a;
                a = b;
                b = temp;
            }
            c = a % b;
            if (c == 0) return b;
            return LCM(b, c);
        }
    }
}
