using System;
using System.Collections.Generic;
using System.Text;
using dotnetHW1.Data.Respository;
using dotnetHW1.Data.Model;

namespace dotnetHW1
{
    class ManageCustm
    {
        IRepository<Custm> custmRepository;
        public ManageCustm()
        {
            custmRepository = new CustmRepository();
        }
        void AddCustomer()
        {
            Custm c = new Custm();
            Console.Write("Enter Id = ");
            c.Id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter FirstName = ");
            c.FirstName = Console.ReadLine();
            Console.Write("Enter LastName = ");
            c.LastName = Console.ReadLine();

            Console.Write("Enter Mobile = ");
            c.Mobile = Console.ReadLine();
            Console.Write("Enter Email = ");
            c.EmailId = Console.ReadLine();

            Console.Write("Enter City = ");
            c.City = Console.ReadLine();
            Console.Write("Enter State = ");
            c.State = Console.ReadLine();
            if (custmRepository.Insert(c) > 0)
            {
                Console.WriteLine("Customer Added successfully");
            }
            else
            {
                Console.WriteLine("Some err has occured");
            }
        }

        void UpdateCustomer()
        {
            Custm c = new Custm();
            Console.Write("Enter Id = ");
            c.Id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter FirstName = ");
            c.FirstName = Console.ReadLine();
            Console.Write("Enter LastName = ");
            c.LastName = Console.ReadLine();
            if (custmRepository.Update(c) > 0)
            {
                Console.WriteLine("Customer Updated successfully");
            }
            else
            {
                Console.WriteLine("Some err has occured");
            }

        }
        void DeleteCustomer()
        {
            Custm c = new Custm();
            Console.Write("Enter Id = ");
            c.Id = Convert.ToInt32(Console.ReadLine());
            if (custmRepository.Delete(1) > 0)
            {
                Console.WriteLine("Customer Deleted successfully");
            }
            else
            {
                Console.WriteLine("Some err has occured");
            }

        }

        public void Run()
        {
            DeleteCustomer();
        }

    }
}
