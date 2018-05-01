using System;

namespace BigShop.Web.Models
{
    [Serializable]
    public class ShoppingCartViewModel
    {
        public int ProductID { get; set; }
        public ProductViewModel Product { set; get; }
        public int Quantity { get; set; }
    }
}