using FSPBook.Application.Commands;
using FSPBook.Data.Entities;
using FSPBook.Data.Interfaces;
using MediatR;

namespace FSPBook.Application.Commands
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, int>
    {
        private readonly IPostRepository _postRepository;

        public CreatePostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = new Post
            {
                AuthorId = request.AuthorId,
                Content = request.Content,
                DateTimePosted = DateTimeOffset.Now
            };

            await _postRepository.AddAsync(post);
            return post.Id;
        }
    }
}
