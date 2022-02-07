using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SolaProcurementV2.Server.Identity
{
    public class IdentityAppContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public IdentityAppContext(DbContextOptions<IdentityAppContext> options) : base(options)
        {

        }
    }
}
