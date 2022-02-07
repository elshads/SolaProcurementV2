using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System.Data;

namespace SolaProcurementV2.Server.DataService
{
    public class AppUserService
    {
        public IEnumerable<AppUser> IndexList(IEnumerable<AppUser> dataList)
        {
            var result = new List<AppUser>();
            var index = 0;
            foreach (var item in dataList)
            {
                item.RowIndex = index;
                result.Add(item);
                index++;
            }
            return result;
        }

        public int GetNextRowIndex(IEnumerable<AppUser> dataList)
        {
            if (dataList.Count() > 0)
            {
                var maxRowIndex = dataList.Max(e => e.RowIndex);
                return maxRowIndex + 1;
            }
            else
            {
                return 0;
            }
        }

        public AppUser GetClonedInstance(AppUser instance)
        {
            return new AppUser(instance);
        }

        public async Task<(IEnumerable<AppUser> Result, string ReturnMessage)> GetAll(int currentUserId, bool indexed)
        {
            string message = "";
            IEnumerable<AppUser> result = new List<AppUser>();
            try
            {
                using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                {
                    var sql = $"SELECT * FROM AspNetUsers";
                    var _result = await cn.QueryAsync<AppUser>(sql);
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

        public async Task<SqlResult> Edit(AppUser appUser, int currentUserId)
        {
            var result = new SqlResult();
            try
            {
                using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                {
                    var sql = $"UPDATE AspNetUsers SET ThemeId = {appUser.ThemeId}, UpdatedBy = {currentUserId}, UpdatedOn = '{DateTime.Now}' WHERE Id = {appUser.Id}";
                    result.UpdatedResult = await cn.ExecuteAsync(sql);
                }
            }
            catch (Exception e)
            {
                result.UpdatedResultMessage = e.Message;
            }
            return result;
        }

        public async Task<AppUser> GetByIdAsync(int userId, int currentUserId)
        {
            var result = new AppUser();

            if (userId > 0)
            {
                try
                {
                    using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                    {
                        var sql = $"SELECT * FROM AspNetUsers WHERE Id = {userId}";
                        var _result = await cn.QueryAsync<AppUser>(sql);
                        result = _result.FirstOrDefault();
                    }
                }
                catch (Exception e)
                {
                    result.ReturnMessage = e.Message;
                }
            }
            return result;
        }

        public AppUser GetById(int userId)
        {
            var result = new AppUser();
            try
            {
                using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                {
                    var sql = $"SELECT * FROM AspNetUsers WHERE Id = {userId}";
                    result = cn.Query<AppUser>(sql).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                result.ReturnMessage = e.Message;
            }
            return result;
        }

        public async Task<AppUser> GetByUserNameAsync(string userName)
        {
            var result = new AppUser();
            try
            {
                using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                {
                    var sql = $"SELECT * FROM AspNetUsers WHERE UserName = '{userName}'";
                    var _result = await cn.QueryAsync<AppUser>(sql);
                    result = _result?.FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                result.ReturnMessage = e.Message;
            }
            return result;
        }

        
        public async Task<SqlResult> SetUserTheme(int themeId, int currentUserId)
        {
            var result = new SqlResult();
            try
            {
                using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                {
                    var sql = $"UPDATE AspNetUsers SET ThemeId = {themeId}, UpdatedBy = {currentUserId}, UpdatedOn = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}' WHERE Id = {currentUserId}";
                    result.UpdatedResult = await cn.ExecuteAsync(sql);
                }
            }
            catch (Exception e)
            {
                result.UpdatedResultMessage = e.Message; //INSERT INTO AppUser (AspNetUserId, UserName) VALUES (2, 'zaddd')
            }
            return result;
        }

        public async Task<SqlResult> CreateAsync(AppUser appUser, int currentUserId)
        {
            var result = new SqlResult();
            try
            {
                using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                {
                    var sql = $"INSERT INTO AppUser (AspNetUserId, UserName) VALUES ({appUser.Id}, '{appUser.UserName}')";
                    result.InsertedResult = await cn.ExecuteAsync(sql);
                }
            }
            catch (Exception e)
            {
                result.InsertedResultMessage = e.Message;
            }
            return result;
        }

        public async Task<SqlResult> DeleteAspNetUserAsync(int deletedId)
        {
            var result = new SqlResult();
            try
            {
                using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                {
                    var sql = $"DELETE FROM AspNetUsers WHERE Id = {deletedId}";
                    result.DeletedResult = await cn.ExecuteAsync(sql);
                }
            }
            catch (Exception e)
            {
                result.DeletedResultMessage = e.Message;
            }
            return result;
        }

        public async Task<SqlResult> UpdateAsync(AppUser model)
        {
            var result = new SqlResult();
            try
            {
                var p = new DynamicParameters();
                p.Add("Id", model.Id, DbType.Int32, ParameterDirection.Input);
                p.Add("FullName", model.FullName, DbType.String, ParameterDirection.Input);
                p.Add("Description", model.Description, DbType.String, ParameterDirection.Input);
                p.Add("ChangePassword", model.ChangePassword, DbType.Boolean, ParameterDirection.Input);
                p.Add("Administrator", model.Administrator, DbType.Boolean, ParameterDirection.Input);
                p.Add("StatusId", model.StatusId, DbType.Int32, ParameterDirection.Input);
                p.Add("ExpirationDate", model.ExpirationDate, DbType.DateTime, ParameterDirection.Input);
                p.Add("Photo", model.Photo, DbType.Binary, ParameterDirection.Input);
                p.Add("PhoneNumber", model.PhoneNumber, DbType.String, ParameterDirection.Input);
                p.Add("NotificationEmail", model.NotificationEmail, DbType.String, ParameterDirection.Input);
                p.Add("UpdatedOn", model.UpdatedOn, DbType.DateTime, ParameterDirection.Input);
                p.Add("UpdatedBy", model.UpdatedBy, DbType.Int32, ParameterDirection.Input);

                using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                {
                    var sql = $"UPDATE AspNetUsers SET FullName=@FullName, Description=@Description, ChangePassword=@ChangePassword, Administrator=@Administrator, StatusId=@StatusId, ExpirationDate=@ExpirationDate, Photo=@Photo, PhoneNumber=@PhoneNumber, NotificationEmail=@NotificationEmail, UpdatedOn=@UpdatedOn, UpdatedBy=@UpdatedBy WHERE Id=@Id";
                    result.UpdatedResult = await cn.ExecuteAsync(sql, p, commandType: CommandType.Text);
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
            var _result = 0;
            try
            {
                using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                {
                    var sql = $"DELETE FROM AppUser WHERE AspNetUserId = {deletedId}";
                    _result = await cn.ExecuteAsync(sql);
                }

                if (_result > 0)
                {
                    using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                    {
                        var sql = $"DELETE FROM AspNetUsers WHERE Id = {deletedId}";
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

        public async Task<SqlResult> DeleteListAsync(IEnumerable<int> deletedIdList)
        {
            var result = new SqlResult();
            var _result = 0;
            try
            {
                foreach (var deletedId in deletedIdList)
                {
                    using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                    {
                        var sql = $"DELETE FROM AppUser WHERE AspNetUserId = {deletedId}";
                        _result = await cn.ExecuteAsync(sql);
                    }

                    if (_result > 0)
                    {
                        using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                        {
                            var sql = $"DELETE FROM AspNetUsers WHERE Id = {deletedId}";
                            result.DeletedResult = await cn.ExecuteAsync(sql);
                        }
                    }
                    _result = 0;
                }
            }
            catch (Exception e)
            {
                result.DeletedResultMessage = e.Message;
            }
            return result;
        }

        public async Task<SqlResult> SetUserActiveAsync(int currentUserId)
        {
            var result = new SqlResult();
            if (currentUserId > 0)
            {
                try
                {
                    using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                    {
                        var sql = $"UPDATE AspNetUsers SET IsActive=(IsActive + 1), LastActivity='{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}' WHERE Id = {currentUserId}";
                        result.UpdatedResult = await cn.ExecuteAsync(sql);
                    }
                }
                catch (Exception e)
                {
                    result.UpdatedResultMessage = e.Message;
                }
            }
            return result;
        }

        public async Task<SqlResult> SetUserInactiveAsync(int currentUserId)
        {
            var result = new SqlResult();
            if (currentUserId > 0)
            {
                try
                {
                    using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                    {
                        var sql = $"UPDATE AspNetUsers SET IsActive=(IsActive - 1), LastActivity='{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}' WHERE Id = {currentUserId}";
                        result.UpdatedResult = await cn.ExecuteAsync(sql);
                    }
                }
                catch (Exception e)
                {
                    result.UpdatedResultMessage = e.Message;
                }
            }
            return result;
        }

        public void ExportToExcel(IEnumerable<AppUser> dataList)
        {
            using (var package = new ExcelPackage())
            {
                var sheet = package.Workbook.Worksheets.Add("export");
                sheet.Cells["A1"].LoadFromCollection(dataList, true);
                package.SaveAs(new FileInfo(@"Export"));
            }
        }

        public async Task<IdentityResult> ResetPasswordByIdAsync(int userId, string newPassword, UserManager<AppUser> userManager)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var result = await userManager.ResetPasswordAsync(user, token, newPassword);
            return result;
        }

        public async Task<IdentityResult> ResetPasswordByEmailAsync(string email, string newPassword, UserManager<AppUser> userManager)
        {
            var user = await userManager.FindByEmailAsync(email);
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var result = await userManager.ResetPasswordAsync(user, token, newPassword);
            return result;
        }
    }
}
