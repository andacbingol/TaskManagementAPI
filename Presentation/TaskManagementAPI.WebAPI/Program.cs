using TaskManagementAPI.Persistence;
using TaskManagementAPI.Application;
using TaskManagementAPI.Infrastructure;
using Microsoft.OpenApi.Models;
using NpgsqlTypes;
using Serilog.Sinks.PostgreSQL;
using Serilog;
using Serilog.Core;
using TaskManagementAPI.WebAPI.Configurations.ColumnWriters;
using Microsoft.AspNetCore.HttpLogging;
using Serilog.Context;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using TaskManagementAPI.WebAPI.Extentions;
using TaskManagementAPI.WebAPI;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment;

builder.Configuration
    .SetBasePath(env.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddWebAPIServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgreSQL"), "logs", needAutoCreateTable: true,
    columnOptions: new Dictionary<string, ColumnWriterBase>
    {
        {"message", new RenderedMessageColumnWriter(NpgsqlDbType.Text)},
        {"message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text)},
        {"level", new LevelColumnWriter(true , NpgsqlDbType.Varchar)},
        {"time_stamp", new TimestampColumnWriter(NpgsqlDbType.Timestamp)},
        {"exception", new ExceptionColumnWriter(NpgsqlDbType.Text)},
        {"log_event", new LogEventSerializedColumnWriter(NpgsqlDbType.Json)},
        {"user_name", new UsernameColumnWriter()}
    })
    .WriteTo.Seq(builder.Configuration["Seq:ServerURL"])
    .Enrich.FromLogContext()
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

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options => 
    {
        options.SuppressModelStateInvalidFilter = true;
        options.SuppressInferBindingSourcesForParameters = true;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskManagement API", Version = "v1", Description = "TaskManagement API swagger client" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,

    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            }, Array.Empty<string>()
        }
    });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();
app.ConfigureExceptionHandlingMiddleware();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseSerilogRequestLogging();

app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    var username = context.User?.Identity?.IsAuthenticated != null || true ? context?.User?.FindFirstValue(JwtRegisteredClaimNames.Name) : null;
    LogContext.PushProperty("user_name", username);
    await next();
});

app.MapControllers();

app.Run();
