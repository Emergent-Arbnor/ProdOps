using backend;

class Program
{
    static void Main(string[] args)
    {
        // Initialize the builder
        WebApplicationBuilder builder = WebApplication.CreateBuilder();

        
        // Add services to the container
        IServiceCollection services = builder.Services;
        // Enables controllers for your app
        services.AddControllers(); 


        WebApplication app = builder.Build();

        app.Use(async (context, next) =>
        {
            // Replacing the request body with a custom stream to handle duplicates
            context.Request.Body = new CustomJsonStream(context.Request.Body);

            // Continue processing
            await next(); 
        });

        // Enforce HTTPS redirection to ensure that all HTTP traffic is redirected to HTTPS.
        app.UseHttpsRedirection();

        // Map controller routes
        app.MapControllers();

        // Start the application
        app.Run();
    }
}