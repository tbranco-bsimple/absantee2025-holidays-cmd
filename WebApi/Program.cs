using Application.DTO;
using Application.Services;
using Domain.Factory;
using Domain.IRepository;
using Domain.Models;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Resolvers;
using Microsoft.EntityFrameworkCore;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment.EnvironmentName;

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AbsanteeContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

//Services
builder.Services.AddTransient<HolidayPlanService>();
builder.Services.AddTransient<CollaboratorService>();

//Repositories
builder.Services.AddTransient<IHolidayPlanRepository, HolidayPlanRepositoryEF>();
builder.Services.AddTransient<ICollaboratorRepository, CollaboratorRepositoryEF>();


//Factories
builder.Services.AddTransient<IHolidayPlanFactory, HolidayPlanFactory>();
builder.Services.AddTransient<IHolidayPeriodFactory, HolidayPeriodFactory>();
builder.Services.AddTransient<ICollaboratorFactory, CollaboratorFactory>();

//Mappers
builder.Services.AddTransient<HolidayPlanDataModelConverter>();
builder.Services.AddTransient<HolidayPeriodDataModelConverter>();
builder.Services.AddTransient<CollaboratorDataModelConverter>();

builder.Services.AddAutoMapper(cfg =>
{
    //DataModels
    cfg.AddProfile<DataModelMappingProfile>();

    //DTO
    cfg.CreateMap<HolidayPlan, HolidayPlanDTO>();
    cfg.CreateMap<HolidayPlanDTO, HolidayPlan>();
    cfg.CreateMap<HolidayPeriod, HolidayPeriodDTO>();
    cfg.CreateMap<HolidayPeriodDTO, HolidayPeriod>();
    cfg.CreateMap<HolidayPeriod, CreateHolidayPeriodDTO>()
            .ForMember(dest => dest.InitDate, opt => opt.MapFrom(src => src.PeriodDate.InitDate))
            .ForMember(dest => dest.FinalDate, opt => opt.MapFrom(src => src.PeriodDate.FinalDate));
    cfg.CreateMap<Collaborator, CollaboratorDTO>();
    cfg.CreateMap<CollaboratorDTO, Collaborator>();
});

// MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<HolidayPeriodConsumer>();
    x.AddConsumer<HolidayPlanConsumer>();
    x.AddConsumer<CollaboratorConsumer>();


    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost");
        var instance = InstanceInfo.InstanceId;
        cfg.ReceiveEndpoint($"holidays-cmd-{instance}", e =>
        {
            e.ConfigureConsumer<HolidayPeriodConsumer>(context);
            e.ConfigureConsumer<HolidayPlanConsumer>(context);
            e.ConfigureConsumer<CollaboratorConsumer>(context);
        });
    });
});

builder.Services.AddScoped<IMessagePublisher, MassTransitPublisher>();


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
