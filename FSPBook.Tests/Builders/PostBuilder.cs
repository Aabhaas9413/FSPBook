using FSPBook.Data.Entities;

namespace FSPBook.Tests.Builders;
public class PostBuilder
{
    private int _id = 1;
    private string _content = "Sample Post";
    private Profile _author = new Profile();
    private int _authorId = 0;
    private DateTimeOffset _dateTimePosted = DateTimeOffset.Now;

    public PostBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public PostBuilder WithContent(string content)
    {
        _content = content;
        return this;
    }

    public PostBuilder WithAuthor(Profile author)
    {
        _author = author;
        return this;
    }

    public PostBuilder WithAuthorId(int authorId)
    {
        _authorId = authorId;
        return this;
    }

    public PostBuilder WithDateTimePosted(DateTimeOffset dateTimePosted)
    {
        _dateTimePosted = dateTimePosted;
        return this;
    }

    public Post Build()
    {
        return new Post
        {
            Id = _id,
            Content = _content,
            AuthorId = _authorId,
            Author = _author,
            DateTimePosted = _dateTimePosted
        };
    }
}
