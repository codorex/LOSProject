using LOS.CommentModel.Domain;
using LOS.ProducModel.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LOS.CommentService.Domain
{
    public interface ICommentService
    {
        Task<Comment> GetAsync(int commentID);

        Task<List<Comment>> GetListAsync(int pageNumber, int pageSize);

        Task CreateAsync(Comment comment);

        Task UpdateAsync(Comment comment);

        Task DeleteAsync(int commentID);

        Task<List<Comment>> GetAllAsync();

        Task<List<Comment>> GetByProductId(int id);

        Task<int> GetCommentsCountForUserAsync(string userId);

        Task<List<Product>> GetReviewedProductsForUserAsync(string userId);
    }
}
