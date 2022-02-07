namespace SolaProcurementV2.Server.Configurations
{
    public class PageData
    {
        AppUser sessionUser { get; set; }
        public PageData(SessionData sessionData)
        {
            sessionUser = sessionData.SessionUser;
            var businessUnitService = new BusinessUnitService();
            var menuService = new MenuService();
            BusinessUnitList = businessUnitService.GetAllOpen(sessionUser, false).Result;
            if (BusinessUnitList != null && BusinessUnitList.Any())
            {
                SelectedBusinessUnit = BusinessUnitList.FirstOrDefault();
            }
            if (!sessionUser.Administrator)
            {
                menuList = menuService.GetAccessLevelList(sessionUser).Result;
            }
        }
        public IEnumerable<BusinessUnit> BusinessUnitList { get; set; }
        public BusinessUnit SelectedBusinessUnit { get; set; }
        public Menu Menu { get; set; }

        IEnumerable<Menu> menuList { get; set; } = new List<Menu>();

        public Menu GetAccessLevel(int menuId)
        {
            if (sessionUser.Administrator)
            {
                Menu = new Menu() { Id = menuId, CreateAccess = true, ReadAccess = true, UpdateAccess = true, DeleteAccess = true };
            }
            else
            {
                var _menuList = menuList.Where(e => e.Id == menuId);
                if (_menuList.Any())
                {
                    Menu = _menuList.FirstOrDefault();
                }
                else
                {
                    Menu = new Menu() { Id = menuId, CreateAccess = false, ReadAccess = false, UpdateAccess = false, DeleteAccess = false };
                }
            }
            return Menu;
        }

        public BusinessUnit SetSelectedBusinessUnit(int urlBu)
        {
            SelectedBusinessUnit = (urlBu > 0 ? BusinessUnitList.FirstOrDefault(e => e.Id == urlBu) : SelectedBusinessUnit);
            return SelectedBusinessUnit;
        }
    }
}
