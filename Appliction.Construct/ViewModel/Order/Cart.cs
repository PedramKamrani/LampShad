using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appliction.Construct.ViewModel.Order
{
   public class Cart
    {

        public double TotalAmount { get; set; }
        public double DiscountAmount { get; set; }
        public double PayAmount { get; set; }
        public int PaymentMethod { get; set; }
        public List<Cartitem> Items { get; set; }


        public Cart()
        {
            Items = new List<Cartitem>();
        }
        public void Add(Cartitem cartItem)
        {
            Items.Add(cartItem);
            TotalAmount += cartItem.TotalItemPrice;
            DiscountAmount += cartItem.DiscountAmount;
            PayAmount += cartItem.ItemPayAmount;
        }
        public void SetPaymentMethod(int methodId)
        {
            PaymentMethod = methodId;
        }

    }
}
