using Microsoft.Extensions.Configuration;
using Microsoft.Extensions;
using MongoDB.Driver;
using NeoSOFT.Domain;
using NeoSOFT.Infrastructure.Context;
using NeoSOFT.Infrastructure.Contract;
using NeoSOFT.Infrastructure.Repository;
using NeoSOFT.Application.Contracts;
using NeoSOFT.Application.Services;
using NeoSOFTWebAPI.Extentions;
using AutoMapper;
using NeoSOFT.Domain.Mapping;
using Asp.Versioning;
using System.Data.Common;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Add DI Container for DBContext Configuration 
builder.Services.Configure<DBConnection>(builder.Configuration.GetSection(nameof(DBConnection)));
builder.Services.AddSingleton<IDbContext>(
    sp=>sp.GetRequiredService<IOptions<DBConnection>>().Value);
builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient(builder.Configuration.GetValue<string>("DBConnection:ConnectionString")));

//TODO: Allow specific origin
builder.Services.ConfigureCors();
builder.Services.AddMemoryCache();
//Configure the HttpContext accessor
builder.Services.ConfigureHttpContext(builder.Configuration);

//DI for the Business services
builder.Services.ConfigureBusinessServices();


//Adding API versioning capabilities
builder.Services.AddApiVersioning(cfg =>
{
    cfg.DefaultApiVersion = new ApiVersion(1, 0);
    cfg.AssumeDefaultVersionWhenUnspecified = true;
    cfg.ReportApiVersions = true;
});

//Configure the Swagger with API Version & Securtiy requirement
builder.Services.ConfigureSwagger();
//DI for Repository
builder.Services.ConfigureRepositoryWrapper();


//Configure the AutoMapper
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseRouting();
app.UseSwaggerUI(c =>
{
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    c.DefaultModelsExpandDepth(-1);
});

app.UseCors("CorsPolicy");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
