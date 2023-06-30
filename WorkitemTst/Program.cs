using System.Configuration;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.TeamFoundation.TestManagement.WebApi;
using WorkitemTst.Facade;
using WorkitemTst.Models;

namespace WorkitemTst
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddSingleton<Tfs, Tfs>();

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://example.com",
                                                          "https://localhost:4200",
                                                          "http://localhost:4200");
                                  });
            });


            builder.Services.AddSingleton(builder.Configuration.GetSection("AppOptions").Get<AppOptions>());


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
                    //c.OrderActionsBy(apiDesc => $"{apiDesc.ActionDescriptor.RouteValues["Tfs"]}_{apiDesc.RelativePath}");
                    c.OrderActionsBy((apiDesc) => $"{apiDesc.RelativePath}");
                });
        

            var connectionString = builder.Configuration.GetConnectionString("WIConnection");


            builder.Services.AddDbContext<AppDBContext>(options => {
                options.UseSqlServer(connectionString);
            });



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(
                    c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    });
            }

            app.UseHttpsRedirection();

            //app.UseCors(MyAllowSpecificOrigins);


            app.UseCors(x => x
.AllowAnyMethod()
.AllowAnyHeader()
.SetIsOriginAllowed(origin => true) // allow any origin
.AllowCredentials()); // allow credentials



            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}