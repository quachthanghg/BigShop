using BigShop.Common.ViewModels;
using BigShop.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Service.Services
{
    public interface IStatisticService
    {
        IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(string fromDate, string toDate);
    }
    public class StatisticService: IStatisticService
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
            return _orderRepository.GetRevenueStatistic(fromDate, toDate);
        }
    }
}
