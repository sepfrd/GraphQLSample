namespace Infrastructure.Common.Configurations;

public record JwtOptions
{
    public string? PublicKey { get; set; }

    public string? PrivateKey { get; set; }

    public double TokenExpirationDurationMinutes { get; set; }
}