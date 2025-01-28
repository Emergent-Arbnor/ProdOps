class Program
{
    static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(); // Initialize the builder

        
        // Add services to the container
        IServiceCollection services = builder.Services;
        services.AddControllers(); // Enables controllers for your app

        WebApplication app = builder.Build(); // Finalize the app

        // Enforce HTTPS redirection to ensure that all HTTP traffic is redirected to HTTPS.
        app.UseHttpsRedirection();

        // Map controller routes
        app.MapControllers();

        app.Run(); // Start the application
    }
}