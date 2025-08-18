using ChatApp.Hubs; // Bu namespace projenizin ana namespace'i ve Hubs klas�r�n�n ad�d�r.
                    // E�er Hubs klas�r� yoksa veya ana namespace farkl�ysa buray� do�ru ayarlay�n.

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// SignalR servisini ekle
builder.Services.AddSignalR();

// CORS servisini ekle ve politikay� tan�mla
// React uygulaman�z�n https://localhost:7027'ye eri�mesine izin verin
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173") // React uygulaman�n �al��t��� adres
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials(); // SignalR i�in bu zorunludur
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

// CORS middleware'ini kullan. Bu sat�r UseRouting ve UseAuthorization aras�nda olmal�.
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ChatHub'� /chathub endpoint'ine haritala
app.MapHub<ChatHub>("/chathub");

app.Run();