using E_CommerceAPI.API.Configurations.ColumnWriters;
using E_CommerceAPI.API.Extensions;
using E_CommerceAPI.Application;
using E_CommerceAPI.Application.Validations.Products;
using E_CommerceAPI.Infrastructure;
using E_CommerceAPI.Infrastructure.Filters;
using E_CommerceAPI.Infrastructure.Services.Storage.Azure;
using E_CommerceAPI.Persistence;
using E_CommerceAPI.SignalR;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using NpgsqlTypes;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpContextAccessor(); // Clinte'den gelen request neticesinde olusturulan HttpContext nesnesine katmanlardaki class'lar uzerinde businnes logic erisebilmesini saglayan bir servicedir
builder.Services.AddPresistenceServices();

builder.Services.AddInfrastructureServices();

builder.Services.AddApplicationServices();

builder.Services.AddSignalRServices();

builder.Services.AddStorage<AzureStorage>();// burda bildiremki filerim hansi localdanmi istifade edecek yoxsa azuredenmi



builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins("http://localhost:4200", "http://localhost:4200")
          .AllowAnyHeader()
          .AllowAnyMethod().AllowCredentials()
));

Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("log/log.txt")
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgreSQL"),"logs",
    needAutoCreateTable : true, columnOptions: new Dictionary<string, ColumnWriterBase>
    {
           {"message", new RenderedMessageColumnWriter(NpgsqlDbType.Text)},
            {"message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text)},
            {"level", new LevelColumnWriter(true , NpgsqlDbType.Varchar)},
            {"time_stamp", new TimestampColumnWriter(NpgsqlDbType.Timestamp)},
            {"exception", new ExceptionColumnWriter(NpgsqlDbType.Text)},
            {"log_event", new LogEventSerializedColumnWriter(NpgsqlDbType.Json)},
            {"user_name", new UsernameColumnWriter()} // cutsum ozumuzun yaratdiqimiz classdi
    })
    .WriteTo.Seq(builder.Configuration["Seq:ServerURL"])
    .Enrich.FromLogContext() // contexden xairci propetilere catmaq ucun istifade edirik bunu yeni conexte qoyduqumuz username cata bilirik
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog(log);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore,expires, securityToken,validationParameters) => expires != null ? expires > DateTime.UtcNow : false , // burds ise acces tokenimiz muddetini methodla gonderecem ona uygun edirem nece saniye olacaq deye bu namespacededi  E_CommerceAPI.Infrastructure.Services.Token
            NameClaimType = ClaimTypes.Name //JWT üzerinde Name claimne karþýlýk gelen deðeri User.Identity.Name propertysinden elde edebiliriz.
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());


//app.UseExceptionHandler();


app.UseStaticFiles();

app.UseSerilogRequestLogging(); // seri logun cagirikqi midalware islesin
app.UseHttpLogging(); // midilwreni cagiriq 
app.UseCors(); // AddCors() method isledir


app.UseHttpsRedirection();

app.UseAuthentication(); // midilawaredir bunlar
app.UseAuthorization(); // midilawaredir bunlar

app.Use(async(context, next) =>
{
    var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null; // birince deyiremki user nul deyilse Identity null deyilse eger IsAuthenticated olubsa yeni daxil olubsa ve gelen deyerler null deyilse ve ya ture ise  context.User.Identity.Name ver yox eger null ise : null geri donder
    LogContext.PushProperty("user_name", username); // burda ise men user_name tabline artiq yaxaldiqim userin namemini puhs edirem gedib dusecek sqlme
    await next();
});

app.MapControllers();

app.MapHubs();

app.Run();
