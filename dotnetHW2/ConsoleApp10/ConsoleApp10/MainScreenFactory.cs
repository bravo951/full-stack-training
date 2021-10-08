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
                case (int)Options.RoomType:
                    return new ManageRoomType();
                case (int)Options.Room:
                    return new ManageRoom();
                case (int)Options.Service:
                    return new ManageService();
                case (int)Options.Customer:
                    return new ManageCustomer();
                //case (int)Options.Employee:
                //    return new ManageEmployee();

            }
            return null;
        }
    }
}
