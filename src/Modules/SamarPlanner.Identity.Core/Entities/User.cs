namespace SamarPlanner.Identity.Core.Entities;

public class User
{
    public const string TableName = "Users";

    public Guid Id { get; private init; }
    public string PhoneNumber { get; private set; } = string.Empty;

    public static User Create(Guid id, string phoneNumber) =>
        new()
        {
            Id = id,
            PhoneNumber = phoneNumber
        };

    public static User Create(string phoneNumber) =>
        new()
        {
            PhoneNumber = phoneNumber
        };
}