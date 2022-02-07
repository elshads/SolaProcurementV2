using System.Data;

namespace SolaProcurementV2.Server.DataService
{
    public class UserGroupService : BaseModelService<UserGroup>
    {
        public new async Task<(IEnumerable<UserGroup> Result, string ReturnMessage)> GetAllAsync(int currentUserId, bool indexed)
        {
            string message = "";
            IEnumerable<UserGroup> result = new List<UserGroup>();
            try
            {
                using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                {
                    var sql = $"SELECT g.*, (SELECT COUNT(Id) FROM UserGroupAppUser WHERE UserGroupId = g.Id) AppUserCount, (SELECT COUNT(Id) FROM UserGroupMenu WHERE UserGroupId = g.Id) MenuCount, (SELECT COUNT(Id) FROM UserGroupBusinessUnit WHERE UserGroupId = g.Id) BusinessUnitCount, (SELECT COUNT(Id) FROM UserGroupApproveRole WHERE UserGroupId = g.Id) ApproveRoleCount FROM UserGroup g LEFT JOIN UserGroupAppUser gu ON g.Id = gu.UserGroupId LEFT JOIN UserGroupMenu gs ON g.Id = gs.UserGroupId LEFT JOIN UserGroupBusinessUnit gb ON g.Id = gb.UserGroupId LEFT JOIN UserGroupApproveRole gr ON g.Id = gr.UserGroupId GROUP BY g.Id, g.Code, g.Name, g.Description, g.StatusId, g.CreatedOn, g.CreatedBy, g.UpdatedOn, g.UpdatedBy";
                    var _result = await cn.QueryAsync<UserGroup>(sql);
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

        public new async Task<UserGroup> GetByIdAsync(int modelId)
        {
            var result = new UserGroup();
            try
            {
                using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                {
                    var sql = $"SELECT * FROM UserGroup WHERE Id = {modelId}";
                    var _result = await cn.QueryAsync<UserGroup>(sql);
                    if (_result != null)
                    {
                        result = _result.FirstOrDefault();
                    }
                }

                if (result != null)
                {
                    using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                    {
                        var sql = $"SELECT x.* FROM BusinessUnit x INNER JOIN UserGroupBusinessUnit ug ON x.Id = ug.BusinessUnitId AND ug.UserGroupId = {modelId}";
                        var _result = await cn.QueryAsync<BusinessUnit>(sql);
                        if (_result != null)
                        {
                            result.BusinessUnitList = _result.ToList();
                        }
                    }

                    using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                    {
                        var sql = $"SELECT x.*, ug.CreateAccess, ug.ReadAccess, ug.UpdateAccess, ug.DeleteAccess FROM Menu x INNER JOIN UserGroupMenu ug ON x.Id = ug.MenuId AND ug.UserGroupId = {modelId}";
                        var _result = await cn.QueryAsync<Menu>(sql);
                        if (_result != null)
                        {
                            result.MenuList = _result.ToList();
                        }
                    }

                    using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                    {
                        var sql = $"SELECT x.* FROM AspNetUsers x INNER JOIN UserGroupAppUser ug ON x.Id = ug.AppUserId AND ug.UserGroupId = {modelId}";
                        var _result = await cn.QueryAsync<AppUser>(sql);
                        if (_result != null)
                        {
                            result.AppUserList = _result.ToList();
                        }
                    }

                    using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                    {
                        var sql = $"SELECT x.* FROM ApproveRole x INNER JOIN UserGroupApproveRole ug ON x.Id = ug.ApproveRoleId AND ug.UserGroupId = {modelId}";
                        var _result = await cn.QueryAsync<ApproveRole>(sql);
                        if (_result != null)
                        {
                            result.ApproveRoleList = _result.ToList();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                result.ReturnMessage = e.Message;
            }
            return result;
        }

        public async Task<UserGroup> GetByCodeAsync(string code)
        {
            var result = new UserGroup();
            try
            {
                using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                {
                    var sql = $"SELECT * FROM UserGroup WHERE Code = '{code}'";
                    var _result = await cn.QueryAsync<UserGroup>(sql);
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

        public async Task<(IEnumerable<UserGroup> Result, string ReturnMessage)> GetUserGroupAsync(int userId)
        {
            IEnumerable<UserGroup> result = new List<UserGroup>();
            var message = "";
            try
            {
                using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                {
                    var sql = $"SELECT g.* FROM UserGroup g INNER JOIN UserGroupAppUser ug ON g.Id = ug.UserGroupId AND ug.AppUserId = {userId}";
                    var _result = await cn.QueryAsync<UserGroup>(sql);
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

        public async Task<SqlResult> CreateAsync(UserGroup userGroup, int currentUserId)
        {
            var result = new SqlResult();
            var groupUserResult = 0;
            var groupBuResult = 0;
            var groupMenuResult = 0;
            var groupRoleResult = 0;
            try
            {
                var p = new DynamicParameters();
                p.Add("Code", userGroup.Code, DbType.String, ParameterDirection.Input);
                p.Add("Name", userGroup.Name, DbType.String, ParameterDirection.Input);
                p.Add("Description", userGroup.Description, DbType.String, ParameterDirection.Input);
                p.Add("StatusId", userGroup.StatusId, DbType.Int32, ParameterDirection.Input);
                p.Add("CreatedOn", DateTime.Now, DbType.DateTime, ParameterDirection.Input);
                p.Add("CreatedBy", currentUserId, DbType.Int32, ParameterDirection.Input);
                p.Add("UpdatedOn", DateTime.Now, DbType.DateTime, ParameterDirection.Input);
                p.Add("UpdatedBy", currentUserId, DbType.Int32, ParameterDirection.Input);
                using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                {
                    var sql = $"INSERT INTO UserGroup (Code, Name, Description, StatusId, CreatedOn, CreatedBy, UpdatedOn, UpdatedBy) VALUES (@Code, @Name, @Description, @StatusId, @CreatedOn, @CreatedBy, @UpdatedOn, @UpdatedBy)";
                    result.InsertedResult = await cn.ExecuteAsync(sql, p, commandType: CommandType.Text);
                }

                if (result.InsertedResult > 0 && userGroup.AppUserList.Count() > 0)
                {
                    foreach (var item in userGroup.AppUserList)
                    {
                        var d = new DynamicParameters();
                        d.Add("UserGroupId", userGroup.Id, DbType.Int32, ParameterDirection.Input);
                        d.Add("AppUserId", item.Id, DbType.Int32, ParameterDirection.Input);
                        d.Add("Description", item.Description, DbType.String, ParameterDirection.Input);
                        using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                        {
                            var sql = $"INSERT INTO UserGroupAppUser (UserGroupId, AppUserId, Description) VALUES (@UserGroupId, @AppUserId, @Description)";
                            groupUserResult = await cn.ExecuteAsync(sql, d, commandType: CommandType.Text);
                        }
                    }
                }

                if (result.InsertedResult > 0 && userGroup.BusinessUnitList.Count() > 0)
                {
                    foreach (var item in userGroup.BusinessUnitList)
                    {
                        var d = new DynamicParameters();
                        d.Add("UserGroupId", userGroup.Id, DbType.Int32, ParameterDirection.Input);
                        d.Add("BusinessUnitId", item.Id, DbType.Int32, ParameterDirection.Input);
                        d.Add("Description", item.Description, DbType.String, ParameterDirection.Input);
                        using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                        {
                            var sql = $"INSERT INTO UserGroupBusinessUnit (UserGroupId, BusinessUnitId, Description) VALUES (@UserGroupId, @BusinessUnitId, @Description)";
                            groupBuResult = await cn.ExecuteAsync(sql, d, commandType: CommandType.Text);
                        }
                    }
                }

                if (result.InsertedResult > 0 && userGroup.MenuList.Count() > 0)
                {
                    foreach (var item in userGroup.MenuList)
                    {
                        var d = new DynamicParameters();
                        d.Add("UserGroupId", userGroup.Id, DbType.Int32, ParameterDirection.Input);
                        d.Add("MenuId", item.Id, DbType.Int32, ParameterDirection.Input);
                        d.Add("Description", item.Description, DbType.String, ParameterDirection.Input);
                        d.Add("CreateAccess", item.CreateAccess, DbType.Int32, ParameterDirection.Input);
                        d.Add("ReadAccess", item.ReadAccess, DbType.Int32, ParameterDirection.Input);
                        d.Add("UpdateAccess", item.UpdateAccess, DbType.Int32, ParameterDirection.Input);
                        d.Add("DeleteAccess", item.DeleteAccess, DbType.Int32, ParameterDirection.Input);
                        using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                        {
                            var sql = $"INSERT INTO UserGroupMenu (UserGroupId, MenuId, Description, CreateAccess, ReadAccess, UpdateAccess, DeleteAccess) VALUES (@UserGroupId, @MenuId, @Description, @CreateAccess, @ReadAccess, @UpdateAccess, @DeleteAccess)";
                            groupMenuResult = await cn.ExecuteAsync(sql, d, commandType: CommandType.Text);
                        }
                    }
                }

                if (result.InsertedResult > 0 && userGroup.ApproveRoleList.Count() > 0)
                {
                    foreach (var item in userGroup.ApproveRoleList)
                    {
                        var d = new DynamicParameters();
                        d.Add("UserGroupId", userGroup.Id, DbType.Int32, ParameterDirection.Input);
                        d.Add("ApproveRoleId", item.Id, DbType.Int32, ParameterDirection.Input);
                        d.Add("Description", item.Description, DbType.String, ParameterDirection.Input);
                        using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                        {
                            var sql = $"INSERT INTO UserGroupApproveRole (UserGroupId, ApproveRoleId, Description) VALUES (@UserGroupId, @ApproveRoleId, @Description)";
                            groupRoleResult = await cn.ExecuteAsync(sql, d, commandType: CommandType.Text);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                result.InsertedResultMessage = e.Message;
            }
            return result;
        }

        public async Task<SqlResult> UpdateAsync(UserGroup userGroup, int currentUserId)
        {
            var result = new SqlResult();
            var groupUserResult = 0;
            var groupBuResult = 0;
            var groupMenuResult = 0;
            var groupRoleResult = 0;
            var deletedResult = 0;
            try
            {
                var p = new DynamicParameters();
                p.Add("Id", userGroup.Id, DbType.Int32, ParameterDirection.Input);
                p.Add("Code", userGroup.Code, DbType.String, ParameterDirection.Input);
                p.Add("Name", userGroup.Name, DbType.String, ParameterDirection.Input);
                p.Add("Description", userGroup.Description, DbType.String, ParameterDirection.Input);
                p.Add("StatusId", userGroup.StatusId, DbType.Int32, ParameterDirection.Input);
                p.Add("UpdatedOn", DateTime.Now, DbType.DateTime, ParameterDirection.Input);
                p.Add("UpdatedBy", currentUserId, DbType.Int32, ParameterDirection.Input);
                using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                {
                    var sql = $"UPDATE UserGroup SET Code=@Code, Name=@Name, Description=@Description, StatusId=@StatusId, UpdatedOn=@UpdatedOn, UpdatedBy=@UpdatedBy WHERE Id = @Id";
                    result.UpdatedResult = await cn.ExecuteAsync(sql, p, commandType: CommandType.Text);
                }

                if (result.UpdatedResult > 0)
                {
                    using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                    {
                        var sql = $"DELETE FROM UserGroupAppUser WHERE UserGroupId = {userGroup.Id};" +
                        $"DELETE FROM UserGroupBusinessUnit WHERE UserGroupId = {userGroup.Id};" +
                        $"DELETE FROM UserGroupMenu WHERE UserGroupId = {userGroup.Id};" +
                        $"DELETE FROM UserGroupApproveRole WHERE UserGroupId = {userGroup.Id};";
                        deletedResult = await cn.ExecuteAsync(sql);
                    }
                }

                if (result.UpdatedResult > 0 && userGroup.AppUserList.Count() > 0)
                {
                    foreach (var item in userGroup.AppUserList)
                    {
                        var d = new DynamicParameters();
                        d.Add("UserGroupId", userGroup.Id, DbType.Int32, ParameterDirection.Input);
                        d.Add("AppUserId", item.Id, DbType.Int32, ParameterDirection.Input);
                        d.Add("Description", item.Description, DbType.String, ParameterDirection.Input);
                        using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                        {
                            var sql = $"INSERT INTO UserGroupAppUser (UserGroupId, AppUserId, Description) VALUES (@UserGroupId, @AppUserId, @Description)";
                            groupUserResult += await cn.ExecuteAsync(sql, d, commandType: CommandType.Text);
                        }
                    }
                }

                if (result.UpdatedResult > 0 && userGroup.BusinessUnitList.Count() > 0)
                {
                    foreach (var item in userGroup.BusinessUnitList)
                    {
                        var d = new DynamicParameters();
                        d.Add("UserGroupId", userGroup.Id, DbType.Int32, ParameterDirection.Input);
                        d.Add("BusinessUnitId", item.Id, DbType.Int32, ParameterDirection.Input);
                        d.Add("Description", item.Description, DbType.String, ParameterDirection.Input);
                        using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                        {
                            var sql = $"INSERT INTO UserGroupBusinessUnit (UserGroupId, BusinessUnitId, Description) VALUES (@UserGroupId, @BusinessUnitId, @Description)";
                            groupBuResult += await cn.ExecuteAsync(sql, d, commandType: CommandType.Text);
                        }
                    }
                }

                if (result.UpdatedResult > 0 && userGroup.MenuList.Count() > 0)
                {
                    foreach (var item in userGroup.MenuList)
                    {
                        var d = new DynamicParameters();
                        d.Add("UserGroupId", userGroup.Id, DbType.Int32, ParameterDirection.Input);
                        d.Add("MenuId", item.Id, DbType.Int32, ParameterDirection.Input);
                        d.Add("Description", item.Description, DbType.String, ParameterDirection.Input);
                        d.Add("CreateAccess", item.CreateAccess, DbType.Int32, ParameterDirection.Input);
                        d.Add("ReadAccess", item.ReadAccess, DbType.Int32, ParameterDirection.Input);
                        d.Add("UpdateAccess", item.UpdateAccess, DbType.Int32, ParameterDirection.Input);
                        d.Add("DeleteAccess", item.DeleteAccess, DbType.Int32, ParameterDirection.Input);
                        using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                        {
                            var sql = $"INSERT INTO UserGroupMenu (UserGroupId, MenuId, Description, CreateAccess, ReadAccess, UpdateAccess, DeleteAccess) VALUES (@UserGroupId, @MenuId, @Description, @CreateAccess, @ReadAccess, @UpdateAccess, @DeleteAccess)";
                            groupMenuResult += await cn.ExecuteAsync(sql, d, commandType: CommandType.Text);
                        }
                    }
                }

                if (result.UpdatedResult > 0 && userGroup.ApproveRoleList.Count() > 0)
                {
                    foreach (var item in userGroup.ApproveRoleList)
                    {
                        var d = new DynamicParameters();
                        d.Add("UserGroupId", userGroup.Id, DbType.Int32, ParameterDirection.Input);
                        d.Add("ApproveRoleId", item.Id, DbType.Int32, ParameterDirection.Input);
                        d.Add("Description", item.Description, DbType.String, ParameterDirection.Input);
                        using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                        {
                            var sql = $"INSERT INTO UserGroupApproveRole (UserGroupId, ApproveRoleId, Description) VALUES (@UserGroupId, @ApproveRoleId, @Description)";
                            groupRoleResult += await cn.ExecuteAsync(sql, d, commandType: CommandType.Text);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                result.UpdatedResultMessage = e.Message;
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
                    var sql = $"DELETE FROM UserGroupAppUser WHERE UserGroupId = {deletedId};" +
                    $"DELETE FROM UserGroupBusinessUnit WHERE UserGroupId = {deletedId};" +
                    $"DELETE FROM UserGroupMenu WHERE UserGroupId = {deletedId};" +
                    $"DELETE FROM UserGroupApproveRole WHERE UserGroupId = {deletedId};";
                    await cn.ExecuteAsync(sql);
                }

                using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                {
                    var sql = $"DELETE FROM UserGroup WHERE Id = {deletedId};";
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
            try
            {
                foreach (var item in deletedIdList)
                {
                    using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                    {
                        var sql = $"DELETE FROM UserGroupAppUser WHERE UserGroupId = {item};" +
                        $"DELETE FROM UserGroupBusinessUnit WHERE UserGroupId = {item};" +
                        $"DELETE FROM UserGroupMenu WHERE UserGroupId = {item};" +
                        $"DELETE FROM UserGroupApproveRole WHERE UserGroupId = {item};";
                        await cn.ExecuteAsync(sql);
                    }

                    using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                    {
                        var sql = $"DELETE FROM UserGroup WHERE Id = {item};";
                        result.DeletedResult = await cn.ExecuteAsync(sql);
                    }
                }
            }
            catch (Exception e)
            {
                result.DeletedResultMessage = e.Message;
            }
            return result;
        }
    }
}
