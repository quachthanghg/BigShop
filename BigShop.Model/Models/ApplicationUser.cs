using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BigShop.Model.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(50)]
        public string FullName { get; set; }

        [MaxLength(256)]
        public string Address { get; set; }

        public DateTime? Birthday { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual IEquatable<Order> Orders { get; set; }
    }
}