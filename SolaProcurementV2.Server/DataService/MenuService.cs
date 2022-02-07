
namespace SolaProcurementV2.Server.DataService
{
    public class MenuService : BaseModelService<Menu>
    {
        
        public async Task<(IEnumerable<Menu> Result, string ReturnMessage)> GetHierarchy(AppUser currentUser)
        {
            string message = "";
            IEnumerable<Menu> result = new List<Menu>();
            try
            {
                using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                {
                    var sqlAll = $"SELECT * FROM Menu";
                    var sqlUser = $"SELECT DISTINCT m.* FROM Menu m INNER JOIN UserGroupMenu gm ON m.Id = gm.MenuId INNER JOIN UserGroup g ON gm.UserGroupId = g.Id INNER JOIN UserGroupAppUser gu ON g.Id = gu.UserGroupId AND gm.ReadAccess = 1 AND gu.AppUserId = {currentUser.Id} ORDER BY Sequence";

                    var tempList = await cn.QueryAsync<Menu>(currentUser.Administrator ? sqlAll : sqlUser);
                    if (tempList != null && tempList.Count() > 0)
                    {
                        var treeList = GetParents(tempList);
                        result = treeList;
                    }
                }
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return (result, message);
        }

        public async Task<Menu> GetAccessLevel(AppUser currentUser, int menuId)
        {
            Menu result = new();
            if (currentUser.Administrator)
            {
                result = new Menu() { Id = menuId, CreateAccess = true, ReadAccess = true, UpdateAccess = true, DeleteAccess = true };
            }
            else
            {
                try
                {
                    using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                    {
                        var sql = $"SELECT MAX(gm.MenuId) Id, MAX(gm.CreateAccess) CreateAccess, MAX(gm.ReadAccess) ReadAccess, MAX(gm.UpdateAccess) UpdateAccess, MAX(gm.DeleteAccess) DeleteAccess FROM UserGroupMenu gm INNER JOIN UserGroup g ON gm.UserGroupId = g.Id INNER JOIN UserGroupAppUser gu ON g.Id = gu.UserGroupId AND gu.AppUserId = {currentUser.Id} AND gm.MenuId = {menuId}";
                        var _result = await cn.QueryAsync<Menu>(sql);
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
            }
            return result;
        }

        public async Task<(IEnumerable<Menu> Result, string ReturnMessage)> GetAccessLevelListAsync(AppUser currentUser)
        {
            var message = "";
            IEnumerable <Menu> result = new List<Menu>();
            try
            {
                using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                {
                    var sql = $"SELECT gm.MenuId Id, MAX(gm.CreateAccess) CreateAccess, MAX(gm.ReadAccess) ReadAccess, MAX(gm.UpdateAccess) UpdateAccess, MAX(gm.DeleteAccess) DeleteAccess FROM UserGroupMenu gm INNER JOIN UserGroup g ON gm.UserGroupId = g.Id INNER JOIN UserGroupAppUser gu ON g.Id = gu.UserGroupId AND gu.AppUserId = {currentUser.Id} GROUP BY gm.MenuId";
                    var _result = await cn.QueryAsync<Menu>(sql);
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

        public (IEnumerable<Menu> Result, string ReturnMessage) GetAccessLevelList(AppUser currentUser)
        {
            var message = "";
            IEnumerable<Menu> result = new List<Menu>();
            try
            {
                using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                {
                    var sql = $"SELECT gm.MenuId Id, MAX(gm.CreateAccess) CreateAccess, MAX(gm.ReadAccess) ReadAccess, MAX(gm.UpdateAccess) UpdateAccess, MAX(gm.DeleteAccess) DeleteAccess FROM UserGroupMenu gm INNER JOIN UserGroup g ON gm.UserGroupId = g.Id INNER JOIN UserGroupAppUser gu ON g.Id = gu.UserGroupId AND gu.AppUserId = {currentUser.Id} GROUP BY gm.MenuId";
                    var _result = cn.Query<Menu>(sql);
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


        IEnumerable<Menu> GetParents(IEnumerable<Menu> flatList)
        {
            var tempList = flatList.Where(e => e.ParentId == 0 || e.ParentId == null).Select(e => new Menu 
            {
                Id = e.Id,
                ParentId = e.ParentId,
                Code = e.Code,
                Name = e.Name,
                Url = e.Url,
                Icon = (e.Icon != null ? Icons.Filled.GetType().GetProperty(e.Icon)?.GetValue(Icons.Filled, null).ToString() : ""),
                Sequence = e.Sequence,
                Children = GetCildren(flatList, e.Id)
            });
            return tempList;
        }

        IEnumerable<Menu> GetCildren(IEnumerable<Menu> flatList, int parentId)
        {
            var tempList = flatList.Where(e => e.ParentId == parentId).Select(e => new Menu
            {
                Id = e.Id,
                ParentId = e.ParentId,
                Code = e.Code,
                Name = e.Name,
                Url = e.Url,
                Icon = (e.Icon != null ? Icons.Filled.GetType().GetProperty(e.Icon)?.GetValue(Icons.Filled, null).ToString() : ""),
                Sequence = e.Sequence,
                Children = GetCildren(flatList, e.Id)
            });
            return tempList;
        }

    }
}
