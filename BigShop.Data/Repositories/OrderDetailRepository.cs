using BigShop.Data.Infrastructure;
using BigShop.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace BigShop.Data.Repositories
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        //IEnumerable<OrderDetail> GetOrderDetail();
    }

    public class OrderDetailRepository : RepositoryBase<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        //public IQueryable<OrderDetail> GetOrderDetail()
        //{
        //    var query = from od in DbContext.OrderDetails
        //                join o in DbContext.Orders
        //                on od.OrderID equals o.ID
        //                join p in DbContext.Products
        //                on od.ProductID equals p.ID
        //                select new { o.CustomerName, o.CustomerMobile, od.Product.Name, od.Price, od.Quantity, o.CreatedDate };

        //    return query;
        //}
    }
}