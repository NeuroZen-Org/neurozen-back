using neurozen.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;
using neurozen.API.Appointments.Application.Internal.CommandServices;
using neurozen.API.Appointments.Application.Internal.QueryServices;
using neurozen.API.Appointments.Domain.Repositories;
using neurozen.API.Appointments.Domain.Services;
using neurozen.API.Appointments.Infrastructure.Repositories;
using neurozen.API.Resources;
using neurozen.API.Triggers.Application.Internal.CommandServices;
using neurozen.API.Triggers.Domain.Repositories;
using neurozen.API.Triggers.Domain.Services;
using neurozen.API.Triggers.Infraestructure.Respositories;
using neurozen.API.Subscriptions.Application.Internal.CommandServices;
using neurozen.API.Subscriptions.Domain.Repositories; 
using neurozen.API.Subscriptions.Domain.Services;
using neurozen.API.Subscriptions.Infraestructure.Respositories;
using neurozen.API.Professionals.Application.Internal.CommandServices;
using neurozen.API.Professionals.Application.Internal.QueryServices;
using neurozen.API.Professionals.Domain.Repositories;
using neurozen.API.Professionals.Domain.Services;
using neurozen.API.Professionals.Infrastructure.Repositories;
using neurozen.API.ResourcesLibrary.Application.Internal.CommandServices;
using neurozen.API.ResourcesLibrary.Domain.Repositories;
using neurozen.API.ResourcesLibrary.Domain.Services;
using neurozen.API.ResourcesLibrary.Infrastructure.Repositories;
using neurozen.API.Shared.Domain.Repositories;
using neurozen.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using neurozen.API.Shared.Infrastructure.Persistence.EFC.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddRouting(options => options.LowercaseUrls = true );
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers().AddDataAnnotationsLocalization(options =>
{
    options.DataAnnotationLocalizerProvider = (type, factory) =>
    {
        return factory.Create(typeof(SharedResource));
    };
});

//Localization

builder.Services.AddLocalization();
var LocalizationOptions = new RequestLocalizationOptions();


LocalizationOptions.SetDefaultCulture("es-PE");
LocalizationOptions.AddSupportedCultures("es-PE", "en-US");
LocalizationOptions.AddSupportedUICultures("es-PE", "en-US");
LocalizationOptions.ApplyCurrentCultureToResponseHeaders = true;

// Enable Swashbuckle Annotations so [SwaggerOperation]/[SwaggerResponse] attributes are recognized
builder.Services.AddSwaggerGen(options => options.EnableAnnotations());


if (builder.Environment.IsDevelopment())
    builder.Services.AddDbContext<AppDbContext>(
        options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            // Verify Database Connection String
            if (connectionString is null)
                // Stop the application if the connection string is not set.
                throw new Exception("Database connection string is not set.");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        });
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Appointments services
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IAppointmentCommandService, AppointmentCommandService>();
builder.Services.AddScoped<IAppointmentQueryService, AppointmentQueryService>();

// Triggers services
builder.Services.AddScoped<ITriggerRepository, TriggerRepository>();
builder.Services.AddScoped<ITriggerCommandService, TriggerCommandService>();

// Subscriptions services
builder.Services.AddScoped<ISubscriptionRepository, SubcriptionRepository>();
builder.Services.AddScoped<ISubscriptionCommandService, SubscriptionCommandService>();

// Professionals services
builder.Services.AddScoped<IProfessionalRepository, ProfessionalRepository>();
builder.Services.AddScoped<IProfessionalCommandService, ProfessionalCommandService>();
builder.Services.AddScoped<IProfessionalQueryService, ProfessionalQueryService>();

// ResourcesLibrary services
builder.Services.AddScoped<IResourceLibraryRepository, ResourceLibraryRepository>();
builder.Services.AddScoped<IResourceLibraryCommandService, ResourceLibraryCommandService>();

var app = builder.Build();

// Verify Database Objects are created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}
app.UseRequestLocalization(LocalizationOptions);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();


app.Run();
