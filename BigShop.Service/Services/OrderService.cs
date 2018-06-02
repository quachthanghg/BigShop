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
        IEnumerable<Order> GetAll();
        Order Create(Order order, List<OrderDetail> lstOrderDetail);
        Order GetOrderById(int id);
        void UpdateStatus(int orderId);
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

        public Order Create(Order order, List<OrderDetail> lstOrderDetail)
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
                return order;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        

        public IEnumerable<Order> GetAll()
        {
            return _orderRepository.GetMulti(x => x.Status == false || x.PaymentStatus == "Chua thanh toan");
        }

        public Order GetOrderById(int id)
        {
            var model = _orderRepository.GetSignleById(id);
            return model;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void UpdateStatus(int orderId)
        {
            var order = _orderRepository.GetSignleById(orderId);
            order.Status = true;
            _orderRepository.Update(order);
        }
    }
}
