using API.Search.Interfaces;
using API.Search.Models;
using System.Linq;
using System.Threading.Tasks;

namespace API.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService ordersService;
        private readonly IProductsService productsService;
        private readonly ICustomersService customersService;

        public SearchService(IOrdersService ordersService, IProductsService productsService, ICustomersService customersService)
        {
            this.ordersService = ordersService;
            this.productsService = productsService;
            this.customersService = customersService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var ordersResult = await ordersService.GetOrdersAsync(customerId);
            var productsResult = await productsService.GetProductsAsync();
            var customersResult = await customersService.GetCustomersAsync();

            if (ordersResult.IsSuccess)
            {
                foreach (var order in ordersResult.Orders)
                {
                    order.customer = customersResult.IsSuccess ?
                        customersResult.Customers.FirstOrDefault(c => c.Id == customerId) : new Customer { Id = 0, Name = "Not available", Address = null };
                    decimal ActualTotal = 0;
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productsResult.IsSuccess ?
                            productsResult.Products.FirstOrDefault(p => p.Id == item.ProductId).Name :
                            "Product information is not available";
                        item.UnitPrice = productsResult.IsSuccess ?
                            productsResult.Products.FirstOrDefault(p => p.Id == item.ProductId).Price :
                            item.UnitPrice;
                        ActualTotal += item.UnitPrice * item.Quantity;
                        item.ProductName = productsResult.IsSuccess ?
                            productsResult.Products.FirstOrDefault(p => p.Id == item.ProductId).Name :
                            "Product information is not available";
                    }
                    order.Total = ActualTotal;
                }
                var result = new
                {
                    Orders = ordersResult.Orders
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}
