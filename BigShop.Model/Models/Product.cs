using BigShop.Model.Abstracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BigShop.Model.Models
{
    [Table("Products")]
    public class Product : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        [Required]
        [MaxLength(256)]
        [Column(TypeName = "varchar")]
        public string Alias { set; get; }

        [Required]
        public int CategoryID { set; get; }

        [MaxLength(256)]
        public string Image { set; get; }

        [Column(TypeName = "xml")]
        public string MoreImage { set; get; }

        public decimal OriginalPrice { get; set; }

        public decimal Price { set; get; }

        public decimal? Promotion { set; get; }

        public int? Warranty { set; get; }
        public string Profile { get; set; }
        
        public string Description { set; get; }
     
        public string Content { set; get; }

        public bool? HomeFlag { set; get; }

        public bool? HotFlag { set; get; }

        public int? ViewCount { set; get; }

        public string Tags { set; get; }
        public int? Quantity { get; set; }

        [ForeignKey("CategoryID")]
        public virtual ProductCategory ProductCategory { set; get; }
        [ForeignKey("Tags")]

        public virtual IEnumerable<ProductTag> ProductTags { set; get; }
        public virtual IEnumerable<Sale> Sales { set; get; }
    }
}