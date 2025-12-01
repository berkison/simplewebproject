using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using Simple.Data;
using Simple.Services;
using Simple.Mapping;
using Simple.Validations;
using FluentValidation.AspNetCore;
using Simple.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

//using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


// 1. Identity Servisi (Kullanýcý Yönetimi)
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<SimpleContext>()
    .AddDefaultTokenProviders();

// 2. JWT Ayarlarý (Bilet Kontrolü)
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(secretKey)
    };
});


// ... DbContext ve Identity ayarlarýn burada kalsýn ...

builder.Services.AddEndpointsApiExplorer();



// Swagger'a "Bearer Token" yeteneði ekliyoruz
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Simple API", Version = "1.00" });

    // Kilit simgesini ve giriþ kutusunu tanýmla
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Lütfen token'ý 'Bearer {token}' formatýnda giriniz.",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    // Bütün endpointlerde bu güvenliði zorunlu kýl
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});



builder.Services.AddControllers();

builder.Services.AddDbContext<SimpleContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("HerkesGelsin", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });

});



builder.Services.AddScoped<IKitapService, KitapService>();
builder.Services.AddAuthorization();

builder.Services.AddAutoMapper(typeof(MapProfile)); // BUÝLD ETMEDEN ÖNCE SERVÝSLERÝ EKLEMEM LAZIM AÞAÐIYA EKLEYÝNCE HATALAR ALIRIM
//*********************************************************************************************************************************
//*********************************************************************************************************************************
builder.Services.AddControllers()
    .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<KitapValidator>());
// Fiyat Takip Servisi
builder.Services.AddHttpClient<Simple.Services.FiyatTakipService>();
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();


// SWAGGER'I AKTÝF ETMEK ÝÇÝN BUNLARI EKLE:
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Arayüzü açan kod budur
}
app.UseMiddleware<GlobalExceptionMiddleware>();




app.UseCors("HerkesGelsin"); //HERKESE AÇIK ***********************************************************************************
// ...


app.UseAuthorization();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
