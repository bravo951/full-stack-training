using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface IRepository<T>
    {
        int Insert(T obj);
        int Delete(int id);
        List<T> GetAll();
        T GetById(int id);
        
    }
}
