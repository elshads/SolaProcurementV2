namespace SolaProcurementV2.Server.DataModel
{
    public class ReferenceModel : SystemModel
    {
        public ReferenceModel() : base() { }

        public ReferenceModel(ReferenceModel instance) : base(instance)
        {
            BusinessUnitId = instance.BusinessUnitId;
            StatusId = instance.StatusId;
            CreatedOn = instance.CreatedOn;
            CreatedBy = instance.CreatedBy;
            UpdatedOn = instance.UpdatedOn;
            UpdatedBy = instance.UpdatedBy;
        }


        public int BusinessUnitId { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
    }
}
