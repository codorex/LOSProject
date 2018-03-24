using LOS.CommentModel.Domain;
using LOS.ProducModel.Domain;
using System.Collections.Generic;

namespace LOS.WebApplication.Models
{
    public class CommentsModel
    {
        public int productId { get; set; }
        public List<Comment> Comments { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public string CurrentUserId { get; set; }
    }
}