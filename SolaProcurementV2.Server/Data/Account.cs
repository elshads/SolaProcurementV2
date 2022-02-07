namespace SolaProcurementV2.Server.Data
{
    public class Account : ReferenceModel
    {
        public Account() : base() { }

        public Account(Account instance) : base(instance)
        {
            AccountTypeId = instance.AccountTypeId;
            Amount = instance.Amount;
        }

        
        public int AccountTypeId { get; set; }
        public decimal Amount { get; set; }

    }
}
