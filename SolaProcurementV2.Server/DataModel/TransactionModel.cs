namespace SolaProcurementV2.Server.DataModel
{
    public class TransactionModel : BaseModel
    {
        public TransactionModel() : base() { }

        public TransactionModel(TransactionModel instance) : base(instance) 
        {
            LineNo = instance.LineNo;
            BusinessUnitId = instance.BusinessUnitId;
            CreatedOn = instance.CreatedOn;
            CreatedBy = instance.CreatedBy;
            UpdatedOn = instance.UpdatedOn;
            UpdatedBy = instance.UpdatedBy;

            AnalysisId01 = instance.AnalysisId01;
            AnalysisId02 = instance.AnalysisId02;
            AnalysisId03 = instance.AnalysisId03;
            AnalysisId04 = instance.AnalysisId04;
            AnalysisId05 = instance.AnalysisId05;
            AnalysisId06 = instance.AnalysisId06;
            AnalysisId07 = instance.AnalysisId07;
            AnalysisId08 = instance.AnalysisId08;
            AnalysisId09 = instance.AnalysisId09;
            AnalysisId10 = instance.AnalysisId10;

            LookupName01 = instance.LookupName01;
            LookupName02 = instance.LookupName02;
            LookupName03 = instance.LookupName03;
            LookupName04 = instance.LookupName04;
            LookupName05 = instance.LookupName05;
            LookupName06 = instance.LookupName06;
            LookupName07 = instance.LookupName07;
            LookupName08 = instance.LookupName08;
            LookupName09 = instance.LookupName09;
        }


        public int LineNo { get; set; }
        public int BusinessUnitId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }


        public int AnalysisId01 { get; set; }
        public int AnalysisId02 { get; set; }
        public int AnalysisId03 { get; set; }
        public int AnalysisId04 { get; set; }
        public int AnalysisId05 { get; set; }
        public int AnalysisId06 { get; set; }
        public int AnalysisId07 { get; set; }
        public int AnalysisId08 { get; set; }
        public int AnalysisId09 { get; set; }
        public int AnalysisId10 { get; set; }
        public string LookupName01 { get; set; }
        public string LookupName02 { get; set; }
        public string LookupName03 { get; set; }
        public string LookupName04 { get; set; }
        public string LookupName05 { get; set; }
        public string LookupName06 { get; set; }
        public string LookupName07 { get; set; }
        public string LookupName08 { get; set; }
        public string LookupName09 { get; set; }
        public string LookupName10 { get; set; }
    }
}
