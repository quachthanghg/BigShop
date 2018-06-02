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
    public interface IOrderDetailService
    {
        IEnumerable<OrderDetail> GetAllOrderDetail();
        IEnumerable<OrderDetail> GetAllOrderDetail1();
        IEnumerable<OrderDetail> GetAllOrderDetail2();
        IEnumerable<OrderDetail> GetAllOrderDetail3();
        IEnumerable<OrderDetail> GetAllOrderDetail4();
        IEnumerable<OrderDetail> GetOrderDetailById(int orderID);
        Order GetById(int id);
        OrderDetail Remove(int id);
    }
    public class OrderDetailService : IOrderDetailService
    {
        private IProductRepository _productRepository;
        private IOrderDetailRepository _orderDetailRepository;
        private IOrderRepository _orderRepository;
        private IUnitOfWork _unitOfWork;
        public OrderDetailService(IProductRepository productRepository, IOrderDetailRepository orderDetailRepository, IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<OrderDetail> GetAllOrderDetail()
        {
            return _orderDetailRepository.GetMulti(x => x.Order.Status == false);
        }

        public IEnumerable<OrderDetail> GetAllOrderDetail1()
        {
            return _orderDetailRepository.GetMulti(x => x.Order.Status == true);
        }

        public IEnumerable<OrderDetail> GetAllOrderDetail2()
        {
            return _orderDetailRepository.GetMulti(x => x.Order.Status == false);
        }

        public IEnumerable<OrderDetail> GetAllOrderDetail3()
        {
            return _orderDetailRepository.GetMulti(x => x.Order.Status == false);
        }

        public IEnumerable<OrderDetail> GetAllOrderDetail4()
        {
            return _orderDetailRepository.GetMulti(x => x.Order.Status == false);
        }

        public Order GetById(int id)
        {
            return _orderRepository.GetSignleById(id);
        }

        public IEnumerable<OrderDetail> GetOrderDetailById(int orderID)
        {
            var model = _orderDetailRepository.GetMulti(x => x.OrderID == orderID);
            return model;
        }

        public OrderDetail Remove(int id)
        {
            return _orderDetailRepository.Delete(id);
        }
    }
}
