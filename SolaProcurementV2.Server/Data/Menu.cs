namespace SolaProcurementV2.Server.Data
{
    public class Menu : SystemModel
    {
        public Menu() : base() { }

        public Menu(Menu instance) : base(instance)
        {
            Url = instance.Url;
            Icon = instance.Icon;
            ParentId = instance.ParentId;
            Sequence = instance.Sequence;
            Hidden = instance.Hidden;
            Children = instance.Children;
        }

        public string Url { get; set; }
        public string Icon { get; set; }
        public int? ParentId { get; set; }
        public int Sequence { get; set; }
        public bool Hidden { get; set; }
        public IEnumerable<Menu> Children { get; set; }


        bool createAccess = false;
        bool readAccess = false;
        bool updateAccess = false;
        bool deleteAccess = false;

        public bool CreateAccess { get { return createAccess; } set { createAccess = value; if (value && !ReadAccess) { ReadAccess = true; } } }
        public bool ReadAccess { get { return readAccess; } set { readAccess = value; if (!value) { CreateAccess = false; UpdateAccess = false; DeleteAccess = false; } } }
        public bool UpdateAccess { get { return updateAccess; } set { updateAccess = value; if (value && !ReadAccess) { ReadAccess = true; } } }
        public bool DeleteAccess { get { return deleteAccess; } set { deleteAccess = value; if (value && !ReadAccess) { ReadAccess = true; } } }
    }
}
