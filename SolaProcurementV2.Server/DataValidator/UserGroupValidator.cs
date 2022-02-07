namespace SolaProcurementV2.Server.DataValidator
{
    public class UserGroupValidator : AbstractValidator<UserGroup>
    {
        public UserGroupValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("This field is mandatory");
            RuleFor(p => p.Code).NotEmpty().WithMessage("This field is mandatory");
            RuleFor(p => p.Code).Must(UniqueCode).WithMessage("This code already exists").When(p => p.Id == 0);
        }

        private bool UniqueCode(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                try
                {
                    using (var cn = new SqlConnection(SqlConfiguration.StaticConnectionString))
                    {
                        var sql = $"SELECT Code FROM UserGroup";
                        var codeList = cn.Query<string>(sql);
                        var match = codeList
                                    .Where(e => e.ToLower() == code.ToLower())
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
