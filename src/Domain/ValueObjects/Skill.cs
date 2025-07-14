namespace Domain.ValueObjects;

public record Skill
{
    public string Name { get; init; }

    public Skill(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Skill name cannot be empty.", nameof(name));
        }

        Name = name;
    }
}