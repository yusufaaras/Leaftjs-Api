using Microsoft.EntityFrameworkCore;
using MediatR;
using MoskBilisimAPI.Context;
using MoskBilisimAPI.Interfaces;
using MoskBilisimAPI.Repositories;

namespace MoskBilisimAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // DbContext ve Repository kayıtları
            builder.Services.AddDbContext<MoskBilisimDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // MediatR için kayıt
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            // CORS ayarları
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            // Diğer servisler
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // HTTP istek boru hattını yapılandır
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAllOrigins");
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
