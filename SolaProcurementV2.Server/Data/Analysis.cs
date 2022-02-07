namespace SolaProcurementV2.Server.Data
{
    public class Analysis : ReferenceModel
    {
        public Analysis() : base() { }

        public Analysis(Analysis instance) : base(instance)
        {
            AnalysisDimensionId = instance.AnalysisDimensionId;
        }

        public int AnalysisDimensionId { get; set; }
    }
}
