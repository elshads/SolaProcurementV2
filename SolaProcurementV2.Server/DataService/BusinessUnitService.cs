
using System.Data;

namespace SolaProcurementV2.Server.DataService
{
    public class BusinessUnitService : BaseModelService<BusinessUnit>
    {
        public async Task<(IEnumerable<BusinessUnit> Result, string ReturnMessage)> GetAllOpenAsync(AppUser currentUser, bool indexed)
        {
            string message = "";
            IEnumerable<BusinessUnit> result = new List<BusinessUnit>();
            try
            {
                using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                {
                    var sqlAdmin = $"SELECT * FROM BusinessUnit WHERE StatusId = 0";
                    var sqlUser = $"SELECT b.* FROM BusinessUnit b INNER JOIN UserGroupBusinessUnit gb ON b.Id = gb.BusinessUnitId INNER JOIN UserGroup g ON gb.UserGroupId = g.Id INNER JOIN UserGroupAppUser gu ON g.Id = gu.UserGroupId AND gu.AppUserId = {currentUser.Id} AND b.StatusId = 0 GROUP BY b.Id, b.Code, b.Name, b.Description, b.StatusId, b.CreatedOn, b.CreatedBy, b.UpdatedOn, b.UpdatedBy";
                    var _result = await cn.QueryAsync<BusinessUnit>(currentUser.Administrator ? sqlAdmin : sqlUser);
                    if (_result != null && indexed)
                    {
                        result = IndexList(_result);
                    }
                    else if (_result != null && !indexed)
                    {
                        result = _result;
                    }
                }
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return (result, message);
        }

        public (IEnumerable<BusinessUnit> Result, string ReturnMessage) GetAllOpen(AppUser currentUser, bool indexed)
        {
            string message = "";
            IEnumerable<BusinessUnit> result = new List<BusinessUnit>();
            try
            {
                using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                {
                    var sqlAdmin = $"SELECT * FROM BusinessUnit WHERE StatusId = 0";
                    var sqlUser = $"SELECT b.* FROM BusinessUnit b INNER JOIN UserGroupBusinessUnit gb ON b.Id = gb.BusinessUnitId INNER JOIN UserGroup g ON gb.UserGroupId = g.Id INNER JOIN UserGroupAppUser gu ON g.Id = gu.UserGroupId AND gu.AppUserId = {currentUser.Id} AND b.StatusId = 0 GROUP BY b.Id, b.Code, b.Name, b.Description, b.StatusId, b.CreatedOn, b.CreatedBy, b.UpdatedOn, b.UpdatedBy";
                    var _result = cn.Query<BusinessUnit>(currentUser.Administrator ? sqlAdmin : sqlUser);
                    if (_result != null && indexed)
                    {
                        result = IndexList(_result);
                    }
                    else if (_result != null && !indexed)
                    {
                        result = _result;
                    }
                }
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return (result, message);
        }

        public async Task<(IEnumerable<BusinessUnit> Result, string ReturnMessage)> GetAllAsync()
        {
            string message = "";
            IEnumerable<BusinessUnit> result = new List<BusinessUnit>();
            try
            {
                using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                {
                    var sql = $"SELECT * FROM BusinessUnit";
                    var _result = await cn.QueryAsync<BusinessUnit>(sql);
                    if (_result != null)
                    {
                        result = _result;
                    }
                }
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return (result, message);
        }

        public async Task<BusinessUnit> GetByCodeAsync(string code)
        {
            var result = new BusinessUnit();
            try
            {
                using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                {
                    var sql = $"SELECT * FROM BusinessUnit WHERE Code = '{code}'";
                    var _result = await cn.QueryAsync<BusinessUnit>(sql);
                    if (_result != null)
                    {
                        result = _result.FirstOrDefault();
                    }
                }
            }
            catch (Exception e)
            {
                result.ReturnMessage = e.Message;
            }
            return result;
        }

        public async Task<SqlResult> DeleteAsync(int deletedId)
        {
            var result = new SqlResult();
            try
            {
                using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                {
                    var sql = $"DELETE FROM BusinessUnit WHERE Id = {deletedId};";
                    result.DeletedResult = await cn.ExecuteAsync(sql);
                }
            }
            catch (Exception e)
            {
                result.DeletedResultMessage = e.Message;
            }
            return result;
        }

        public async Task<SqlResult> DeleteListAsync(IEnumerable<int> deletedIdList)
        {
            var result = new SqlResult();
            var sql = "";
            try
            {
                foreach (var item in deletedIdList)
                {
                    sql += $"DELETE FROM BusinessUnit WHERE Id = {item}; ";
                }
                using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                {
                    result.DeletedResult = await cn.ExecuteAsync(sql);
                }
            }
            catch (Exception e)
            {
                result.DeletedResultMessage = e.Message;
            }
            return result;
        }

        public async Task<SqlResult> CreateAsync(BusinessUnit businessUnit, int currentUserId)
        {
            var result = new SqlResult();
            try
            {
                var p = new DynamicParameters();
                p.Add("Code", businessUnit.Code, DbType.String, ParameterDirection.Input);
                p.Add("Name", businessUnit.Name, DbType.String, ParameterDirection.Input);
                p.Add("Description", businessUnit.Description, DbType.String, ParameterDirection.Input);
                p.Add("StatusId", businessUnit.StatusId, DbType.Int32, ParameterDirection.Input);
                p.Add("CreatedOn", DateTime.Now, DbType.DateTime, ParameterDirection.Input);
                p.Add("CreatedBy", currentUserId, DbType.Int32, ParameterDirection.Input);
                p.Add("UpdatedOn", DateTime.Now, DbType.DateTime, ParameterDirection.Input);
                p.Add("UpdatedBy", currentUserId, DbType.Int32, ParameterDirection.Input);
                using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                {
                    var sql = $"INSERT INTO BusinessUnit (Code, Name, Description, StatusId, CreatedOn, CreatedBy, UpdatedOn, UpdatedBy) VALUES (@Code, @Name, @Description, @StatusId, @CreatedOn, @CreatedBy, @UpdatedOn, @UpdatedBy)";
                    result.InsertedResult = await cn.ExecuteAsync(sql, p, commandType: CommandType.Text);
                }
            }
            catch (Exception e)
            {
                result.InsertedResultMessage = e.Message;
            }
            return result;
        }

        public async Task<SqlResult> UpdateAsync(BusinessUnit businessUnit, int currentUserId)
        {
            var result = new SqlResult();
            try
            {
                var p = new DynamicParameters();
                p.Add("Id", businessUnit.Id, DbType.Int32, ParameterDirection.Input);
                p.Add("Code", businessUnit.Code, DbType.String, ParameterDirection.Input);
                p.Add("Name", businessUnit.Name, DbType.String, ParameterDirection.Input);
                p.Add("Description", businessUnit.Description, DbType.String, ParameterDirection.Input);
                p.Add("StatusId", businessUnit.StatusId, DbType.Int32, ParameterDirection.Input);
                p.Add("UpdatedOn", DateTime.Now, DbType.DateTime, ParameterDirection.Input);
                p.Add("UpdatedBy", currentUserId, DbType.Int32, ParameterDirection.Input);
                using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                {
                    var sql = $"UPDATE BusinessUnit SET Code=@Code, Name=@Name, Description=@Description, StatusId=@StatusId, UpdatedOn=@UpdatedOn, UpdatedBy=@UpdatedBy WHERE Id = @Id";
                    result.UpdatedResult = await cn.ExecuteAsync(sql, p, commandType: CommandType.Text);
                }
            }
            catch (Exception e)
            {
                result.UpdatedResultMessage = e.Message;
            }
            return result;
        }
    }
}
