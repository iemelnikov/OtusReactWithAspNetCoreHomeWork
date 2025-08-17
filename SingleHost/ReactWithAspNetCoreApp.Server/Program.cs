namespace ReactWithAspNetCoreApp.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            if (builder.Environment.IsProduction())
            {
                // Настраиваем SPA
                builder.Services.AddSpaStaticFiles(options =>
                {
                    options.RootPath = "ClientApp"; // Путь к собранным статическим файлам
                });
            }
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHttpsRedirection();
                app.UseRouting();
                app.UseAuthorization();
                app.MapControllers();
            }
            else
            {
                // В Production обслуживаем статические файлы
                app.UseHttpsRedirection();
                app.UseStaticFiles();
                app.UseSpaStaticFiles();
                app.UseRouting();
                app.UseAuthorization();
                app.MapControllers();
                app.MapFallbackToFile("index.html");
            }

            app.Run();
        }
    }
}
