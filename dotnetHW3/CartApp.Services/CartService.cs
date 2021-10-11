using System;
using CartApp.Data.Repository;
using CartApp.Data.Models;

namespace CartApp.Services
{
    public class CartService
    {
        IRepository<Order> orderRes;
        IRepository<OrderDetail> odRes;

        public CartService()
        {
            orderRes = new OrderRepository();
            odRes = new OrderDetailRepository();
        }

        public int Commit(Order o,bool discount)
        {
            //OrderDetail or = new Order();
            int iden = orderRes.Insert(o);
            if (iden > 0)
            {
                //"insert into OrderDetails values(@OrderId,@ProductId,@Quantity, @UnitPrice, @Discount
                OrderDetail od = new OrderDetail();
                od.OrderId = iden;
                od.Discount = discount;
                if (o.AppleNo > 0)
                {
                    od.ProductId = 1;
                    od.Quantity = o.AppleNo;
                    od.UnitPrice = 0.45M;
                    odRes.Insert(od);
                }
                if (o.OrangeNo > 0)
                {
                    od.ProductId = 2;
                    od.Quantity = o.OrangeNo;
                    od.UnitPrice = 0.65M;
                    odRes.Insert(od);
                }
                return 1;
            }
                //Console.WriteLine("Your order has been placed successfully!");
            else
                return 0;
                //Console.WriteLine("Somme error has occurred");
                
        }

        public decimal calculate(int a, int o,bool discount)
        {
            if (discount)
                return (a / 2 + a % 2) * 0.45M + a / 3 * 1.3M + (a % 3) * 0.65M;
            return 0.45M * a + 0.65M * o;
        }
        

        public void quit()
        {
            Console.WriteLine("Your order has been cancelled.");
        }
    }
}
