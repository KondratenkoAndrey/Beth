using Beth.Identity.Domain.Authenticate;
using Beth.Identity.Domain.Interfaces;
using Beth.Identity.Domain.Services;
using Beth.Identity.Infrastructure;
using Beth.Identity.Infrastructure.Repositories;
using Beth.SharedKernel.EventBusRabbitMQ.Extensions;
using Beth.SharedKernel.EventBusRabbitMQ.Settings;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<OneTimeCodeSettings>(builder.Configuration.GetSection("OneTimeCodeSettings"));

builder.Services.AddScoped<IOneTimeCodeService, OneTimeCodeService>();
builder.Services.AddScoped<IOneTimeCodeRepository, OneTimeCodeRepository>();
builder.Services.AddScoped<IOneTimeCodeSender, FakeOneTimeCodeSender>();
builder.Services.AddScoped<IUserRepository, FakeUserRepository>();

builder.Services.AddSingleton<ConnectionMultiplexer>(sp =>
{
    var configuration = ConfigurationOptions.Parse(builder.Configuration["ConnectionStrings:RedisDb"], true);
    return ConnectionMultiplexer.Connect(configuration);
});

builder.Services.AddMassTransitEventBus(builder.Configuration.GetSection("RabbitMQSettings").Get<RabbitMQSettings>());

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();