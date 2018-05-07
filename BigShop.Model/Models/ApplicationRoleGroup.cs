using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BigShop.Model.Models
{
    [Table("ApplicationRoleGroups")]
    public class ApplicationRoleGroup
    {
        [Key]
        [Column(Order = 1)]
        public int GroupID { get; set; }

        [MaxLength(128)]
        [Key]
        [Column(Order = 2)]
        public string RoleID { get; set; }

        [ForeignKey("RoleID")]
        public virtual ApplicationRole ApplicationRole { get; set; }

        [ForeignKey("GroupID")]
        public virtual ApplicationGroup ApplicationGroup { get; set; }
    }
}