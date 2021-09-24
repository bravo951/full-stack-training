using System;
using System.Collections.Generic;
using System.Text;
using Data.Model;
using Data.Repository;
namespace ConsoleApp10.UI
{
    class ManageEmployee: MainScreen
    {
        IRepository<Employee> empRepository;
        public ManageEmployee()
        {
            empRepository = new EmployeeRepository();
        }
        void AddEmployee()
        {
            Employee e = new Employee();
            Console.Write("Enter Id => ");
            e.Id= Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Name => ");
            e.Name = Console.ReadLine();
            Console.Write("Enter Salary => ");
            e.Salary = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Enter department => ");
            e.Department = Console.ReadLine();
           if( empRepository.Insert(e)>0)
                Console.WriteLine("Employee added successfully");
           else
                Console.WriteLine("Some error has occurred");
        }

        void DeleteEmployee()
        {
            Console.Write("Enter Id => ");
            int id = Convert.ToInt32(Console.ReadLine());
            if(empRepository.Delete(id)>0)
                Console.WriteLine("Employee Deleted");
            else
                Console.WriteLine("Some error has occurred");
        }
        void PrintAllEmployee()
        {
            List<Employee> collection = empRepository.GetAll();
            foreach (Employee item in collection)
            {
                Console.WriteLine(item.Id+"\t"+item.Name+"\t"+item.Department+"\t"+item.Salary);
            }
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
                        AddEmployee();
                        break;
                    case (int)Operations.Delete:
                        DeleteEmployee();
                        break;
                    case (int)Operations.Print:
                        PrintAllEmployee();
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
