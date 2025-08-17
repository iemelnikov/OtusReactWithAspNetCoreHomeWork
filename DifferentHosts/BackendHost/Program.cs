namespace AspNetCoreWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string Origin = "MyAllowSpecificOrigins";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                    name: Origin,
                    corsBuilder =>
                    {
                        var origins = builder.Configuration.GetSection("CORS:Origins").Get<string[]>() ?? [];
                        var headers = builder.Configuration.GetSection("CORS:Headers").Get<string[]>() ?? [];
                        var methods = builder.Configuration.GetSection("CORS:Methods").Get<string[]>() ?? [];

                        corsBuilder
                            .WithOrigins(origins)
                            .WithHeaders(headers)
                            .WithMethods(methods);
                    }
                );
            });

            // Add services to the container.

            builder.Services.AddControllers();

            var app = builder.Build();

            app.UseCors(Origin);

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
