using System;
using System.Collections.Generic;
using System.Text;
using Data.Model;
using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace Data.Repository
{
    public class RoomTypeRespository : IRepository<RoomType>
    {
        MyDBContext db;
        public RoomTypeRespository()
        {
            db = new MyDBContext();
        }
        public int Delete(int id)
        {
            IDbConnection conn = db.GetConnection();
            return conn.Execute("delete from RoomTypes where Id = @Id ", new { Id=id });
        }

        public IEnumerable<RoomType> GetAll()
        {
            IDbConnection conn = db.GetConnection();
            return conn.Query<RoomType>("select id, RTDESC, Rent from RoomTypes");
        }

        public int Insert(RoomType obj)
        {
            //throw new NotImplementedException();
            IDbConnection conn = db.GetConnection();
            return conn.Execute("insert into RoomTypes values(@RTDESC,@Rent)", obj);
        }

        public int Update(RoomType obj)
        {
            //throw new NotImplementedException();
            IDbConnection conn = db.GetConnection();
            return conn.Execute("update RoomTypes set RTDESC = @RTDESC, Rent = @Rent where Id = @Id ", obj);
        }
    
    }
}
