namespace SolaProcurementV2.Server.DataService
{
    public class AnalysisStructureService : BaseModelService<AnalysisStructure>
    {
        public async Task<(IEnumerable<AnalysisStructure> Result, string ReturnMessage)> GetAllAsync(BusinessUnit businessUnit, bool namesOnly)
        {
            string message = "";
            IEnumerable<AnalysisStructure> result = new List<AnalysisStructure>();
            if (businessUnit != null)
            {
                try
                {
                    using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                    {
                        var sqlId = $"SELECT st.Id, m.Id MenuId, m.Name MenuName, st.BusinessUnitId, st.AnalysisDimensionId01, st.AnalysisDimensionId02, st.AnalysisDimensionId03, st.AnalysisDimensionId04, st.AnalysisDimensionId05, st.AnalysisDimensionId06, st.AnalysisDimensionId07, st.AnalysisDimensionId08, st.AnalysisDimensionId09, st.AnalysisDimensionId10 FROM Menu m LEFT JOIN AnalysisStructure st ON m.Id = st.MenuId AND st.BusinessUnitId = {businessUnit.Id} WHERE m.IsFunction = 1";
                        var sqlNames = $"SELECT st.Id, m.Id MenuId, m.Name MenuName, st.BusinessUnitId, st.AnalysisDimensionId01, st.AnalysisDimensionId02, st.AnalysisDimensionId03, st.AnalysisDimensionId04, st.AnalysisDimensionId05, st.AnalysisDimensionId06, st.AnalysisDimensionId07, st.AnalysisDimensionId08, st.AnalysisDimensionId09, st.AnalysisDimensionId10, (SELECT Code FROM AnalysisDimension WHERE Id = st.AnalysisDimensionId01) AnalysisDimensionCode01, (SELECT Code FROM AnalysisDimension WHERE Id = st.AnalysisDimensionId02) AnalysisDimensionCode02, (SELECT Code FROM AnalysisDimension WHERE Id = st.AnalysisDimensionId03) AnalysisDimensionCode03, (SELECT Code FROM AnalysisDimension WHERE Id = st.AnalysisDimensionId04) AnalysisDimensionCode04, (SELECT Code FROM AnalysisDimension WHERE Id = st.AnalysisDimensionId05) AnalysisDimensionCode05, (SELECT Code FROM AnalysisDimension WHERE Id = st.AnalysisDimensionId06) AnalysisDimensionCode06, (SELECT Code FROM AnalysisDimension WHERE Id = st.AnalysisDimensionId07) AnalysisDimensionCode07, (SELECT Code FROM AnalysisDimension WHERE Id = st.AnalysisDimensionId08) AnalysisDimensionCode08, (SELECT Code FROM AnalysisDimension WHERE Id = st.AnalysisDimensionId09) AnalysisDimensionCode09, (SELECT Code FROM AnalysisDimension WHERE Id = st.AnalysisDimensionId10) AnalysisDimensionCode10 FROM Menu m LEFT JOIN AnalysisStructure st ON m.Id = st.MenuId AND st.BusinessUnitId = {businessUnit.Id} WHERE m.IsFunction = 1";
                        var sqlZero = "SELECT Id MenuId, Name MenuName FROM Menu WHERE IsFunction = 1";
                        var sql = (businessUnit.Id > 0 ? (namesOnly ? sqlNames : sqlId) : sqlZero);
                        result = await cn.QueryAsync<AnalysisStructure>(sql);
                    }
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
            return (result, message);
        }

    }
}
