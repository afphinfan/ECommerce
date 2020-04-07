using API.Search.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Search.Interfaces
{
    public interface IOrdersService
    {
        Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId);
    }
}
