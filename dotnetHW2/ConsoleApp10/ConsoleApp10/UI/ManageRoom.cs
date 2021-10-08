using System;
using System.Collections.Generic;
using System.Text;
using Data.Repository;
using Data.Model;

namespace ConsoleApp10.UI
{
    class ManageRoom : MainScreen
    {
        IRepository<Room> rm;
        public ManageRoom()
        {
            rm = new RoomRespository();
        }

        public override void Run()
        {
            int choice = 0;
            do
            {
                Console.Clear();
                Menu m = new Menu();
                choice = m.Print(typeof(Operations));
                switch (choice)
                {
                    case (int)Operations.Add:
                        Room room = new Room();
                        Console.Write("Enter Room Type Code => ");
                        room.RTCODE = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Room Status => ");
                        room.STATUS = Convert.ToInt32(Console.ReadLine());
                        if (rm.Insert(room) > 0)
                            Console.WriteLine("Room added successfully");
                        else
                            Console.WriteLine("Some error has occurred");
                        break;
                    case (int)Operations.Update:
                        Room rr = new Room();
                        Console.Write("Enter Id => ");
                        rr.Id = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Room Type Code => ");
                        rr.RTCODE = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Status => ");
                        rr.STATUS = Convert.ToInt32(Console.ReadLine());
                        if (rm.Update(rr) > 0)
                            Console.WriteLine("Room updated successfully");
                        else
                            Console.WriteLine("Some error has occurred");
                        break;
                    case (int)Operations.Delete:
                        Console.Write("Enter Id => ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        if (rm.Delete(id) > 0)
                            Console.WriteLine("Room Type deleted successfully");
                        else
                            Console.WriteLine("Some error has occurred");
                        break;
                    case (int)Operations.Print:

                        IEnumerable<Room> Rm_List = rm.GetAll();
                        foreach (var item in Rm_List)
                        {
                            Console.WriteLine(item.Id + "   " + item.RTCODE + '\t' + item.STATUS);
                        }
                        break;
                    case (int)Operations.Exit:
                        Console.WriteLine("Thanks for visit!!!!");
                        break;
                    default:
                        Console.WriteLine("Invalid Option");
                        break;
                }
                Console.WriteLine("Press Enter to continue.....");
                Console.ReadLine();
            } while (choice != (int)Operations.Exit);
        }
    }
}
