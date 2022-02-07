namespace SolaProcurementV2.Server.DataValidator
{
    public class AppUserValidator : AbstractValidator<AppUser>
    {
        public AppUserValidator()
        {
            RuleFor(p => p.FullName).NotEmpty().WithMessage("This field is mandatory");
            RuleFor(p => p.Password).Length(6, 255).WithMessage("Password must be at least 6 characters long");
            RuleFor(p => p.PasswordConfirmation).Equal(p => p.Password).WithMessage("Password does not match");
            RuleFor(p => p.UserName).NotEmpty().WithMessage("This field is mandatory").EmailAddress().WithMessage("Not valid email address"); 
            RuleFor(p => p.UserName).Must(UniqueEmail).WithMessage("This username already exists").When(p => p.Id == 0);
            RuleFor(p => p.NotificationEmail).EmailAddress().WithMessage("Not valid email address");
        }

        private bool UniqueEmail(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                try
                {
                    using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                    {
                        var sql = $"SELECT Email FROM AspNetUsers";
                        var emailList = cn.Query<string>(sql);
                        var match = emailList
                                    .Where(e => e.ToLower() == username.ToLower())
                                    .SingleOrDefault();

                        if (match == null)
                        {
                            return true;
                        }
                    }
                }
                catch (Exception e)
                {
                    var message = e.Message;
                }
            }
            return false;
        }
    }
}
