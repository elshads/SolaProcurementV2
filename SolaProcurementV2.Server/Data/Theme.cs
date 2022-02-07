namespace SolaProcurementV2.Server.Data
{
    public class Theme : ReferenceModel
    {
        public Theme() : base() { }

        public Theme(Theme instance) : base(instance)
        {
            NavBackgroundColor = instance.NavBackgroundColor;
            NavFontColor = instance.NavFontColor;
            NavActiveFontColor = instance.NavActiveFontColor;
            NavActiveBackground = instance.NavActiveBackground;
            NavFooterBackground = instance.NavFooterBackground;
            ButtonRadiusColor = instance.ButtonRadiusColor;
        }

        public string NavBackgroundColor { get; set; }
        public string NavFontColor { get; set; }
        public string NavActiveFontColor { get; set; }
        public string NavActiveBackground { get; set; }
        public string NavFooterBackground { get; set; }
        public string ButtonRadiusColor { get; set; }

    }
}
