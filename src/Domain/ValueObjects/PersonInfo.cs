namespace Domain.ValueObjects;

public record PersonInfo
{
    public PersonInfo(string firstName, string lastName, DateTimeOffset birthDate)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentException("First name cannot be empty.", nameof(firstName));
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException("Last name cannot be empty.", nameof(lastName));
        }

        if (birthDate > DateTimeOffset.UtcNow.AddYears(-18))
        {
            throw new ArgumentException("Person must be at least 18 years old.", nameof(birthDate));
        }

        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
    }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public DateTimeOffset BirthDate { get; init; }

    public string Name => FirstName + " " + LastName;

    public int Age
    {
        get
        {
            var currentDate = DateTime.UtcNow;
            var birthDate = BirthDate.ToUniversalTime();

            var age = currentDate.Year - birthDate.Year;

            if (birthDate.AddYears(age) < currentDate)
            {
                age--;
            }

            return age;
        }
    }
}