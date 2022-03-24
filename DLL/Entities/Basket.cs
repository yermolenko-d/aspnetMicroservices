using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Entities
{
    public class Basket
    {
        public string UserName { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
        public decimal TotalPrice
        {
            get
            {
                decimal totalprice = 0;
                foreach (var item in Items)
                {
                    totalprice += item.Price * item.Quantity;
                }
                return totalprice;
            }
        }
        
        public Basket() 
        {

        }
        public Basket(string userName)
        {
            UserName = userName;
        }
    }
}
