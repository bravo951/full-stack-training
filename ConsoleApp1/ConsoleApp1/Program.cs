using System;

namespace ConsoleApp1
{
    class Program
    {   
        //public void add(int a,int b)
        //{
        //    Console.WriteLine(a + b);
        //}
        static void Main(string[] args)
        {
            Sol1 sol = new Sol1();
            Console.WriteLine(sol.factorial(3));
            Console.WriteLine(sol.isPrime(18));
            Console.WriteLine(sol.isLeap(1996));
            Console.WriteLine(sol.LCM(105,350));
        }
    }
}
