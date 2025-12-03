using neurozen.API.Shared.Infrastructure.Persistence.EFC.Configuration;
// IAM imports
using neurozen.API.IAM.Infrastructure.Tokens.JWT.Configuration;
using neurozen.API.IAM.Infrastructure.Tokens.JWT.Services;
using neurozen.API.IAM.Application.Internal.OutboundServices;
using neurozen.API.IAM.Infrastructure.Hashing.BCrypt.Services;
using neurozen.API.IAM.Infrastructure.Persistence.EFC.Repositories;
using neurozen.API.IAM.Application.Internal.CommandServices;
using neurozen.API.IAM.Application.Internal.QueryServices;
using neurozen.API.IAM.Infrastructure.Pipeline.Middleware.Extensions;
using neurozen.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
// IAM domain interfaces (ensure interface types are in scope)
using neurozen.API.IAM.Domain.Repositories;
using neurozen.API.IAM.Domain.Services;
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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddRouting(options => options.LowercaseUrls = true );
// Configure CORS para permitir peticiones desde el frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5173",  // Vite dev server
                "https://localhost:5173",
                "http://localhost:4173",  // Vite preview
                "https://neurozen-frontend.web.app",  // Producción Firebase
                "https://neurozen-frontend.firebaseapp.com"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
builder.Services.AddEndpointsApiExplorer();

// Add controllers and apply a global authorization filter so every endpoint requires
// authorization by default. Controllers/actions decorated with [AllowAnonymous]
// will skip this filter.
builder.Services.AddControllers(options =>
{
    // Require authenticated user by default for all controllers; AllowAnonymous will opt-out.
    options.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()));
}).AddDataAnnotationsLocalization(options =>
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

// Enable Swashbuckle and configure JWT Bearer support in Swagger UI
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    
    // Configurar para que use los valores por defecto de las propiedades
    options.SchemaFilter<neurozen.API.Shared.Infrastructure.Interfaces.ASP.Configuration.DefaultValueSchemaFilter>();
    
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] { }
        }
    });
});


// Register AppDbContext for all environments so DI is available during startup.
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

// IAM / Identity registrations
// Bind TokenSettings from configuration
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();

// Configure JWT authentication
var tokenSecret = builder.Configuration["TokenSettings:Secret"] ?? Environment.GetEnvironmentVariable("TokenSettings__Secret");
if (string.IsNullOrEmpty(tokenSecret))
    throw new Exception("TokenSettings:Secret must be provided via configuration or TokenSettings__Secret env var");
var key = Encoding.ASCII.GetBytes(tokenSecret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

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

// Verify Database Objects are created and apply schema changes
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    var logger = services.GetRequiredService<ILogger<Program>>();
    
    Console.WriteLine("========================================");
    Console.WriteLine("INICIANDO ACTUALIZACIÓN DE BASE DE DATOS");
    Console.WriteLine("========================================");
    
    try
    {
        var pendingMigrations = context.Database.GetPendingMigrations().ToList();
        
        if (pendingMigrations.Any())
        {
            Console.WriteLine($"⚠ Aplicando {pendingMigrations.Count} migraciones pendientes...");
            logger.LogInformation("Applying {Count} pending migrations...", pendingMigrations.Count);
            context.Database.Migrate();
            logger.LogInformation("Database migrations applied successfully.");
            Console.WriteLine("✓ Migraciones aplicadas exitosamente.");
        }
        else
        {
            Console.WriteLine("ℹ No hay migraciones pendientes.");
            
            // Si no hay migraciones pendientes, asegurar que la BD existe
            var canConnect = context.Database.CanConnect();
            if (!canConnect)
            {
                Console.WriteLine("⚠ Base de datos no existe. Creando...");
                logger.LogInformation("Database does not exist. Creating...");
                context.Database.EnsureCreated();
                logger.LogInformation("Database created successfully.");
                Console.WriteLine("✓ Base de datos creada exitosamente.");
            }
            else
            {
                Console.WriteLine("✓ Conexión a base de datos verificada.");
                logger.LogInformation("Database connection verified.");
                
                // Ejecutar SQL para agregar columnas faltantes de forma segura
                try
                {
                    Console.WriteLine("ℹ Verificando esquema de la tabla user...");
                    logger.LogInformation("Checking user table schema...");
                    
                    // Verificar y agregar columnas una por una con manejo de errores individual
                    var columnsToAdd = new List<(string name, string definition, bool needsUpdate)>
                    {
                        ("email", "VARCHAR(255) NULL", false),
                        ("full_name", "VARCHAR(255) NULL", false),
                        ("phone_number", "VARCHAR(50) NULL", false),
                        ("address", "VARCHAR(500) NULL", false),
                        ("avatar_url", "VARCHAR(500) NULL", false),
                        ("date_of_birth", "DATETIME(6) NULL", false),
                        ("created_at", "DATETIME(6) NULL", true),
                        ("updated_at", "DATETIME(6) NULL", true)
                    };

                    var addedColumns = 0;
                    var existingColumns = 0;
                    
                    foreach (var (name, definition, needsUpdate) in columnsToAdd)
                    {
                        try
                        {
                            var sql = $"ALTER TABLE `user` ADD COLUMN `{name}` {definition}";
                            context.Database.ExecuteSqlRaw(sql);
                            Console.WriteLine($"  ✓ Columna '{name}' agregada a la tabla user.");
                            logger.LogInformation("✓ Added column '{ColumnName}' to user table.", name);
                            addedColumns++;
                            
                            // Si es una columna de timestamp y se acaba de agregar, actualizar registros existentes
                            if (needsUpdate)
                            {
                                try
                                {
                                    var updateSql = $"UPDATE `user` SET `{name}` = CURRENT_TIMESTAMP(6) WHERE `{name}` IS NULL";
                                    context.Database.ExecuteSqlRaw(updateSql);
                                    Console.WriteLine($"  ✓ Registros existentes actualizados para '{name}'.");
                                    logger.LogInformation("✓ Updated existing records with default value for '{ColumnName}'.", name);
                                }
                                catch (Exception updateEx)
                                {
                                    Console.WriteLine($"  ⚠ No se pudieron actualizar registros para '{name}': {updateEx.Message}");
                                    logger.LogWarning("Could not update existing records for '{ColumnName}': {Error}", name, updateEx.Message);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // Columna ya existe, ignorar el error
                            if (ex.Message.Contains("Duplicate column name") || ex.Message.Contains("already exists"))
                            {
                                existingColumns++;
                                Console.WriteLine($"  - Columna '{name}' ya existe.");
                            }
                            else
                            {
                                Console.WriteLine($"  ✗ Error al agregar columna '{name}': {ex.Message}");
                                logger.LogWarning("Failed to add column '{ColumnName}': {Error}", name, ex.Message);
                            }
                        }
                    }
                    
                    Console.WriteLine("");
                    if (addedColumns > 0)
                    {
                        Console.WriteLine($"✓✓✓ ACTUALIZACIÓN COMPLETADA: {addedColumns} columnas nuevas agregadas.");
                        logger.LogInformation("✓ User profile schema update completed. Added {Count} new columns.", addedColumns);
                    }
                    else if (existingColumns > 0)
                    {
                        Console.WriteLine($"✓ Esquema actualizado ({existingColumns} columnas ya existían).");
                        logger.LogInformation("✓ User table schema is up to date ({Count} columns already exist).", existingColumns);
                    }
                    else
                    {
                        Console.WriteLine("⚠⚠⚠ ADVERTENCIA: No se agregaron columnas. Puede haber un problema.");
                        logger.LogWarning("⚠ No columns were added. This might indicate a problem.");
                    }
                }
                catch (Exception sqlEx)
                {
                    Console.WriteLine($"✗✗✗ ERROR durante actualización de esquema: {sqlEx.Message}");
                    Console.WriteLine($"Stack trace: {sqlEx.StackTrace}");
                    logger.LogError(sqlEx, "❌ Error during schema update: {Message}", sqlEx.Message);
                    logger.LogWarning("⚠ The application may not work correctly until the database schema is updated manually.");
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"✗✗✗ ERROR CRÍTICO durante inicialización de base de datos:");
        Console.WriteLine($"Mensaje: {ex.Message}");
        Console.WriteLine($"Stack trace: {ex.StackTrace}");
        logger.LogError(ex, "Error during database initialization: {Message}", ex.Message);
        throw;
    }
    
    Console.WriteLine("========================================");
    Console.WriteLine("INICIALIZACIÓN DE BASE DE DATOS COMPLETADA");
    Console.WriteLine("========================================");
    Console.WriteLine("");
}
app.UseRequestLocalization(LocalizationOptions);
app.UseCors("AllowFrontend");
app.UseHttpsRedirection();
// Authentication middleware must run before authorization
app.UseAuthentication();
// Custom request authorization middleware (validates JWT and sets HttpContext.Items["User"])
app.UseRequestAuthorization();
app.UseAuthorization();
app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();


app.Run();
