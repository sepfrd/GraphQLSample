namespace Infrastructure.Common.Constants;

public static class RegexPatternConstants
{
    public const string UsernamePattern = @"^(?![\d_])[\w\d_]{8,16}$";
    public const string PasswordPattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[.,!@#$%^&*()_+])[A-Za-z\d.,!@#$%^&*()_+]{8,20}$";

    public const string UrlPattern =
        @"(https:\/\/www\.|http:\/\/www\.|https:\/\/|http:\/\/)?[a-zA-Z0-9]{2,}\.[a-zA-Z0-9]{2,}\.[a-zA-Z0-9]{2,}(\.[a-zA-Z0-9]{2,})?";

    public const string PhoneNumberPattern = @"^(?!0)(\d{10}|\d{8})$";
    public const string PostalCodePattern = "^[0-9]{5,10}$";
    public const string UnitNumberPattern = "^[0-9]{0,20}$";
}