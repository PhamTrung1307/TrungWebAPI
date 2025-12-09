namespace Core.ConfigOptions
{
    public class JwtTokenSettings
    {
        public required string Key { get; set; }
        public required string Issuer { get; set; }
        public required int ExpireInHours { get; set; }
    }
}
