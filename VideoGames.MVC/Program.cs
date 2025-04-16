using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using NToastNotify;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllersWithViews()
    .AddNToastNotifyToastr(new ToastrOptions
    {
        ProgressBar = true, // ilerleme �ubu�u
        PositionClass = ToastPositions.TopRight,// pozisyon
        CloseButton = true, // kapatma butonu
        TimeOut = 5000, // s�resi 5 sn
        ShowDuration = 1000, // a��l�rken silikten g�r�n�r hale ge�me s�resi
        HideDuration = 1000, // kapan�rken silik hale ge�me s�resi
        ShowEasing = "swing", // a��lma efekti
        HideEasing = "linear", // 
        ShowMethod = "fadeIn", // g�r�n�r olma olay�
        HideMethod = "fadeOut" // kapanma olay�
    });


builder
    .Services
    .AddHttpClient(
        "VideoGamesAPI",
        client => client.BaseAddress = new Uri("http://localhost:5178/api/")
    );

builder
    .Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "VideoGames.Authorization";
        options.LoginPath = "/Authorization/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied"; // yetkisiz sayfa
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.Cookie.HttpOnly = true; // sadece Http protokol� �zerine istek als�n
    });

builder.Services.AddAuthorization();

#region Data Protection
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(
        builder.Environment.ContentRootPath, "keys")))
    .SetApplicationName("ECommerce.MVC")
    .SetDefaultKeyLifetime(TimeSpan.FromDays(14));
builder.Services.AddDistributedMemoryCache();
#endregion

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
