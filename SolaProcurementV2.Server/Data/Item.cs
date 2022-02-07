namespace SolaProcurementV2.Server.Data
{
    public class Item : ReferenceModel
    {
        public Item() : base() { }

        public Item(Item instance) : base(instance)
        {
            LongName = instance.LongName;
            DefaultLocation = instance.DefaultLocation;
            DefaultUnit = instance.DefaultUnit;
            ItemTypeId = instance.ItemTypeId;
            AccountCode = instance.AccountCode;
            MinQuantity = instance.MinQuantity;
            MaxQuantity = instance.MaxQuantity;
            Barcode = instance.Barcode;
            Images = instance.Images;
            AnalysisCode01 = instance.AnalysisCode01;
            AnalysisCode02 = instance.AnalysisCode02;
            AnalysisCode03 = instance.AnalysisCode03;
            AnalysisCode04 = instance.AnalysisCode04;
            AnalysisSequence = instance.AnalysisSequence;
        }

        public string LongName { get; set; }
        public string DefaultLocation { get; set; }
        public string DefaultUnit { get; set; }
        public int ItemTypeId { get; set; }
        public string AccountCode { get; set; }
        public decimal MinQuantity { get; set; }
        public decimal MaxQuantity { get; set; }
        public string Barcode { get; set; }
        public List<byte[]> Images { get; set; }

        public string AnalysisCode01 { get; set; }
        public string AnalysisCode02 { get; set; }
        public string AnalysisCode03 { get; set; }
        public string AnalysisCode04 { get; set; }
        public string AnalysisSequence { get; set; }
    }
}
