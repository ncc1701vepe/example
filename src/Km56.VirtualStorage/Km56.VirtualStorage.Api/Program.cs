using km56.VirtualStorage.Application.Extension;
using Serilog;

namespace km56.VirtualStorage.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var logger = new LoggerConfiguration()
                .MinimumLevel.Information() 
                .WriteTo.Console()
                .WriteTo.File(@"Logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            builder.Services.AddStorageServices();

            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { 
                    Title = "Km56 Virtual Storage API Service",
                    Version = "v1.0",
                    Description = "Provides operations to interact with Storage repositories in the cloud",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact { 
                        Name = "Vicente Perez Escobar",
                        Email = "veperez@hotmail.com",
                        Url = new Uri("https://github.com/ncc1701vepe")
                    }
                });
            }); 

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}