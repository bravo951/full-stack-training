using System;
using System.Collections.Generic;
using System.Text;
using dotnetHW1.Data.Model;

namespace dotnetHW1.Data.Respository
{
    public interface IRepository<T> where T : class
    {
        //int excute(string cmdText, Dictionary<string, object> parameters);
        int Insert(Custm c);
        int Update(Custm c);
        int Delete(int id);
     
        IEnumerable<T> GetAll();
        T GetById(int id);

    }
}
