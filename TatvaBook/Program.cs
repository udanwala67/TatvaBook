using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TatvaBook.Entities.Data;
using TatvaBook.Repository;
using TatvaBook.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<TatvaBookContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("TatvaBook")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    /* options.Password.RequireNonAlphanumeric = false;*/
    /* options.Password.RequiredLength = 10;
     options.Password.RequiredUniqueChars = 2;
    */
    options.SignIn.RequireConfirmedEmail = true;
})

    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<TatvaBookContext>();

builder.Services.Configure<DataProtectionTokenProviderOptions>(Options =>
                      Options.TokenLifespan = TimeSpan.FromHours(3));

/*builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);*/


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login/Login";
    options.ExpireTimeSpan = TimeSpan.FromDays(5);
    options.SlidingExpiration = true;
});

builder.Services.AddAuthorization(options =>
    options.AddPolicy("TwoFactorEnabled", x => x.RequireClaim("amr", "mfa")));

builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddSession();



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


app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
