namespace SolaProcurementV2.Server.ComponentConfigurations
{
    public class MenuItem
    {
        public string Text { get; set; }
        public string Icon { get; set; }
        public Action Action { get; set; }
        public string CommandName { get; set; }
        public IEnumerable<MenuItem> Items { get; set; }
    }
}
