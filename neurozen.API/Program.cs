using neurozen.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddRouting(options => options.LowercaseUrls = true );

builder.Services.AddEndpointsApiExplorer();
// Enable Swashbuckle Annotations so [SwaggerOperation]/[SwaggerResponse] attributes are recognized
builder.Services.AddSwaggerGen();


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

var app = builder.Build();

// Verify Database Objects are created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();

    /*
    var blog1 = new Blog()
    {
        BlogId = 1,
        Title = "Hello",
        Url = "1231231"
    };
    context.Blogs.Add(blog1);  
    context.SaveChanges();
    */
}
app.UseHttpsRedirection();


app.UseSwagger();
app.UseSwaggerUI();


app.Run();
