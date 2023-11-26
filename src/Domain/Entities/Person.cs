#region

using Domain.Common;

#endregion

namespace Domain.Entities;

public sealed class Person : BaseEntity
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public DateTime BirthDate { get; set; }

    public int Age
    {
        get
        {
            DateTime currentDate = DateTime.UtcNow;
            DateTime birthDate = BirthDate.ToUniversalTime();

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