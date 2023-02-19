//using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SalaryManage.DAL;
using SalaryManage.Infrastructure.Constracts;
using SalaryManage.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DBLocation");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


//// add dependency
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();


//builder.Services.Configure<IdentityOptions>(options =>
//{
//   // Default password
//   options.Password.RequireDigit = true;
//   options.Password.RequireNonAlphanumeric = false;
//   options.Password.RequireLowercase = true;
//   options.Password.RequireUppercase = true;
//   options.Password.RequiredLength = 6;
//   options.Password.RequiredUniqueChars = 1;

//   // Default Lockout
//   options.Lockout.AllowedForNewUsers = true;
//   options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
//   options.Lockout.MaxFailedAccessAttempts = 5;

//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseMigrationsEndPoint();
}
else
{
   app.UseExceptionHandler("/Home/Error");
   // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
   app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.MapRazorPages();

app.Run();
