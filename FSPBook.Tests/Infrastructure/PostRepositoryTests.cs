using FSPBook.Infrastructure.Repositories;
using FSPBook.Data.Entities;
using Microsoft.EntityFrameworkCore;
using FSPBook.Infrastructure;

public class PostRepositoryTests
{
    private readonly DbContextOptions<Context> _dbContextOptions;

    public PostRepositoryTests()
    {
       
    }

    [Fact]
    public async Task GetPostsByAuthorIdAsync_ShouldReturnPosts_WhenAuthorHasPosts()
    {
        var _dbContextOptions = new DbContextOptionsBuilder<Context>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique database
        .Options;
        // Arrange
        using (var context = new Context(_dbContextOptions))
        {
            var profile = new Profile { Id = 1, FirstName = "John", LastName = "Doe" };
            context.Profile.Add(profile);

            var posts = new List<Post>
            {
                new Post { Id = 1, Content = "Post 1", AuthorId = 1, DateTimePosted = DateTimeOffset.Now },
                new Post { Id = 2, Content = "Post 2", AuthorId = 1, DateTimePosted = DateTimeOffset.Now.AddMinutes(-10) }
            };
            context.Post.AddRange(posts);
            await context.SaveChangesAsync();
        }

        using (var context = new Context(_dbContextOptions))
        {
            var repository = new PostRepository(context);

            // Act
            var result = await repository.GetPostsByAuthorIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Post 1", result.First().Content);
            Assert.Equal("Post 2", result.Last().Content);
        }
    }

    [Fact]
    public async Task GetPostsByAuthorIdAsync_ShouldReturnEmptyList_WhenAuthorHasNoPosts()
    {
        var _dbContextOptions = new DbContextOptionsBuilder<Context>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique database
        .Options;
        // Arrange
        using (var context = new Context(_dbContextOptions))
        {
            var repository = new PostRepository(context);

            // Act
            var result = await repository.GetPostsByAuthorIdAsync(999);

            // Assert
            Assert.Empty(result);
        }
    }
}
