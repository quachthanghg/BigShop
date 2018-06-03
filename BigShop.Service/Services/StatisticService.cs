using BigShop.Common.ViewModels;
using BigShop.Data.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace BigShop.Service.Services
{
    public interface IStatisticService
    {
        IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(string fromDate, string toDate);

        IEnumerable<TopSaleViewModel> GetTopSale();

        IEnumerable<ProductNotBuyViewModel> GetProductNotBuy();

        IEnumerable<CountProductViewModel> GetProductIsPhone();

        IEnumerable<CountProductViewModel> GetProductIsTablet();

        IEnumerable<CountProductViewModel> GetProductIsLaptop();
    }

    public class StatisticService : IStatisticService
    {
        private IOrderRepository _orderRepository;
        private IErrorService _errorService;

        public StatisticService(IErrorService errorService, IOrderRepository orderRepository)
        {
            this._errorService = errorService;
            this._orderRepository = orderRepository;
        }

        public IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(string fromDate, string toDate)
        {
            return _orderRepository.GetRevenueStatistic(fromDate, toDate).ToList();
        }

        public IEnumerable<TopSaleViewModel> GetTopSale()
        {
            var query = _orderRepository.TopSale();
            return query;
        }

        public IEnumerable<ProductNotBuyViewModel> GetProductNotBuy()
        {
            var query = _orderRepository.ProductNotBuy();
            return query;
        }

        public IEnumerable<CountProductViewModel> GetProductIsPhone()
        {
            var query = _orderRepository.ProductIsPhone();
            return query;
        }

        public IEnumerable<CountProductViewModel> GetProductIsTablet()
        {
            var query = _orderRepository.ProductIsTablet();
            return query;
        }

        public IEnumerable<CountProductViewModel> GetProductIsLaptop()
        {
            var query = _orderRepository.ProductIsLaptop();
            return query;
        }
    }
}