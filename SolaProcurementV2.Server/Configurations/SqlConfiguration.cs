namespace SolaProcurementV2.Server.Configurations
{
    public class SqlConfiguration
    {
        public string ConnectionString { get; }
        public static string StaticConnectionString { get; private set; }
        public SqlConfiguration(string connectionString)
        {
            ConnectionString = connectionString;
            StaticConnectionString = connectionString;
        }
    }
}
