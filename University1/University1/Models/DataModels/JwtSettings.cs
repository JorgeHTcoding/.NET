namespace University1.Models.DataModels
{
    public class JwtSettings
    {
        public bool ValidateUserSignKey { get; set; }
        public string IssueSigningKey { get; set; } = string.Empty;  
        public bool ValidateIssuer { get; set; }
        public string? ValidIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public string? ValidAudience { get; set; }
        public bool RequireExpiration { get; set; }
        public bool ValidateLifeTime { get; set; }


    }
}
