using BigShop.Common.ViewModels;
using BigShop.Data.Infrastructure;
using BigShop.Model.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BigShop.Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(string fromDate, string toDate);

        IEnumerable<TopSaleViewModel> TopSale();

        IEnumerable<ProductNotBuyViewModel> ProductNotBuy();

        IEnumerable<CountProductViewModel> ProductIsPhone();

        IEnumerable<CountProductViewModel> ProductIsTablet();

        IEnumerable<CountProductViewModel> ProductIsLaptop();
    }

    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(string fromDate, string toDate)
        {
            var parameters = new SqlParameter[]{
                new SqlParameter("@fromDate",fromDate),
                new SqlParameter("@toDate",toDate)
            };
            return DbContext.Database.SqlQuery<RevenueStatisticViewModel>("GetRevenueStatistic @fromDate,@toDate", parameters);
        }

        public IEnumerable<TopSaleViewModel> TopSale()
        {
            var parameters = new SqlParameter[] { };
            return DbContext.Database.SqlQuery<TopSaleViewModel>("TopSale", parameters);
        }

        public IEnumerable<ProductNotBuyViewModel> ProductNotBuy()
        {
            var parameters = new SqlParameter[] { };
            return DbContext.Database.SqlQuery<ProductNotBuyViewModel>("ProductNotBuy", parameters);
        }

        public IEnumerable<CountProductViewModel> ProductIsPhone()
        {
            var parameters = new SqlParameter[] { };
            return DbContext.Database.SqlQuery<CountProductViewModel>("ProductIsPhone", parameters);
        }

        public IEnumerable<CountProductViewModel> ProductIsTablet()
        {
            var parameters = new SqlParameter[] { };
            return DbContext.Database.SqlQuery<CountProductViewModel>("ProductIsTablet", parameters);
        }

        public IEnumerable<CountProductViewModel> ProductIsLaptop()
        {
            var parameters = new SqlParameter[] { };
            return DbContext.Database.SqlQuery<CountProductViewModel>("ProductIsLaptop", parameters);
        }
    }
}