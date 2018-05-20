namespace BigShop.Web.Models
{
    public class SaleViewModel
    {
        public int ID { get; set; }
        
        public string Description { get; set; }

        public int ProductID { get; set; }

        public virtual ProductViewModel Product { set; get; }
    }
}