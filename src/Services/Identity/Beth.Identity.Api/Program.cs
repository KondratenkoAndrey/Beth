using Beth.Identity.Domain.Interfaces;
using Beth.Identity.Domain.Services;
using Beth.Identity.Infrastructure;
using Beth.Identity.Infrastructure.Repositories;

namespace Beth.Identity.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            builder.Services.AddScoped<IOneTimeCodeService, OneTimeCodeService>();

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddScoped<IUserRepository, FakeUserRepository>();
                builder.Services.AddScoped<IOneTimeCodeRepository, FakeOneTimeCodeRepository>();
                builder.Services.AddScoped<IOneTimeCodeSender, FakeOneTimeCodeSender>();
            }
            else
            {
            }

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}