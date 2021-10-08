using System;
using System.Collections.Generic;
using System.Text;
using Data.Repository;
using Data.Model;
namespace ConsoleApp10.UI
{
    class ManageRoomType: MainScreen
    {
        IRepository<RoomType> RT_Repository;
        public ManageRoomType()
        {
            RT_Repository = new RoomTypeRespository();
        }

        public override void Run()
        {
            //throw new NotImplementedException();
            int choice = 0;
            do
            {
                Console.Clear();
                Menu m = new Menu();
                choice = m.Print(typeof(Operations));
                switch (choice)
                {
                    case (int)Operations.Add:
                        RoomType r = new RoomType();
                        //Console.Write("Enter Id => ");
                        //r.Id = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Room Type Description(Deluxe/Super Deluxe) => ");
                        r.RTDESC = Console.ReadLine();
                        Console.Write("Enter Rent => ");
                        r.Rent = Convert.ToDecimal(Console.ReadLine());
                        if (RT_Repository.Insert(r) > 0)
                            Console.WriteLine("Room Type added successfully");
                        else
                            Console.WriteLine("Some error has occurred");
                        break;
                    case (int)Operations.Update:
                        RoomType rr = new RoomType();
                        Console.Write("Enter Id => ");
                        rr.Id = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Room Type Description(Deluxe/Super Deluxe) => ");
                        rr.RTDESC = Console.ReadLine();
                        Console.Write("Enter Rent => ");
                        rr.Rent = Convert.ToDecimal(Console.ReadLine());
                        if (RT_Repository.Update(rr) > 0)
                            Console.WriteLine("Room Type updated successfully");
                        else
                            Console.WriteLine("Some error has occurred");
                        break;
                    case (int)Operations.Delete:
                        Console.Write("Enter Id => ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        if (RT_Repository.Delete(id) > 0)
                            Console.WriteLine("Room Type deleted successfully");
                        else
                            Console.WriteLine("Some error has occurred");
                        break;
                    case (int)Operations.Print:

                        IEnumerable<RoomType> RT_List = RT_Repository.GetAll();
                        foreach (var item in RT_List)
                        {
                            Console.WriteLine(item.Id + '\t' + item.RTDESC + '\t' + item.Rent);
                        }
                        //PrintAllEmployee();

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
