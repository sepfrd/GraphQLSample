namespace Infrastructure.Common.Configurations;

public record JwtOptions
{
    public required string PublicKey { get; set; }

    public required string PrivateKey { get; set; }

    public required string Issuer { get; set; }

    public required string Audience { get; set; }

    public required double TokenExpirationDurationMinutes { get; set; }
}