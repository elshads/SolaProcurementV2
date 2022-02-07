namespace SolaProcurementV2.Server.Data
{
    public class AnalysisDimension : ReferenceModel
    {
        public AnalysisDimension() : base() { }

        public AnalysisDimension(AnalysisDimension instance) : base(instance)
        {
            Length = instance.Length;
            AnalysisList = instance.AnalysisList;
        }

        public int Length { get; set; }
        public IEnumerable<Analysis> AnalysisList { get; set; }
    }
}
