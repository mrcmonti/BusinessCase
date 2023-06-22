using AutoMapper;
using ParanaBanco.Application.Configurations;
using ParanaBanco.Application.Interfaces.Services;
using ParanaBanco.Application.Services;
using ParanaBanco.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Business Case" });
    s.EnableAnnotations();
});

ConfigureRepository.ConfigureDependenciesRepository(builder.Services, builder.Configuration["ConnectionStrings:DefaultConnection"]);

builder.Services.AddScoped<ICustomerService, CustomerService>();

IMapper mapper = MapperConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
