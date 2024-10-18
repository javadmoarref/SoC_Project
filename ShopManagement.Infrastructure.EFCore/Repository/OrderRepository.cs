using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class OrderRepository : RepositoryBase<long, Order>, IOrderRepository
    {
        private readonly ShopContext _context;
        private readonly AccountContext _accountContext;
        public OrderRepository(ShopContext context, AccountContext accountContext) : base(context)
        {
            _context = context;
            _accountContext = accountContext;
        }

        public List<OrderItemViewModel> GetItems(long orderId)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == orderId);
            if (order == null)
            {
                return new List<OrderItemViewModel>();
            }
            var items = order.Items.Where(x=>x.OrderId==orderId).Select(x => new OrderItemViewModel()
            {
                Count = x.Count,
                Name = x.Name,
                Picture = x.Picture,
                DiscountRate = x.DiscountRate,
                OrderId = x.OrderId,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice
            }).ToList();
            return items;
        }

        public OrderViewModel GetOrderById(long id)
        {
            var accounts = _accountContext.Accounts.Select(x => new {x.Id,x.Address,x.PostalCode}).ToList();
            var order= _context.Orders.Select(x=>new OrderViewModel()
            {
                Id = x.Id,
                AccountId = x.AccountId,
                AccountName = x.AccountName,
                AccountMobile = x.AccountMobile,
                DiscountAmount = x.DiscountAmount,
                IsCanceled = x.IsCanceled,
                IsPaid = x.IsPaid,
                IssueTrackingNo = x.IssueTrackingNo,
                PostalCode=x.PostalCode,
                PayAmount = x.PayAmount,
                RefId = x.RefId,
                TotalAmount = x.TotalAmount,
                CreationDate = x.CreationDate.ToFarsi()
            }).FirstOrDefault(x=>x.Id == id);
            order.AccountAddress = accounts.FirstOrDefault(x => x.Id == order.AccountId).Address;
            order.AccountAddressPostalCode=accounts.FirstOrDefault(x=>x.Id==order.AccountId).PostalCode;
            return order;
        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel)
        {
            var query = _context.Orders.Select(x => new OrderViewModel()
            {
                Id = x.Id,
                AccountId = x.AccountId,
                AccountName = x.AccountName,
                AccountMobile = x.AccountMobile,
                CreationDateGr = x.CreationDate,
                CreationDate = x.CreationDate.ToFarsi(),
                DiscountAmount = x.DiscountAmount,
                IsPaid = x.IsPaid,
                IsCanceled = x.IsCanceled,
                IsPayUnSuccess = x.IsPayUnSuccess,
                IsOrderCanceled = x.IsOrderCanceled,
                IssueTrackingNo = x.IssueTrackingNo,
                PayAmount = x.PayAmount,
                PostalCode = x.PostalCode,
                RefId = x.RefId,
                TotalAmount = x.TotalAmount
            });

            //foreach(var order in query)
            //{
            //    order.Items=GetItems(order.Id);
            //}

            if (!string.IsNullOrWhiteSpace(searchModel.CreationDate))
            {
                query = query.Where(x => x.CreationDateGr.Date == searchModel.CreationDate.ToGeorgianDateTime().Date);
            }
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(x => x.AccountName.Contains(searchModel.Name));
            }
            if (!string.IsNullOrWhiteSpace(searchModel.Mobile))
            {
                query = query.Where(x => x.AccountMobile.Contains(searchModel.Mobile));
            }
            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
