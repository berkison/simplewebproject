// ... diðer using'ler
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Simple.Data;
using Microsoft.AspNetCore.Identity;



var builder = WebApplication.CreateBuilder(args);

// ... DbContext ve Identity ayarlarýn burada kalsýn ...

builder.Services.AddEndpointsApiExplorer();

// HATA ALDIÐIN SATIR ARTIK ÇALIÞACAK:
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



builder.Services.AddAuthorization();

var app = builder.Build();

// ... Identity map iþlemlerin kalsýn ...

// SWAGGER'I AKTÝF ETMEK ÝÇÝN BUNLARI EKLE:
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Arayüzü açan kod budur
}

app.UseHttpsRedirection();
// ALTINA BUNU EKLE:
app.UseCors("HerkesGelsin");
// ...
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
