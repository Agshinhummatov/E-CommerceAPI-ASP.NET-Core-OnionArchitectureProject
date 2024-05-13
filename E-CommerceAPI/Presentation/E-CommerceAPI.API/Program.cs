using E_CommerceAPI.Application;
using E_CommerceAPI.Application.Validations.Products;
using E_CommerceAPI.Infrastructure;
using E_CommerceAPI.Infrastructure.Filters;
using E_CommerceAPI.Infrastructure.Services.Storage.Azure;
using E_CommerceAPI.Infrastructure.Services.Storage.Local;
using E_CommerceAPI.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPresistenceServices();

builder.Services.AddInfrastructureServices();

builder.Services.AddApplicationServices();

builder.Services.AddStorage<AzureStorage>();// burda bildiremki filerim hansi localdanmi istifade edecek yoxsa azuredenmi




//builder.Services.AddDbContext<EcommerceAPIDbContext>(opt =>
//{
//    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
//});

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
          .AllowAnyHeader()
          .AllowAnyMethod()
));

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>()).AddFluentValidation(configuration => configuration
.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>()).ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin",options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true, // olusturulacak token  degerini/ hangi orginlerin / hangi sitelerin kulanici 
            // belirledigimi degeridir  --> burda her hansi bir cilinte terefindeki web sayt www.bilem.com 

            ValidateIssuer = true, // olusturulacak token degerin kimin  dahitdigi ifade edeceyimiz deyerdir 
            // -- > burda ise bizim APi yimiz olur www.myapi.com

            ValidateLifetime = true, // olsuturulan token degerinin suresini kontrol edecek  olan dogrulamadir 

            ValidateIssuerSigningKey = true, // Uretilecek token degerinin uygulamamiza ait bir deger oldugunu ifade eden
            // sucrikey verisin dogrulanmasidir // sadece bizim uygulamaizi diger uygulamalardan ferqlendiren keydir yeni biz bunun vasitesi ile
            // hec kim bu keyi tahmin edemez 

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseStaticFiles();


app.UseCors(); // AddCors() method isledir


app.UseHttpsRedirection();

app.UseAuthentication(); // midilawaredir bunlar
app.UseAuthorization(); // midilawaredir bunlar

app.MapControllers();

app.Run();
