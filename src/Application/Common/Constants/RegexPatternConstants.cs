namespace Application.Common.Constants;

public static class RegexPatternConstants
{
    public const string EmailPattern = @"^[\w\.-]+@[a-zA-Z\d\.-]{3,}\.[a-zA-Z]{2,}$";
    public const string UsernamePattern = @"^(?![\d_])[\w\d_]{8,16}$";
    public const string PasswordPattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[.,!@#$%^&*()_+])[A-Za-z\d.,!@#$%^&*()_+]{8,20}$";
    public const string PhoneNumberPattern = @"^(?!0)(\d{10}|\d{8})$";
}