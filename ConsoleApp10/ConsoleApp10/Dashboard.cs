using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp10
{
    class Dashboard
    {
        public void ShowDashboard()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Clear();
            Console.Title = "Antra Inc";

            Menu m = new Menu();
            int choice = m.Print(typeof(Options));
            MainScreenFactory factory = new MainScreenFactory();
            MainScreen ms = factory.GetObject(choice);
            if (ms != null)
            {
                ms.Run();
            }

        }
    }
}
