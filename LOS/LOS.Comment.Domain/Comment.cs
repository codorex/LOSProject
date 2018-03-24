using System;

namespace LOS.CommentModel.Domain
{
    public class Comment
    {
        public int CommentID { get; set; }
        public string Content { get; set; }
        public int ProductID { get; set; }
        public string UserID { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
