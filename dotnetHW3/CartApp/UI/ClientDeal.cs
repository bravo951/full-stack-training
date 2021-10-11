using System;
using System.Collections.Generic;
using System.Text;
using CartApp.Data.Models;
using CartApp.Services;

namespace CartApp.UI
{
    class ClientDeal : MainScreen
    {
        //public bool p = false;
        public bool p { get; set; }
        public override void run()
        {
            int choice = 0;
            CartService cv = new CartService();
            do
            {
                Console.Clear();
                Order or = new Order();
                Console.Write("Enter Number of Apples => ");
                int a = Convert.ToInt32(Console.ReadLine());
                or.AppleNo = a;
                Console.Write("Enter Number of Oranges => ");
                int o = Convert.ToInt32(Console.ReadLine());
                or.OrangeNo = o;
                decimal total = cv.calculate(a, o, p);
                or.TotalPrice = total;
                Console.Clear();
                Console.WriteLine($"Your Total is => {total}");
                Console.WriteLine("Please Select Pay Now to place your order:");
                Menu m = new Menu();
                choice = m.Print(typeof(Operations));
                if (choice == (int)Operations.ReEnter)
                    continue;
                if (choice == (int)Operations.Exit)
                {
                    cv.quit();
                    break;
                }
                or.OrderDate = DateTime.Now.ToString();
                if(cv.Commit(or, p) > 0)
                {
                    Console.WriteLine("-------------------\nYour Order has been placed!");
                    Console.WriteLine("Recipt:");
                    Console.WriteLine("Apple"+'\t'+"Orange"+'\t'+"TotalPrice"+'\t'+"Date");
                    Console.WriteLine(a + ""+'\t' + o + '\t' + total+ '\t' + or.OrderDate+"\n-------------------------------");

                }
                    
                else
                {
                    Console.WriteLine("Some error has occurred");
                    break;
                }
                Console.WriteLine("Press 1 to continue shopping\nPress 0 to quit");
                choice = Convert.ToInt32(Console.ReadLine());
            } while (choice!=0);
            
        }
    }


}
