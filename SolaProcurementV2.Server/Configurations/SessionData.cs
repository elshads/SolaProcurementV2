using Microsoft.AspNetCore.Identity;

namespace SolaProcurementV2.Server.Configurations
{
    public class SessionData
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionData(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            ParentPageTabIndex = 0;

            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;

            var user = _httpContextAccessor.HttpContext.User;
            try
            {
                var appUserService = new AppUserService();
                var menuService  = new MenuService();
                var businessUnitService  = new BusinessUnitService();
                var sessionUserId = int.Parse(_userManager.GetUserId(user));
                SessionUser = appUserService.GetById(sessionUserId);
            }
            catch (Exception e)
            {
                SessionUser = new AppUser();
            }
        }


        public AppUser SessionUser { get; private set; }
        public int ModelId { get; set; }
        public string ModelCode { get; set; }
        public int ParentPageTabIndex { get; set; }

    }
}
