namespace BigShop.Web.Models
{
    public class ContactDetailViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Website { get; set; }

        public double? Lat { get; set; }

        public double? Lng { get; set; }
        public bool Status { get; set; }
    }
}