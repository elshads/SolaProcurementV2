namespace SolaProcurementV2.Server.Configurations
{
    public class SqlResult
    {
        public int InsertedResult { get; set; }
        public int UpdatedResult { get; set; }
        public int DeletedResult { get; set; }
        public string InsertedResultMessage { get; set; }
        public string UpdatedResultMessage { get; set; }
        public string DeletedResultMessage { get; set; }
        public int ReturnId { get; set; }
        public string ReturnCode { get; set; }

    }
}
