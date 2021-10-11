using System;
using System.Collections.Generic;
using System.Text;

namespace CartApp.Data.Repository
{
    public interface IRepository<T> where T : class
    {
        int Insert(T item);
        int Delete(int id);
        int Update(T item);
        IEnumerable<T> GetAll();
        T GetById(int id);
    }
}
