using System.Collections.Generic;

namespace BigShop.Web.Models
{
    public class ApplicationGroupViewModel
    {
        public int ID { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }

        public IEnumerable<ApplicationRoleViewModel> ApplicationRoles { set; get; }
    }
}