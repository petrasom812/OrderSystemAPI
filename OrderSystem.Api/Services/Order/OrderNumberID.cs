namespace OrderSystem.Api.Services
{

    public static class OrderNumberGenerator
    {
        public static string GenerateOrderNumber()
        {
            // Date part (easy to read)
            string date = DateTime.UtcNow.ToString("yyMMdd");

            // Short unique part (safe + simple)
            string unique = Guid.NewGuid().ToString("N")[..6].ToUpper();

            // Final format
            return $"ORD-{date}-{unique}";
        }
    }

}