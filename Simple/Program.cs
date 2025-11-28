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

var builder = WebApplication.CreateBuilder(args);

// ... DbContext ve Identity ayarlarýn burada kalsýn ...

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Benim API", Version = "v1" });
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

var app = builder.Build();

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
