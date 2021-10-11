using System;
using System.Collections.Generic;
using System.Data;
using CartApp.Data.Models;
using Dapper;


namespace CartApp.Data.Repository
{
    public class OrderRepository : IRepository<Order> 
    {
        OrderDBContext db;
        public OrderRepository()
        {
            db = new OrderDBContext();
        }
        public int Delete(int id)
        {
            using (IDbConnection conn = db.GetConnection())
            {
                return conn.Execute("delete from Orders where Id = @oid", new { oid = id });
            }
        }

        public IEnumerable<Order> GetAll()
        {
            using (IDbConnection conn = db.GetConnection())
            {
                return conn.Query<Order>("Select Id, AppleNo, OrangeNo, TotalPrice, OrderDate from Orders");
            }
        }

        public Order GetById(int id)
        {
            using (IDbConnection conn = db.GetConnection())
            {
                return conn.QueryFirstOrDefault<Order>("Select Id, AppleNo, OrangeNo, TotalPrice, OrderDate from Orders where Id = @oid", new { oid = id});
            }
        }

        public int Insert(Order item)
        {
            using (IDbConnection conn = db.GetConnection())
            {
                string query = "insert into Orders values(@AppleNo,@OrangeNo, @TotalPrice,@OrderDate);select @@identity;";
                return conn.ExecuteScalar<int>(query, item);
            }
        }

        public int Update(Order item)
        {
            using (IDbConnection conn = db.GetConnection())
            {
                return conn.Execute(@"update Orders set Id = @Id,AppleNo = @AppleNo, OrangeNo = @OrangeNo, TotalPrice = @TotalPrice, OrderDate = @OrderDate where Id = @Id", item);
            }
        }

        
    }
}
