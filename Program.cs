using Collect.io.DAL;
using Collect.io.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<Collect.io.BL.Auth.IAuthBL, Collect.io.BL.Auth.AuthBl>();
builder.Services.AddSingleton<Collect.io.BL.Auth.IEncrypt, Collect.io.BL.Auth.Encrypt>();
builder.Services.AddSingleton<IAuthDAL, AuthDAL>();
builder.Services.AddScoped<Collect.io.BL.Auth.ICurrentUser, Collect.io.BL.Auth.CurrentUser>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddMvc().AddSessionStateTempDataProvider();
builder.Services.AddSession();

// Register database
builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.Map("/users", (IDbContextFactory<AppDbContext> dtb) => dtb.CreateDbContext().Users.ToList());

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
