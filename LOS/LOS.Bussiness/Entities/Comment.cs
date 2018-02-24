using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOS.Bussiness.Entities
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
