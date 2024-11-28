using FSPBook.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FSPBook.Data.Interfaces
{
    public interface IPostRepository
    {
        Task AddAsync(Post post);
        Task<IEnumerable<Post>> GetAllAsync();
        Task<Post> GetByIdAsync(int id);
        Task<IEnumerable<Post>> GetPostsByAuthorAsync(int authorId);
        Task<List<Post>> GetPostsByAuthorIdAsync(int authorId);
    }
}
