using ChatApp.Hubs; // Bu namespace projenizin ana namespace'i ve Hubs klasörünün adýdýr.
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ChatApp.Models; // Kendi user modelinizin bulunduðu namespace
using ChatApp.Data; // Kendi DbContext'inizin bulunduðu namespace
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// SignalR servisini ekle
builder.Services.AddSignalR();

// CORS servisini ekle ve politikayý tanýmla
// React uygulamanýzýn https://localhost:7027'ye eriþmesine izin verin
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173") // React uygulamanýn çalýþtýðý adres
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials(); // SignalR için bu zorunludur
        });
});

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

// CORS middleware'ini kullan. Bu satýr UseRouting ve UseAuthorization arasýnda olmalý.
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ChatHub'ý /chathub endpoint'ine haritala
app.MapHub<ChatHub>("/chathub");

app.Run();