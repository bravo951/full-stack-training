using System;
using System.Collections.Generic;
using System.Text;
using Data.Model;
using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace Data.Repository
{
    public class RoomRespository : IRepository<Room>
    {
        MyDBContext db;
        public RoomRespository()
        {
            db = new MyDBContext();
        }
        public int Delete(int id)
        {
            IDbConnection conn = db.GetConnection();
            return conn.Execute("delete from Rooms where Id = @Id ", new { Id=id });
        }

        public IEnumerable<Room> GetAll()
        {
            IDbConnection conn = db.GetConnection();
            return conn.Query<Room>("select id, RTCODE, STATUS from Rooms");
        }

        public int Insert(Room obj)
        {
            //throw new NotImplementedException();
            IDbConnection conn = db.GetConnection();
            return conn.Execute("insert into Rooms values(@RTCODE,@STATUS)", obj);
        }

        public int Update(Room obj)
        {
            //throw new NotImplementedException();
            IDbConnection conn = db.GetConnection();
            return conn.Execute("update Rooms set RTCODE = @RTCODE, STATUS = @STATUS where Id = @Id ", obj);
        }
    
    }
}
