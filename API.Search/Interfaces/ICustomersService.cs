using API.Search.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Search.Interfaces
{
    public interface ICustomersService
    {
        Task<(bool IsSuccess, IEnumerable<Customer> Customers, string ErrorMessage)> GetCustomersAsync();
    }
}