using AuthenticationAuthorization.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configurando o DbContext para usar SQLite
builder.Services.AddDbContext<AuthDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("sqlite"));
});

builder.Services.AddControllers();

// Configurando Identity com suporte a roles e armazenando na base de dados
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

// Configurando a autenticação para usar Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Access/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
         options.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuer = false,
             ValidateAudience = false,
             ValidateLifetime = true,
             ValidateIssuerSigningKey = true,
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
             ClockSkew = TimeSpan.Zero
         }
    );

// Adicionando suporte para Razor Pages (se necessário)
builder.Services.AddRazorPages();

var app = builder.Build();

// Configurar o pipeline de solicitação HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

// Usar autenticação e autorização no pipeline
app.UseAuthentication();
app.UseAuthorization();

// Configurar a rota padrão
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Login}/{id?}");

app.Run();
