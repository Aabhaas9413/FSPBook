using FSPBook.Application.DTOs;
using MediatR;

namespace FSPBook.Application.Queries
{
    public class GetPostsQuery : IRequest<GetPostsResult>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class GetPostsResult
    {
        public List<PostDto>? Posts { get; set; }
        public int TotalPages { get; set; }
    }
}
