using GeekShopping.Web.Services;
using GeekShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";

})
    .AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
    .AddOpenIdConnect("oidc", opt =>
    {
        opt.Authority = builder.Configuration["ServicesUrls:IdentityServer"];
        opt.GetClaimsFromUserInfoEndpoint = true;
        opt.ClientId = "geek_shopping"; // GeekShopping.IdentityServer Configuration
        opt.ClientSecret = "my_secret";  // GeekShopping.IdentityServer Configuration
        opt.ResponseType = "code";
        opt.ClaimActions.MapJsonKey("role", "role", "role");
        opt.ClaimActions.MapJsonKey("sub", "sub", "sub");
        opt.TokenValidationParameters.NameClaimType = "name";
        opt.TokenValidationParameters.RoleClaimType = "role";
        opt.Scope.Add("geek_shopping");
        opt.SaveTokens = true;
    });

builder.Services.AddHttpClient<IProductService, ProductService>(
    c => c.BaseAddress = new Uri(builder.Configuration["ServicesUrls:ProductApi"]));

builder.Services.AddHttpClient<ICartService, CartService>(
    c => c.BaseAddress = new Uri(builder.Configuration["ServicesUrls:CartApi"]));

builder.Services.AddHttpClient<ICouponService, CouponService>(
    c => c.BaseAddress = new Uri(builder.Configuration["ServicesUrls:CouponApi"]));

//builder.Services.AddHttpClient<ICouponService, CouponService>(
//    c => c.BaseAddress = new Uri(builder.Configuration["ServicesUrls:CouponApi"]));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
