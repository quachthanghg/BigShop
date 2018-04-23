using AutoMapper;
using BigShop.Model.Models;
using BigShop.Web.Models;

namespace BigShop.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Config()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<PostViewModel, Post>();
                config.CreateMap<PostCategoryViewModel, PostCategory>();
                config.CreateMap<TagViewModel, Tag>();
                config.CreateMap<PostTagViewModel, PostTag>();
                config.CreateMap<ProductViewModel, Product>();
                config.CreateMap<ProductCategoryViewModel, ProductCategory>();
                config.CreateMap<ProductTagViewModel, ProductTag>();
                config.CreateMap<FooterViewModel, Footer>();
                config.CreateMap<SlideViewModel, Slide>();
            });
        }
    }
}