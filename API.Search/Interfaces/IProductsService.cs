using API.Search.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Search.Interfaces
{
    public interface IProductsService
    {
        Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync();
    }
}