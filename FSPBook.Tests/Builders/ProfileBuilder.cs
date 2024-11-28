using FSPBook.Data.Entities;

namespace FSPBook.Tests.Builders;
public class ProfileBuilder
{
    private int _id = 1;
    private string _firstName = "John";
    private string _lastName = "Doe";
    private string _jobTitle = "Developer";

    public ProfileBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public ProfileBuilder WithFirstName(string firstName)
    {
        _firstName = firstName;
        return this;
    }

    public ProfileBuilder WithLastName(string lastName)
    {
        _lastName = lastName;
        return this;
    }

    public ProfileBuilder WithJobTitle(string jobTitle)
    {
        _jobTitle = jobTitle;
        return this;
    }

    public Profile Build()
    {
        return new Profile
        {
            Id = _id,
            FirstName = _firstName,
            LastName = _lastName,
            JobTitle = _jobTitle
        };
    }
}
