using System;
using System.Collections.Generic;
using System.Text;
using Data.Model;
using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace Data.Repository
{
    public class ServiceRepository : IRepository<Service>
    {
        MyDBContext db = new MyDBContext();
        public ServiceRepository()
        {
            db = new MyDBContext();
        }
        public int Delete(int id)
        {
            IDbConnection conn = db.GetConnection();
            return conn.Execute("delete from Services where Id = @Id ", new { Id = id });
        }

        public IEnumerable<Service> GetAll()
        {
            IDbConnection conn = db.GetConnection();
            return conn.Query<Service>("select id, ROOMNO, SDESC, AMOUNT, ServiceDate from Services");
        }

        public int Insert(Service obj)
        {
            IDbConnection conn = db.GetConnection();
            return conn.Execute("insert into Services values(@ROOMNO,@SDESC,@AMOUNT,@ServiceDate)", obj);
        }

        public int Update(Service obj)
        {
            //throw new NotImplementedException();
            IDbConnection conn = db.GetConnection();
            return conn.Execute(@"update Services set ROOMNO = @ROOMNO, SDESC = @SDESC,
                                                      AMOUNT = @AMOUNT, ServiceDate = @ServiceDate 
                                                      where Id = @Id ", obj);
        }
    }
}
