using Microsoft.AspNetCore.Identity;

namespace SolaProcurementV2.Server.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public AppUser()
        {
            RowIndex = -1;
            ChangePassword = 0;
            StatusId = 0;
            ThemeId = 0;
            ExpirationDate = new DateTime(2099, 12, 31);
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
        }

        public AppUser(AppUser instance)
        {
            ReturnMessage = instance.ReturnMessage;
            RowIndex = instance.RowIndex;
            Password = instance.Password;
            PasswordConfirmation = instance.PasswordConfirmation;
            FullName = instance.FullName;
            ChangePassword = instance.ChangePassword;
            NotificationEmail = instance.NotificationEmail;
            Administrator = instance.Administrator;
            StatusId = instance.StatusId;
            ThemeId = instance.ThemeId;
            ExpirationDate = instance.ExpirationDate;
            LastActivity = instance.LastActivity;
            IsActive = instance.IsActive;
            Photo = instance.Photo;
            Description = instance.Description;
            CreatedOn = instance.CreatedOn;
            CreatedBy = instance.CreatedBy;
            UpdatedOn = instance.UpdatedOn;
            UpdatedBy = instance.UpdatedBy;

            Id = instance.Id;
            UserName = instance.UserName;
            NormalizedUserName = instance.NormalizedUserName;
            Email = instance.Email;
            NormalizedEmail = instance.NormalizedEmail;
            EmailConfirmed = instance.EmailConfirmed;
            PasswordHash = instance.PasswordHash;
            SecurityStamp = instance.SecurityStamp;
            ConcurrencyStamp = instance.ConcurrencyStamp;
            PhoneNumber = instance.PhoneNumber;
            PhoneNumberConfirmed = instance.PhoneNumberConfirmed;
            TwoFactorEnabled = instance.TwoFactorEnabled;
            LockoutEnd = instance.LockoutEnd;
            LockoutEnabled = instance.LockoutEnabled;
            AccessFailedCount = instance.AccessFailedCount;
        }


        public int RowIndex { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string NotificationEmail { get; set; }
        public bool Administrator { get; set; }
        public int ChangePassword { get; set; }
        public int StatusId { get; set; }
        public int ThemeId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int IsActive { get; set; }
        public DateTime LastActivity { get; set; }
        public byte[] Photo { get; set; }
        public string ReturnMessage { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
    }
}
