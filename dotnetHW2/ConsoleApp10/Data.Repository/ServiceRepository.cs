using System;
using System.Collections.Generic;
using System.Text;
using Data.Model;
using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace Data.Repository
{
    public class ServiceRepository : IRepository<Services>
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

        public IEnumerable<Services> GetAll()
        {
            IDbConnection conn = db.GetConnection();
            return conn.Query<Services>("select id, ROOMNO, SDESC, AMOUNT, ServiceDate from Services");
        }

        public int Insert(Services obj)
        {
            IDbConnection conn = db.GetConnection();
            return conn.Execute("insert into Services values(@ROOMNO,@SDESC,@AMOUNT,@ServiceDate)", obj);
        }

        public int Update(Services obj)
        {
            //throw new NotImplementedException();
            IDbConnection conn = db.GetConnection();
            return conn.Execute(@"update Services set ROOMNO = @ROOMNO, SDESC = @SDESC,
                                                      AMOUNT = @AMOUNT, ServiceDate = @ServiceDate 
                                                      where Id = @Id ", obj);
        }
    }
}
