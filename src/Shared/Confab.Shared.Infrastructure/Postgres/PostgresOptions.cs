namespace Confab.Shared.Infrastructure.Postgres
{
    internal class PostgresOptions
    {
        public const string Postgres = "Postgres";
        public string ConnectionStrings { get; set; } = string.Empty;
    }
}
