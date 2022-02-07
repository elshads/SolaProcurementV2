namespace SolaProcurementV2.Server.DataModel
{
    public class SystemModel : BaseModel
    {
        public SystemModel() : base() { }

        public SystemModel(SystemModel instance) : base(instance)
        {
            Code = instance.Code;
            Name = instance.Name;
        }

        public string Code { get; set; }
        public string Name { get; set; }
    }
}
