namespace SamarPlanner.Identity.Core.Tests.Builders;

public class UserBuilder
{
    private Guid _id = Guid.NewGuid();
    private string? _phoneNumber = "09123456789";

    public UserBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public UserBuilder WithPhoneNumber(string? phoneNumber)
    {
        _phoneNumber = phoneNumber;
        return this;
    }

    public DomainResult<Entities.User> CreateResult()
    {
        return Entities.User.Create(_id, _phoneNumber);
    }
}