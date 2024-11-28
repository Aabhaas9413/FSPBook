using FSPBook.Data.Entities;
using FSPBook.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FSPBook.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly Context _context;

        public PostRepository(Context context)
        {
            _context = context;
        }

        public async Task AddAsync(Post post)
        {
            await _context.Post.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _context.Post
                .Include(p => p.Author)
                .ToListAsync();
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            return await _context.Post
                .Include(p => p.Author)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Post>> GetPostsByAuthorAsync(int authorId)
        {
            return await _context.Post
                .Where(p => p.AuthorId == authorId)
                .ToListAsync();
        }

        public async Task<List<Post>> GetPostsByAuthorIdAsync(int authorId)
        {
            return await _context.Post
                .Where(p => p.AuthorId == authorId)
                .OrderByDescending(p => p.DateTimePosted)
                .Select(p => new Post
                {
                    Id = p.Id,
                    Content = p.Content,
                    DateTimePosted = p.DateTimePosted,
                    Author = p.Author,
                    AuthorId = authorId
                })
                .ToListAsync();
        }
    }
}
