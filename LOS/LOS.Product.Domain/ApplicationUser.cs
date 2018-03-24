using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace LOS.ProducModel.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public ApplicationUser()
        {
            this.Products = new HashSet<Product>();
        }
    }
}
