using FSPBook.Data.Entities;
using FSPBook.Data.Interfaces;
using FSPBook.Presentation.Pages.Profile;
using FSPBook.Tests.Builders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;

public class ProfilePageTests
{
    [Fact]
    public async Task OnGetAsync_ShouldPopulateProfileAndPosts_WhenValidIdIsProvided()
    {
        // Arrange
        var profileMock = new Mock<IProfileRepository>();
        var postMock = new Mock<IPostRepository>();

        var testProfile = new ProfileBuilder()
            .WithId(1)
            .WithFirstName("John")
            .WithLastName("Doe")
            .WithJobTitle("Developer")
            .Build();

        var testPosts = new List<Post>
        {
            new PostBuilder()
                .WithId(1)
                .WithContent("Post 1")
                .WithAuthorId(1)
                .WithAuthor(testProfile)
                .WithDateTimePosted(DateTimeOffset.Now)
                .Build(),
        
            new PostBuilder()
                .WithId(2)
                .WithContent("Post 2")
                .WithAuthorId(1)
                .WithAuthor(testProfile)
                .WithDateTimePosted(DateTimeOffset.Now.AddMinutes(-10))
                .Build()
        };

        profileMock
            .Setup(repo => repo.GetProfileByIdAsync(1))
            .ReturnsAsync(new Profile
            {
                Id = testProfile.Id,
                FirstName = testProfile.FirstName,
                LastName = testProfile.LastName,
                JobTitle = testProfile.JobTitle
            });

        postMock
            .Setup(repo => repo.GetPostsByAuthorIdAsync(1))
            .ReturnsAsync(testPosts);

        var pageModel = new IndexModel(profileMock.Object, postMock.Object);

        // Act
        var result = await pageModel.OnGetAsync(1);

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.NotNull(pageModel.Profile);
        Assert.NotEmpty(pageModel.Posts);
        Assert.Equal("John Doe", pageModel.Profile.FullName);
        Assert.Equal("Developer", pageModel.Profile.JobTitle);
        Assert.Equal(2, pageModel.Posts.Count);
        Assert.Equal("Post 1", pageModel.Posts.First().Content);
    }

    [Fact]
    public async Task OnGetAsync_ShouldReturnNotFound_WhenProfileDoesNotExist()
    {
        // Arrange
        var profileMock = new Mock<IProfileRepository>();
        var postMock = new Mock<IPostRepository>();

        profileMock.Setup(repo => repo.GetProfileByIdAsync(999)).ReturnsAsync((Profile)null);

        var pageModel = new IndexModel(profileMock.Object, postMock.Object);

        // Act
        var result = await pageModel.OnGetAsync(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task OnGetAsync_ShouldHandleMultipleProfilesAndPosts()
    {
        // Arrange
        var profileMock = new Mock<IProfileRepository>();
        var postMock = new Mock<IPostRepository>();

        var testProfile1 = new ProfileBuilder()
            .WithId(1)
            .WithFirstName("Alice")
            .WithLastName("Smith")
            .WithJobTitle("Engineer")
            .Build();

        var testProfile2 = new ProfileBuilder()
            .WithId(2)
            .WithFirstName("Bob")
            .WithLastName("Brown")
            .WithJobTitle("Manager")
            .Build();

        var testPosts = new List<Post>
    {
        new PostBuilder()
            .WithId(1)
            .WithContent("Post by Alice")
            .WithAuthorId(1)
            .WithAuthor(testProfile1)
            .WithDateTimePosted(DateTimeOffset.Now)
            .Build(),

        new PostBuilder()
            .WithId(2)
            .WithContent("Post by Bob")
            .WithAuthorId(2)
            .WithAuthor(testProfile2)
            .WithDateTimePosted(DateTimeOffset.Now.AddMinutes(-5))
            .Build()
    };

        profileMock
            .Setup(repo => repo.GetProfileByIdAsync(1))
            .ReturnsAsync(new Profile
            {
                Id = testProfile1.Id,
                FirstName = testProfile1.FirstName,
                LastName = testProfile1.LastName,
                JobTitle = testProfile1.JobTitle
            });

        postMock
            .Setup(repo => repo.GetPostsByAuthorIdAsync(1))
            .ReturnsAsync(testPosts.Where(p => p.AuthorId == 1).ToList());

        var pageModel = new IndexModel(profileMock.Object, postMock.Object);

        // Act
        var result = await pageModel.OnGetAsync(1);

        // Assert
        Assert.IsType<PageResult>(result);
        Assert.Equal(1, pageModel.Profile.Id);
        Assert.Equal("Alice Smith", pageModel.Profile.FullName);
        Assert.Single(pageModel.Posts);
        Assert.Equal("Post by Alice", pageModel.Posts.First().Content);
    }

}
