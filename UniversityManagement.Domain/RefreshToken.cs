namespace UniversityManagement.Domain
{
    public class RefreshToken
    {
        public string Token { get; set; } = default!;
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; }
        public string CreatedByIp { get; set; } = default!;
        public DateTime? Revoked { get; set; }
        public string? RevokedByIp { get; set; }
        public string? ReplacedByToken { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public bool IsActive => Revoked == null && !IsExpired;

        public RefreshToken() { }

        public RefreshToken(string token, DateTime expires, DateTime created, string createdByIp)
        {
            Token = token;
            Expires = expires;
            Created = created;
            CreatedByIp = createdByIp;
        }
    }
}
