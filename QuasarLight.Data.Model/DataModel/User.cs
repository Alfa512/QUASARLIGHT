using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace QuasarLight.Data.Model.DataModel
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public override string Email { get; set; }
        public override string PhoneNumber { get; set; }
        public int Status { get; set; }
        public int Group { get; set; }
        public string RestorePasswordToken { get; set; }
        public DateTime TokenValidTo { get; set; }
        public virtual ICollection<UserImage> UserImages { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
