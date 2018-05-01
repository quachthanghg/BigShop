using System;
using System.Runtime.Serialization;

namespace BigShop.Web.Models
{
    [Serializable]
    [DataContract]
    public class ProductViewModel
    {
        [DataMember]
        public int ID { set; get; }
        [DataMember]
        public string Name { set; get; }
        [DataMember]
        public string Alias { set; get; }
        [DataMember]
        public int CategoryID { set; get; }
        [DataMember]
        public string Image { set; get; }
        [DataMember]
        public string MoreImage { set; get; }
        [DataMember]
        public decimal Price { set; get; }
        [DataMember]
        public decimal? Promotion { set; get; }
        [DataMember]
        public int? Warranty { set; get; }
        public string Profile { get; set; }
        [DataMember]
        public string Description { set; get; }
        [DataMember]
        public string Content { set; get; }
        [DataMember]
        public bool? HomeFlag { set; get; }
        [DataMember]
        public bool? HotFlag { set; get; }
        [DataMember]
        public int? ViewCount { set; get; }
        [DataMember]
        public DateTime? CreatedDate { set; get; }
        [DataMember]
        public string CreatedBy { set; get; }
        [DataMember]
        public DateTime? UpdatedDate { set; get; }
        [DataMember]
        public string UpdatedBy { set; get; }
        [DataMember]
        public string MetaKeyword { set; get; }
        [DataMember]
        public string MetaDescription { set; get; }
        public bool Status { set; get; }
        [DataMember]
        public string Tags { get; set; }
        [DataMember]
        public int? Quantity { get; set; }
        [DataMember]
        public virtual ProductCategoryViewModel ProductCategory { set; get; }
    }
}