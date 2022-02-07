namespace SolaProcurementV2.Server.DataService
{
    public class StatusService : BaseModelService<Status>
    {
        public async Task<IEnumerable<Status>> GetAllAsync()
        {
            IEnumerable<Status> result = new List<Status>();
            try
            {
                using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                {
                    var sql = $"SELECT * FROM Status";
                    result = await cn.QueryAsync<Status>(sql);
                }
            }
            catch (Exception e)
            {
                var message = e.Message;
            }
            return result;
        }
    }
}
