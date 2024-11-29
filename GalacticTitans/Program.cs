using GalacticTitans.ApplicationServices.Services;
using GalacticTitans.Core.Domain;
using GalacticTitans.Core.ServiceInterface;
using GalacticTitans.Data;
using GalacticTitans.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ITitansServices, TitansServices>();
builder.Services.AddScoped<IFileServices, FileServices>();
builder.Services.AddScoped<IAccountsServices, AccountsServices>();
builder.Services.AddDbContext<GalacticTitansContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.Password.RequiredLength = 3;

        options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";
        options.Lockout.MaxFailedAccessAttempts = 3;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    })
    .AddEntityFrameworkStores<GalacticTitansContext>()
    .AddDefaultTokenProviders()
    .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("CustomEmailConfirmation")
    .AddDefaultUI();

//all tokenss
builder.Services.Configure<DataProtectionTokenProviderOptions>(
    options => options.TokenLifespan = TimeSpan.FromHours(5)
    );

//email tokens confirmation
builder.Services.Configure<CustomEmailConfirmationTokenProviderOptions>(
    options => options.TokenLifespan = TimeSpan.FromDays(3)
    );


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
