using Domain.Common;

namespace Domain.Entities;

public sealed class Person : BaseEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime BirthDate { get; set; }

    public int Age
    {
        get
        {
            DateTime currentDate = DateTime.UtcNow;
            DateTime birthDate = BirthDate.ToUniversalTime();

            int age = currentDate.Year - birthDate.Year;

            if (birthDate.AddYears(age) < currentDate)
            {
                age--;
            }

            return age;
        }
    }
    public string FullName => FirstName + " " + LastName;
}