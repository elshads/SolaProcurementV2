namespace SolaProcurementV2.Server.Data
{
    public class AnalysisStructure : TransactionModel
    {
        public AnalysisStructure() : base() { }

        public AnalysisStructure(AnalysisStructure instance) : base(instance)
        {
            MenuId = instance.MenuId;
            MenuName = instance.MenuName;
            AnalysisDimensionId01 = instance.AnalysisDimensionId01;
            AnalysisDimensionId02 = instance.AnalysisDimensionId02;
            AnalysisDimensionId03 = instance.AnalysisDimensionId03;
            AnalysisDimensionId04 = instance.AnalysisDimensionId04;
            AnalysisDimensionId05 = instance.AnalysisDimensionId05;
            AnalysisDimensionId06 = instance.AnalysisDimensionId06;
            AnalysisDimensionId07 = instance.AnalysisDimensionId07;
            AnalysisDimensionId08 = instance.AnalysisDimensionId08;
            AnalysisDimensionId09 = instance.AnalysisDimensionId09;
            AnalysisDimensionId10 = instance.AnalysisDimensionId10;

            AnalysisDimensionCode01 = instance.AnalysisDimensionCode01;
            AnalysisDimensionCode02 = instance.AnalysisDimensionCode02;
            AnalysisDimensionCode03 = instance.AnalysisDimensionCode03;
            AnalysisDimensionCode04 = instance.AnalysisDimensionCode04;
            AnalysisDimensionCode05 = instance.AnalysisDimensionCode05;
            AnalysisDimensionCode06 = instance.AnalysisDimensionCode06;
            AnalysisDimensionCode07 = instance.AnalysisDimensionCode07;
            AnalysisDimensionCode08 = instance.AnalysisDimensionCode08;
            AnalysisDimensionCode09 = instance.AnalysisDimensionCode09;
            AnalysisDimensionCode10 = instance.AnalysisDimensionCode10;
        }


        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public int AnalysisDimensionId01 { get; set; }
        public int AnalysisDimensionId02 { get; set; }
        public int AnalysisDimensionId03 { get; set; }
        public int AnalysisDimensionId04 { get; set; }
        public int AnalysisDimensionId05 { get; set; }
        public int AnalysisDimensionId06 { get; set; }
        public int AnalysisDimensionId07 { get; set; }
        public int AnalysisDimensionId08 { get; set; }
        public int AnalysisDimensionId09 { get; set; }
        public int AnalysisDimensionId10 { get; set; }

        public string AnalysisDimensionCode01 { get; set; }
        public string AnalysisDimensionCode02 { get; set; }
        public string AnalysisDimensionCode03 { get; set; }
        public string AnalysisDimensionCode04 { get; set; }
        public string AnalysisDimensionCode05 { get; set; }
        public string AnalysisDimensionCode06 { get; set; }
        public string AnalysisDimensionCode07 { get; set; }
        public string AnalysisDimensionCode08 { get; set; }
        public string AnalysisDimensionCode09 { get; set; }
        public string AnalysisDimensionCode10 { get; set; }
    }
}
