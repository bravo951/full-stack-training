using System;
using System.Collections.Generic;
using System.Text;
using Data.Model;
using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace Data.Repository
{
    public class CustomRepository : IRepository<Customer>
    {
        MyDBContext db = new MyDBContext();
        public CustomRepository()
        {
            db = new MyDBContext();
        }
        public int Delete(int id)
        {
            IDbConnection conn = db.GetConnection();
            return conn.Execute("delete from Customers where Id = @Id ", new { Id = id });
        }

        public IEnumerable<Customer> GetAll()
        {
            IDbConnection conn = db.GetConnection();
            return conn.Query<Customer>("select id, ROOMNO,CNAME,ADDRESS,PHONE,EMAIL,CHECKIN,TotalPERSONS,BookingDays,ADVANCE from Customers");
        }

        public int Insert(Customer obj)
        {
            IDbConnection conn = db.GetConnection();
            return conn.Execute("insert into Customers values(@ROOMNO,@CNAME,@ADDRESS,@PHONE,@EMAIL,@CHECKIN,@TotalPERSONS,@BookingDays,@ADVANCE)", obj);
        }

        

        public int Update(Customer obj)
        {
            //throw new NotImplementedException();
            IDbConnection conn = db.GetConnection();
            return conn.Execute(@"update Customers set ROOMNO = @ROOMNO, CNAME = @CNAME,
                                                      ADDRESS = @ADDRESS, PHONE = @PHONE, 
                                                      EMAIL = @EMAIL,CHECKIN = @CHECKIN, 
                                                      TotalPERSONS = @TotalPERSONS, BookingDays = @BookingDays,
                                                      ADVANCE = @ADVANCE 
                                                      where Id = @Id ", obj);
        }

    }
}
