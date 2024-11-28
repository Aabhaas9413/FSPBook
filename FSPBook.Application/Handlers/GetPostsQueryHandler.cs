using FSPBook.Application.DTOs;
using FSPBook.Application.Queries;
using FSPBook.Data.Interfaces;
using MediatR;

namespace FSPBook.Application.Features.Posts.Queries
{
    public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, GetPostsResult>
    {
        private readonly IPostRepository _postRepository;

        public GetPostsQueryHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<GetPostsResult> Handle(GetPostsQuery request, CancellationToken cancellationToken)
        {
            var allPosts = await _postRepository.GetAllAsync();
            var totalPosts = allPosts.Count();

            var paginatedPosts = allPosts
                .OrderByDescending(p => p.DateTimePosted)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(p => new PostDto
                {
                    Id = p.Id,
                    Content = p.Content,
                    AuthorName = $"{p.Author.FirstName} {p.Author.LastName}",
                    DateTimePosted = p.DateTimePosted,
                    AuthorId = p.AuthorId
                })
                .ToList();

            return new GetPostsResult
            {
                Posts = paginatedPosts,
                TotalPages = (int)Math.Ceiling((double)totalPosts / request.PageSize)
            };
        }
    }
}
