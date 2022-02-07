namespace SolaProcurementV2.Server.Data
{
    public class UserGroup : ReferenceModel
    {
        public UserGroup() : base() { }

        public UserGroup(UserGroup instance) : base(instance) 
        {
            AppUserCount = instance.AppUserCount;
            BusinessUnitCount = instance.BusinessUnitCount;
            MenuCount = instance.MenuCount;
            ApproveRoleCount = instance.ApproveRoleCount;

            AppUserList = instance.AppUserList;
            BusinessUnitList = instance.BusinessUnitList;
            MenuList = instance.MenuList;
            ApproveRoleList = instance.ApproveRoleList;
        }

        public int AppUserCount { get; set; }
        public int BusinessUnitCount { get; set; }
        public int MenuCount { get; set; }
        public int ApproveRoleCount { get; set; }

        public List<AppUser> AppUserList { get; set; } = new();
        public List<BusinessUnit> BusinessUnitList { get; set; } = new();
        public List<Menu> MenuList { get; set; } = new();
        public List<ApproveRole> ApproveRoleList { get; set; } = new();
    }
}
