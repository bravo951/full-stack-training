using System;
using System.Collections.Generic;
using System.Text;
using dotnetHW1.Data.Model;
using System.Data.SqlClient;

namespace dotnetHW1.Data.Respository
{
    public class CustmRepository : IRepository<Custm>
    {//Data Source=.;Initial Catalog=Mydatabase;Integrated Security=True
        DbContext db;
        public CustmRepository()
        {
            db = new DbContext();
        }
        public int Delete(int id)
        {
            string cmd = "delete from Customer where Id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Id", id);
            return db.excute(cmd, parameters);
        }

        public IEnumerable<Custm> GetAll()
        {
            throw new NotImplementedException();
        }

        public Custm GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(Custm c)
        {
            string cmd = "insert into Customer values(@id,@f,@l,@mobile,@email,@city,@state)";
            Console.WriteLine(cmd);
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", c.Id);
            parameters.Add("@f", c.FirstName);
            parameters.Add("@l", c.LastName);
            parameters.Add("@mobile", c.Mobile);
            parameters.Add("@email", c.EmailId);
            parameters.Add("@city", c.City);
            parameters.Add("@state", c.State);
            return db.excute(cmd, parameters);
        }
        public int Update(Custm c)
        {
            //throw new NotImplementedException();
            string cmd = "update Customer set FirstName = @firstNM, LastName = @lastNM where Id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", c.Id);
            parameters.Add("@firstNM", c.FirstName);
            parameters.Add("@lastNM", c.LastName);
            return db.excute(cmd, parameters);
        }
    }
}
