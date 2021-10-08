using System;
using System.Collections.Generic;
using System.Text;
using Data.Repository;
using Data.Model;

namespace ConsoleApp10.UI
{
    class ManageService : MainScreen
    {
        IRepository<Services> ServiceRepository;
        public ManageService()
        {
            ServiceRepository = new ServiceRepository();
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
                        Services s = new Services();
                        Console.Write("Enter Room No. => ");
                        s.ROOMNO = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Service Description => ");
                        s.SDESC = Console.ReadLine();
                        Console.Write("Enter Service Charge => ");
                        s.AMOUNT = Convert.ToDecimal(Console.ReadLine());
                        s.ServiceDate = DateTime.Now.ToString();
                        if (ServiceRepository.Insert(s) > 0)
                            Console.WriteLine("Service added successfully");
                        else
                            Console.WriteLine("Some error has occurred");
                        break;
                    case (int)Operations.Update:
                        Services ss = new Services();
                        Console.Write("Enter Id => ");
                        ss.Id = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Room No. => ");
                        ss.ROOMNO = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Service Description => ");
                        ss.SDESC = Console.ReadLine();
                        Console.Write("Enter Service Charge => ");
                        ss.AMOUNT = Convert.ToDecimal(Console.ReadLine());
                        ss.ServiceDate = DateTime.Now.ToString();
                        if (ServiceRepository.Update(ss) > 0)
                            Console.WriteLine("Service updated successfully");
                        else
                            Console.WriteLine("Some error has occurred");
                        break;
                    case (int)Operations.Delete:
                        Console.Write("Enter Id => ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        if (ServiceRepository.Delete(id) > 0)
                            Console.WriteLine("Service deleted successfully");
                        else
                            Console.WriteLine("Some error has occurred");
                        break;
                    case (int)Operations.Print:

                        IEnumerable<Services> Sv_List = ServiceRepository.GetAll();
                        foreach (var item in Sv_List)
                        {
                            Console.WriteLine(item.Id + "   " + item.ROOMNO + '\t' + item.SDESC + '\t' + item.AMOUNT + '\t' + item.ServiceDate);
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
