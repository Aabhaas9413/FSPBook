using MediatR;

namespace FSPBook.Application.Commands
{
    public class CreatePostCommand : IRequest<int>
    {
        public int AuthorId { get; set; }
        public string? Content { get; set; }
    }
}
