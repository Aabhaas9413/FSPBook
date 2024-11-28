using FSPBook.Infrastructure;
using FSPBook.Data.Entities;
using Microsoft.EntityFrameworkCore;
using FSPBook.Infrastructure.Repositories;

public class ProfileRepositoryTests
{
    private readonly DbContextOptions<Context> _dbContextOptions;

    public ProfileRepositoryTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
    }

    [Fact]
    public async Task GetProfileByIdAsync_ShouldReturnProfile_WhenProfileExists()
    {
        // Arrange
        using (var context = new Context(_dbContextOptions))
        {
            var profile = new Profile { Id = 1, FirstName = "John", LastName = "Doe", JobTitle = "Developer" };
            context.Profile.Add(profile);
            await context.SaveChangesAsync();
        }

        using (var context = new Context(_dbContextOptions))
        {
            var repository = new ProfileRepository(context);

            // Act
            var result = await repository.GetProfileByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John Doe", result.FullName);
            Assert.Equal("Developer", result.JobTitle);
        }
    }

    [Fact]
    public async Task GetProfileByIdAsync_ShouldReturnNull_WhenProfileDoesNotExist()
    {
        // Arrange
        using (var context = new Context(_dbContextOptions))
        {
            var repository = new ProfileRepository(context);

            // Act
            var result = await repository.GetProfileByIdAsync(999);

            // Assert
            Assert.Null(result);
        }
    }
}
