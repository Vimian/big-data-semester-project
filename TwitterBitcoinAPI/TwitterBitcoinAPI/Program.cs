internal class Program {
    private static void Main(string[] args) {

        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddEndpointsApiExplorer();

        var MyAllowSpecificOrigins = "AllowCore";

        builder.Services.AddCors(options => {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                policy => {
                    policy.AllowCredentials().AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(origin => true);
                });
        });

        builder.Services.AddControllers();

        var app = builder.Build();

        app.UseHttpsRedirection();

        app.UseCors(MyAllowSpecificOrigins);

        app.UseAuthorization();

        app.MapControllers();

        app.MapControllers();
        Console.WriteLine(app.MapControllers());
        app.Run();
    }
}