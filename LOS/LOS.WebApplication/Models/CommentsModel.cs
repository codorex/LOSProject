using LOS.Domain.Models.Entities;
using LOS.Domain.Models.Entities.IdentityModels;
using System.Collections.Generic;

namespace LOS.Models
{
    public class CommentsModel
    {
        public int productId { get; set; }
        public List<Comment> Comments { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public string CurrentUserId { get; set; }
    }
}