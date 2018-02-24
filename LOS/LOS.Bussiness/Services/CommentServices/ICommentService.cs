using LOS.Bussiness.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOS.Bussiness.Services.CommentServices
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
    }
}
