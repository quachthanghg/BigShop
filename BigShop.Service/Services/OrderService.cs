using BigShop.Data.Infrastructure;
using BigShop.Data.Repositories;
using BigShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Service.Services
{
    public interface IOrderService
    {
        bool Create(Order order, List<OrderDetail> lstOrderDetail);
        void SaveChanges();
    }
    public class OrderService: IOrderService
    {
        private IOrderRepository _orderRepository;
        private IErrorService _errorService;
        private IUnitOfWork _unitOfWork;
        private IOrderDetailRepository _orderDetailRepository;
        public OrderService(IErrorService errorService, IOrderRepository orderRepository, IUnitOfWork unitOfWork, IOrderDetailRepository orderDetailRepository)
        {
            this._errorService = errorService;
            this._orderRepository = orderRepository;
            this._unitOfWork = unitOfWork;
            this._orderDetailRepository = orderDetailRepository;
        }

        public bool Create(Order order, List<OrderDetail> lstOrderDetail)
        {
            try
            {
                _orderRepository.Add(order);
                _unitOfWork.Commit();
                foreach (var item in lstOrderDetail)
                {
                    item.OrderID = order.ID;
                    _orderDetailRepository.Add(item);
                }
                _unitOfWork.Commit();
                return true;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
