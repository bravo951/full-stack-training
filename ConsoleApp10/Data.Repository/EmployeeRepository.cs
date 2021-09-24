using System;
using System.Collections.Generic;
using System.Text;
using Data.Model;
namespace Data.Repository
{
   public class EmployeeRepository : IRepository<Employee>
    {
        List<Employee> lstEmployee;
        public EmployeeRepository()
        {
            lstEmployee = new List<Employee>();
        }
        public int Delete(int id)
        {
            Employee e = GetById(id);
            if (e != null)
            {
                lstEmployee.Remove(e);
                return 1;
            }
            return 0;
        }

        public List<Employee> GetAll()
        {
            return lstEmployee;
        }

        public Employee GetById(int id)
        {
            foreach (Employee item in lstEmployee)
            {
                if (item.Id == id)
                    return item;
            }
            return null;
        }

        public int Insert(Employee obj)
        {
            lstEmployee.Add(obj);
            return 1;
        }
    }
}
