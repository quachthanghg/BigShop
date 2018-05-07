using System;
using System.Collections.Generic;

namespace BigShop.Web.Models
{
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public DateTime? Birthday { get; set; }
        public string Bio { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public List<ApplicationGroupViewModel> ApplicationGroups { set; get; }
    }
}