using backend.Repositories;
using backend.Middleware;

class Program
{
    static void Main(string[] args)
    {
        // Initialize the builder with args
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container
        builder.Services.AddSingleton<DatabaseRepository>();
        builder.Services.AddControllers();

        // Add CORS service
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalhost", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        WebApplication app = builder.Build();

        // Use the custom middleware
        app.UseMiddleware<CustomJsonStreamMiddleware>();

        // Global exception handling middleware with a route
        app.UseExceptionHandler("/api/error");

        app.UseCors("AllowLocalhost");
        // Map controller routes
        app.MapControllers();

        app.Urls.Add("http://localhost:5001");

        // Start the application
        app.Run();
    }
}
