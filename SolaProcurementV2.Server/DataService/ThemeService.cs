
namespace SolaProcurementV2.Server.DataService
{
    public class ThemeService : BaseModelService<Theme>
    {
        public Theme DefaultTheme { get; } = new Theme()
        {
            Id = 1,
            Code = "light",
            Name = "Light",
            NavBackgroundColor = "#fff",
            NavFontColor = "#424242",
            NavActiveFontColor = "#594ae2",
            NavActiveBackground = "#e2e3e5",
            NavFooterBackground = "#f5f5f5",
            ButtonRadiusColor = "#e2e3e5"
        };
        public new Theme GetById(int themeId)
        {
            var theme = new Theme();
            if (themeId == 1)
            {
                //light
                theme.Id = 1;
                theme.Code = "light";
                theme.Name = "Light";
                theme.NavBackgroundColor = "#fff";
                theme.NavFontColor = "#424242";
                theme.NavActiveFontColor = "#594ae2";
                theme.NavActiveBackground = "#e2e3e5";
                theme.NavFooterBackground = "#f5f5f5";
                theme.ButtonRadiusColor = "#e2e3e5";
            }
            else if (themeId == 2)
            {
                //dark
                theme.Id = 2;
                theme.Code = "dark";
                theme.Name = "Dark";
                theme.NavBackgroundColor = "#11101d"; // 11101d 2d303f
                theme.NavFontColor = "#eeeeee";
                theme.NavActiveFontColor = "#7fff00"; // 7fff00 22f4bb
                theme.NavActiveBackground = "#25253B";
                theme.NavFooterBackground = "#171527";
                theme.ButtonRadiusColor = "#25253B";
            }
            else
            {
                theme = DefaultTheme;
            }
            return theme;
        }
    }
}
