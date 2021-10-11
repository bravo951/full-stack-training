using System;
using System.Collections.Generic;
using System.Text;
using CartApp.Services;
using CartApp.UI;

namespace CartApp
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
            int choice = m.Print(typeof(DISC));
            ClientDeal cd = new ClientDeal();
            if (choice == 1)
                cd.p = true;
            cd.run();
        }
    }
}
