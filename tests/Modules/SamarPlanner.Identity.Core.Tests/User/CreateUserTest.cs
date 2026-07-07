namespace SamarPlanner.Identity.Core.Tests.User;

[Trait("Module", "Identity")]
[Trait("Layer", "Core")]
public class CreateUserTests
{
    private readonly UserBuilder _builder = new();

    public static TheoryData<string> InvalidLengthPhoneNumbers =>
        new() { "0912345678", "091234567890", "12345" };

    public static TheoryData<string> NoneEnglishDigitsOrInvalidCharacters =>
        new() { "0912345678a", "۰9123456789", "0912-345-67" };

    [Fact]
    public void Create_WithValidPhoneNumber_ShouldReturnSuccess()
    {
        var phoneNumber = "09123456789";

        var result = _builder.WithPhoneNumber(phoneNumber).CreateResult();

        var response = result.ShouldBeSuccess();
        using (new AssertionScope())
        {
            response.Id.Should().NotBeEmpty();
            response.PhoneNumber.Should().Be(phoneNumber);
        }
    }

    [Theory]
    [MemberData(nameof(StringTestData.NullOrWhiteSpace), MemberType = typeof(StringTestData))]
    public void Create_WithNullOrWithSpacePhoneNumber_ShouldFail(string? phoneNumber)
    {
        var result = _builder.WithPhoneNumber(phoneNumber).CreateResult();
        result.ShouldBeFailure();
    }

    [Theory]
    [MemberData(nameof(InvalidLengthPhoneNumbers))]
    public void Create_WithInvalidLengthPhoneNumber_ShouldFail(string phoneNumber)
    {
        var result = _builder.WithPhoneNumber(phoneNumber).CreateResult();
        result.ShouldBeFailure("11 رقم");
    }

    [Theory]
    [MemberData(nameof(NoneEnglishDigitsOrInvalidCharacters))]
    public void Create_WithNonEnglishDigitsOrInvalidCharactersPhoneNumber_ShouldFail(string phoneNumber)
    {
        var result = _builder.WithPhoneNumber(phoneNumber).CreateResult();
        result.ShouldBeFailure("اعداد");
    }

    [Fact]
    public void Create_WithEmptyGuid_ShouldFail()
    {
        var result = _builder.WithId(Guid.Empty).CreateResult();
        result.ShouldBeFailure("شناسه");
    }
}