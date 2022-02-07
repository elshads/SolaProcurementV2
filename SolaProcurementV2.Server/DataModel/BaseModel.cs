namespace SolaProcurementV2.Server.DataModel
{
    public class BaseModel
    {
        public BaseModel()
        {
            RowIndex = -1;
        }

        public BaseModel(BaseModel instance)
        {
            ReturnMessage = instance.ReturnMessage;
            RowIndex = instance.RowIndex;
            Id = instance.Id;
            Description = instance.Description;
        }


        public string ReturnMessage { get; set; }
        public int RowIndex { get; set; }

        //

        public int Id { get; set; }
        public string Description { get; set; }
        

    }
}
