using System;
using System.Collections.Generic;
using System.Text;
using Data.Repository;
using Data.Model;

namespace ConsoleApp10.UI
{
    class ManageCustomer : MainScreen
    {
        IRepository<Customer> CstRepository;
        public ManageCustomer()
        {
            CstRepository = new CustomRepository();
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
                        Customer c = new Customer();
                        Console.Write("Enter Room No. => ");
                        c.ROOMNO = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Customer Name => ");
                        c.CNAME = Console.ReadLine();
                        Console.Write("Enter Home Address => ");
                        c.ADDRESS = Console.ReadLine();
                        Console.Write("Enter Phone Number => ");
                        c.PHONE = Console.ReadLine();
                        Console.Write("Enter Email => ");
                        c.EMAIL = Console.ReadLine();
                        //c.CHECKIN = DateTime.Now.ToString();
                        Console.Write("Enter Checkin Time => ");
                        c.CHECKIN = Console.ReadLine();
                        Console.Write("Enter Total Persons => ");
                        c.TotalPERSONS = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Duration => ");
                        c.BookingDays = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Deposit => ");
                        c.ADVANCE = Convert.ToDecimal(Console.ReadLine());
                        if (CstRepository.Insert(c) > 0)
                            Console.WriteLine("Customer added successfully");
                        else
                            Console.WriteLine("Some error has occurred");
                        break;
                    case (int)Operations.Update:
                        Customer cc = new Customer();
                        Console.Write("Enter Id => ");
                        cc.Id = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Room No. => ");
                        cc.ROOMNO = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Service Description => ");
                        cc.CNAME = Console.ReadLine();
                        Console.Write("Enter Home Address => ");
                        cc.ADDRESS = Console.ReadLine();
                        Console.Write("Enter Phone Number => ");
                        cc.PHONE = Console.ReadLine();
                        Console.Write("Enter Email => ");
                        cc.EMAIL = Console.ReadLine();
                        //c.CHECKIN = DateTime.Now.ToString();
                        Console.Write("Enter Checkin Time => ");
                        cc.CHECKIN = Console.ReadLine();
                        Console.Write("Enter Total Persons => ");
                        cc.TotalPERSONS = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Duration => ");
                        cc.BookingDays = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Deposit => ");
                        cc.ADVANCE = Convert.ToDecimal(Console.ReadLine());
                        if (CstRepository.Update(cc) > 0)
                            Console.WriteLine("Customer updated successfully");
                        else
                            Console.WriteLine("Some error has occurred");
                        break;
                    case (int)Operations.Delete:
                        Console.Write("Enter Id => ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        if (CstRepository.Delete(id) > 0)
                            Console.WriteLine("Customer deleted successfully");
                        else
                            Console.WriteLine("Some error has occurred");
                        break;
                    case (int)Operations.Print:

                        IEnumerable<Customer> Cst_List = CstRepository.GetAll();
                        foreach (var item in Cst_List)
                        {
                            //Console.WriteLine(item.CHECKIN + '\t' + item.TotalPERSONS);
                            Console.WriteLine(item.Id + '\t' + item.ROOMNO + '\t' + item.CNAME+ '\t' + item.ADDRESS + '\t' + item.PHONE
                                + item.EMAIL + '\t' + item.CHECKIN + '\t' + item.TotalPERSONS + '\t' + item.BookingDays
                                + item.ADVANCE);
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
