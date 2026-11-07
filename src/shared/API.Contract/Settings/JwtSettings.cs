namespace API.Contract.Settings
{
    public class JwtSettings
    {
        public string Issuer { get; init; } = string.Empty;
        public string Audience { get; init; } = string.Empty;
        public string MetadataAddress { get; init; } = string.Empty;
        public string? PublicKeyPem { get; init; }
    }
}
