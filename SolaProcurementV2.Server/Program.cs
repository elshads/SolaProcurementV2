using SolaProcurementV2.Server.Areas.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using FluentValidation;
using Blazored.LocalStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var sqlConfiguration = new SqlConfiguration(connectionString);

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

builder.Services.AddDbContext<IdentityAppContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
})
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<IdentityAppContext>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; }); ;
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomCenter;
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 8000;
    config.SnackbarConfiguration.HideTransitionDuration = 300;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Text;
    config.SnackbarConfiguration.ClearAfterNavigation = false;
});
builder.Services.AddTelerikBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<AppUser>>();
builder.Services.AddSingleton(sqlConfiguration);
builder.Services.AddScoped<SessionData>();
builder.Services.AddTransient<PageData>();
builder.Services.AddSingleton<StaticData>();
builder.Services.AddSingleton<AppUserService>();
builder.Services.AddSingleton<UserGroupService>();
builder.Services.AddSingleton<ThemeService>();
builder.Services.AddSingleton<MenuService>();
builder.Services.AddSingleton<AccountService>();
builder.Services.AddSingleton<StatusService>();
builder.Services.AddSingleton<BusinessUnitService>();
builder.Services.AddSingleton<Images>();
builder.Services.AddSingleton<AnalysisStructureService>();
builder.Services.AddSingleton<AnalysisDimensionService>();
builder.Services.AddTransient<IValidator<UserGroup>, UserGroupValidator>();
builder.Services.AddTransient<IValidator<AppUser>, AppUserValidator>();
builder.Services.AddTransient<IValidator<BusinessUnit>, BusinessUnitValidator>();
builder.Services.AddTransient<IValidator<AnalysisDimension>, AnalysisDimensionValidator>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
