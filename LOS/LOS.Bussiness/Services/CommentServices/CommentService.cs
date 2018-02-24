using LOS.Bussiness.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOS.Bussiness.Services.CommentServices
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationContext context;

        public CommentService (ApplicationContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(Comment comment)
        {
            context.Comments.Add(comment);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int commentID)
        {
            Comment comment = await context.Comments.SingleOrDefaultAsync(c => c.CommentID == commentID);
            context.Comments.Remove(comment);

            await context.SaveChangesAsync();
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            List<Comment> comments = await context.Comments.ToListAsync();

            return comments;
        }

        public async Task<Comment> GetAsync(int commentID)
        {
            Comment comment = await context.Comments.SingleOrDefaultAsync(c => c.CommentID == commentID);

            return comment;
        }

		public async Task<List<Comment>> GetByProductId(int id)
		{
			return await context.Comments.Where(c => c.ProductID == id).OrderByDescending(c => c.CommentID).ToListAsync();
		}

		public async Task<List<Comment>> GetListAsync(int pageNumber, int pageSize)
        {
            List<Comment> comments = await context.Comments.OrderBy(c => c.CommentID).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return comments;
        }

        public async Task UpdateAsync(Comment comment)
        {
            Comment queryComment = await context.Comments.SingleOrDefaultAsync(c => c.CommentID == comment.CommentID);
            queryComment.Content = comment.Content;

            await context.SaveChangesAsync();
        }
    }
}
