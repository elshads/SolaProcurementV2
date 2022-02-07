namespace SolaProcurementV2.Server.Configurations
{
    public class StaticData
    {
        public IEnumerable<MenuItem> MenuItemList { get; set; }
        public IEnumerable<MenuItem> GetMenuItemList(Menu menu, bool gridReadOnly)
        {
            var result = new List<MenuItem>()
        {
                new MenuItem { Text="Go to Details", Icon="hyperlink", CommandName="GoToDetails" },
                new MenuItem { Text="Show Properties", Icon="info", CommandName="ShowProperties" },
                new MenuItem { Text="Rows per page", Icon="rows", Items = new List<MenuItem>()
                {
                    new MenuItem { Text="All", CommandName="All" },
                    new MenuItem { Text="10", CommandName="10" },
                    new MenuItem { Text="20", CommandName="20" },
                    new MenuItem { Text="50", CommandName="50" },
                    new MenuItem { Text="100", CommandName="100" },
                    new MenuItem { Text="1000", CommandName="1000" },
                } }
        };
            if (menu.UpdateAccess && !gridReadOnly)
            {
                result.Add(new MenuItem { Text = "Edit", Icon = "edit", CommandName = "BeginEdit" });
            }
            if (menu.DeleteAccess && !gridReadOnly)
            {
                result.Add(new MenuItem { Text = "Delete", Icon = "delete", CommandName = "BeginDelete" });
            }

            return result;
        }
    }
}
