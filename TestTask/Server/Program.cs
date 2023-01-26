using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using TestTask.Server.Repositories.Interfaces;
using TestTask.Server.Repositories;
using TestTask.Shared.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TestTask.Shared.Models.Identity;
using TestTask.Server.HostedServices;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("identity"), b => b.MigrationsAssembly("TestTask.Server")));
builder.Services.AddDbContext<PizzaAppDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("db"), b=> b.MigrationsAssembly("TestTask.Server")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#region Hosted Services
builder.Services.AddScoped<IdentityDbSeeder>();
builder.Services.AddHostedService<IdentityDbSeederHostedService>();
builder.Services.AddScoped<PizzaAppDbDataSeeder>();
builder.Services.AddHostedService<PizzaAppDbSeederHostedService>();
#endregion
builder.Services.AddControllersWithViews().AddNewtonsoftJson(option => {
    option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
    option.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
}); 
builder.Services.AddRazorPages();
#region identity configuration
builder.Services.AddIdentity<IdentityUser, IdentityRole>(option =>
{
    option.Password.RequireDigit = false;
    option.Password.RequiredLength = 6;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireUppercase = false;
    option.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<AppDbContext>();
#endregion
#region JWT Configuration
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
               .AddJwtBearer(options => {
                   options.SaveToken = true;
                   options.RequireHttpsMetadata = false;
                   options.TokenValidationParameters = new TokenValidationParameters()
                   {
                       ValidateIssuer = false,
                       ValidateAudience = false,
                       ValidAudience = builder.Configuration["Jwt:Site"],
                       ValidIssuer = builder.Configuration["Jwt:Site"],
                       IssuerSigningKey =
                       new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SigningKey"] ?? "IsDB-BISEW R50 ACSL-A"))
                   };
               });
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
