using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BigShop.Model.Models
{
    [Table("ApplicationUserGroups")]
    public class ApplicationUserGroup
    {
        [MaxLength(128)]
        [Key]
        [Column(Order = 1)]
        public string UserID { get; set; }

        [Key]
        [Column(Order = 2)]
        public int GroupID { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("GroupID")]
        public virtual ApplicationGroup ApplicationGroup { get; set; }
    }
}