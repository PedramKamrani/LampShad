using Appliction.Construct.ViewModel.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_QueryLamshade.Contracts.Product
{
    public interface IProductQuery
    {
        List<ProductQueryModel> GetProductArrivals();
        ProductQueryModel GetProductDetails(string slug);
        List<ProductQueryModel> Search(string value);
        List<Cartitem> CheckInventoryStatus(List<Cartitem> cartItems);
    }
}
