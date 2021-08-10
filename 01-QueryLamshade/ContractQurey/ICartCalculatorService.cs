using Appliction.Construct.ViewModel.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_QueryLamshade.ContractQurey
{
    public interface ICartCalculatorService
    {
        Cart ComputeCart(List<Cartitem> cartItems);
    }
}
