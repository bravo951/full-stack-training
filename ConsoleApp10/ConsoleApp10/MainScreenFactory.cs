using System;
using System.Collections.Generic;
using System.Text;
using ConsoleApp10.UI;

namespace ConsoleApp10
{
    class MainScreenFactory
    {
        public MainScreen GetObject(int choice)
        {
            switch (choice)
            {
                // (int)Options.Department:
                //    return new ManageDepartment();
                case (int)Options.Employee:
                    return new ManageEmployee();

            }
            return null;
        }
    }
}
