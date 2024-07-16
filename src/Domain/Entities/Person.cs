using Domain.Common;

namespace Domain.Entities;

public sealed class Person : BaseEntity
{
    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public DateTime BirthDate { get; init; }

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

    public string FullName => FirstName + " " + LastName;

    public User? User { get; set; }

    public Guid UserId { get; set; }
}