using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface IRepository<T> where T : class
    {
        int Insert(T obj);
        int Update(T obj);
        int Delete(int id);
        IEnumerable<T> GetAll();
        //T GetById(int id);
        
    }
}
