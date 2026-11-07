namespace API.Contract.Settings
{
    public sealed class AppSettings
    {
        public string AppName { get; init; } = string.Empty;
        public string AppVersion { get; init; } = string.Empty;

        public JwtSettings Jwt { get; init; } = new();
        public AuditLogSettings AuditLog { get; init; } = new();
        public DatabaseSettings Database { get; init; } = new();
    }
}
