using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appliction.Construct.ViewModel.Order
{
   public interface ICartService
    {
        Cart Get();
        void Set(Cart cart);
    }
}
