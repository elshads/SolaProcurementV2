
namespace SolaProcurementV2.Server.DataService
{
    public class BaseModelService<T> where T : BaseModel, new()
    {
        string table = typeof(T).Name;

        public T GetClonedInstance(T instance)
        {
            var constructorInfo = typeof(T).GetConstructor(new[] { typeof(T) });
            if (constructorInfo != null)
            {
                var parameters = new object[] { instance };
                return (T)constructorInfo.Invoke(parameters);
            }
            else
            {
                return new T();
            }
        }

        public IEnumerable<T> IndexList(IEnumerable<T> dataList)
        {
            var result = new List<T>();
            var index = 0;
            foreach (var item in dataList)
            {
                item.RowIndex = index;
                result.Add(item);
                index++;
            }
            return result;
        }

        public int GetNextRowIndex(IEnumerable<T> dataList)
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

        //public int GetNextLineNo(IEnumerable<T> dataList)
        //{
        //    var maxLineNo = dataList.Max(e => e.LineNo);
        //    return maxLineNo + 1;
        //}

        public async Task<T> GetByIdAsync(int modelId)
        {
            var result = new T();
            try
            {
                using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                {
                    var sql = $"SELECT * FROM {table} WHERE Id = {modelId}";
                    var _result = await cn.QueryAsync<T>(sql);
                    result = _result.FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                result.ReturnMessage = e.Message;
            }
            return result;
        }

        public async Task<(IEnumerable<T> Result, string ReturnMessage)> GetAllAsync(int currentUserId, bool indexed)
        {
            string message = "";
            IEnumerable<T> result = new List<T>();
            try
            {
                using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                {
                    var sql = $"SELECT * FROM {table}";
                    var _result = await cn.QueryAsync<T>(sql);
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

        public void ExportToExcel(IEnumerable<T> dataList)
        {
            using (var package = new ExcelPackage())
            {
                var sheet = package.Workbook.Worksheets.Add("export");
                sheet.Cells["A1"].LoadFromCollection(dataList, true);
                package.SaveAs(new FileInfo(@"Export"));
            }
        }
    }
}
