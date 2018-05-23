using BigShop.Model.Models;
using BigShop.Web.Models;
using System;

namespace BigShop.Web.Infrastructure.Extension
{
    public static class EntityExtension
    {
        public static void UpdatePostCategory(this PostCategory postCategory, PostCategoryViewModel postCategoryViewModel)
        {
            postCategory.ID = postCategoryViewModel.ID;
            postCategory.Name = postCategoryViewModel.Name;
            postCategory.Alias = postCategoryViewModel.Alias;
            postCategory.Description = postCategoryViewModel.Description;
            postCategory.ParentID = postCategoryViewModel.ParentID;
            postCategory.DisplayOrder = postCategoryViewModel.DisplayOrder;
            postCategory.Image = postCategoryViewModel.Image;
            postCategory.HomeFlag = postCategoryViewModel.HomeFlag;

            postCategory.CreatedDate = postCategoryViewModel.CreatedDate;
            postCategory.CreatedBy = postCategoryViewModel.CreatedBy;
            postCategory.UpdatedDate = postCategoryViewModel.UpdatedDate;
            postCategory.UpdatedBy = postCategoryViewModel.UpdatedBy;
            postCategory.MetaKeyword = postCategoryViewModel.MetaKeyword;
            postCategory.MetaDescription = postCategoryViewModel.MetaDescription;
            postCategory.Status = postCategoryViewModel.Status;
        }

        public static void UpdateProductCategory(this ProductCategory productCategory, ProductCategoryViewModel productCategoryViewModel)
        {
            productCategory.ID = productCategoryViewModel.ID;
            productCategory.Name = productCategoryViewModel.Name;
            productCategory.Alias = productCategoryViewModel.Alias;
            productCategory.Description = productCategoryViewModel.Description;
            productCategory.ParentID = productCategoryViewModel.ParentID;
            productCategory.DisplayOrder = productCategoryViewModel.DisplayOrder;
            productCategory.Image = productCategoryViewModel.Image;

            productCategory.HomeFlag = productCategoryViewModel.HomeFlag;
            productCategory.CreatedDate = productCategoryViewModel.CreatedDate;
            productCategory.CreatedBy = productCategoryViewModel.CreatedBy;
            productCategory.UpdatedDate = productCategoryViewModel.UpdatedDate;
            productCategory.UpdatedBy = productCategoryViewModel.UpdatedBy;
            productCategory.MetaKeyword = productCategoryViewModel.MetaKeyword;
            productCategory.MetaDescription = productCategoryViewModel.MetaDescription;
            productCategory.Status = productCategoryViewModel.Status;
        }

        public static void UpdateProduct(this Product product, ProductViewModel productViewModel)
        {
            product.ID = productViewModel.ID;
            product.Name = productViewModel.Name;
            product.Alias = productViewModel.Alias;
            product.CategoryID = productViewModel.CategoryID;
            product.Image = productViewModel.Image;
            product.MoreImage = productViewModel.MoreImage;
            product.Price = productViewModel.Price;
            product.Promotion = productViewModel.Promotion;
            product.Warranty = productViewModel.Warranty;
            product.Description = productViewModel.Description;
            product.Content = productViewModel.Content;
            product.HomeFlag = productViewModel.HomeFlag;
            product.HotFlag = productViewModel.HotFlag;
            product.ViewCount = productViewModel.ViewCount;
            product.Tags = productViewModel.Tags;
            product.Quantity = productViewModel.Quantity;
            product.Profile = productViewModel.Profile;
            product.OriginalPrice = productViewModel.OriginalPrice;
            product.ImageHome = productViewModel.ImageHome;

            product.CreatedDate = productViewModel.CreatedDate;
            product.CreatedBy = productViewModel.CreatedBy;
            product.UpdatedDate = productViewModel.UpdatedDate;
            product.UpdatedBy = productViewModel.UpdatedBy;
            product.MetaKeyword = productViewModel.MetaKeyword;
            product.MetaDescription = productViewModel.MetaDescription;
            product.Status = productViewModel.Status;
        }

        public static void UpdateFeedback(this Feedback feedback, FeedbackViewModel feedbackViewModel)
        {
            feedback.Name = feedbackViewModel.Name;
            feedback.Email = feedbackViewModel.Email;
            feedback.Message = feedbackViewModel.Message;
            feedback.CreatedDate = DateTime.Now;
            feedback.Status = feedbackViewModel.Status;
        }

        public static void UpdateSlide(this Slide slide, SlideViewModel slideViewModel)
        {
            slide.Name = slideViewModel.Name;
            slide.Description = slideViewModel.Description;
            slide.Content = slideViewModel.Content;
            slide.DisplayOrder = slideViewModel.DisplayOrder;
            slide.Image = slideViewModel.Image;
            slide.URL = slideViewModel.URL;
            slide.Status = slideViewModel.Status;
        }

        public static void UpdatePost(this Post post, PostViewModel postViewModel)
        {
            post.ID = postViewModel.ID;
            post.Name = postViewModel.Name;
            post.Alias = postViewModel.Alias;
            post.Description = postViewModel.Description;
            post.CategoryID = postViewModel.CategoryID;
            post.Content = postViewModel.Content;
            post.Image = postViewModel.Image;
            post.HomeFlag = postViewModel.HomeFlag;
            post.HotFlag = postViewModel.HotFlag;
            post.ViewCount = postViewModel.ViewCount;

            post.CreatedDate = postViewModel.CreatedDate;
            post.CreatedBy = postViewModel.CreatedBy;
            post.UpdatedDate = postViewModel.UpdatedDate;
            post.UpdatedBy = postViewModel.UpdatedBy;
            post.MetaKeyword = postViewModel.MetaKeyword;
            post.MetaDescription = postViewModel.MetaDescription;
            post.Status = postViewModel.Status;
        }

        public static void UpdateOrder(this Order order, OrderViewModel orderViewModel)
        {
            order.CustomerName = orderViewModel.CustomerName;
            order.CustomerEmail = orderViewModel.CustomerEmail;
            order.CustomerAddress = orderViewModel.CustomerAddress;
            order.CustomerMessage = orderViewModel.CustomerMessage;
            order.CustomerMobile = orderViewModel.CustomerMobile;
            order.PaymentMethod = orderViewModel.PaymentMethod;
            order.CreatedDate = DateTime.Now;
            order.CreatedBy = orderViewModel.CreatedBy;
            order.CustomerID = orderViewModel.CustomerID;
            order.PaymentMethod = orderViewModel.PaymentMethod;
            order.PaymentStatus = orderViewModel.PaymentStatus;
            order.Status = orderViewModel.Status;
        }

        public static void UpdateOrderDetail(this OrderDetail orderDetail, OrderDetailViewModel orderDetailViewModel)
        {
            orderDetail.OrderID = orderDetailViewModel.OrderID;
            orderDetail.ProductID = orderDetailViewModel.ProductID;
            orderDetail.Quantity = orderDetailViewModel.Quantity;
            orderDetail.Price = orderDetailViewModel.Price;
        }

        public static void UpdateApplicationGroup(this ApplicationGroup appGroup, ApplicationGroupViewModel appGroupViewModel)
        {
            appGroup.ID = appGroupViewModel.ID;
            appGroup.Name = appGroupViewModel.Name;
            appGroup.Description = appGroupViewModel.Description;
        }

        public static void UpdateApplicationRole(this ApplicationRole appRole, ApplicationRoleViewModel appRoleViewModel, string action = "add")
        {
            if (action == "update")
                appRole.Id = appRoleViewModel.ID;
            else
                appRole.Id = Guid.NewGuid().ToString();
            appRole.Name = appRoleViewModel.Name;
            appRole.Description = appRoleViewModel.Description;
        }

        public static void UpdateUser(this ApplicationUser appUser, ApplicationUserViewModel appUserViewModel, string action = "add")
        {
            appUser.Id = appUserViewModel.Id;
            appUser.FullName = appUserViewModel.FullName;
            appUser.Birthday = appUserViewModel.Birthday;
            appUser.Email = appUserViewModel.Email;
            appUser.UserName = appUserViewModel.UserName;
            appUser.PhoneNumber = appUserViewModel.PhoneNumber;
        }

        public static void UpdateSale(this Sale sale, SaleViewModel saleViewModel)
        {
            sale.Description = saleViewModel.Description;
            sale.ProductID = saleViewModel.ProductID;
        }

        public static void UpdatePolicy(this Policy policy, PolicyViewModel policyViewModel)
        {
            policy.Name = policyViewModel.Name;
            policy.Description = policyViewModel.Description;
        }
    }
}