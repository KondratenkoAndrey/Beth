using Beth.Identity.Domain.Authenticate;
using Beth.Identity.Domain.Interfaces;
using Beth.Identity.Domain.Services;
using Beth.Identity.Infrastructure;
using Beth.Identity.Infrastructure.Repositories;
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
    var configuration = ConfigurationOptions.Parse(builder.Configuration["ConnectionStrings:redisDb"], true);
    return ConnectionMultiplexer.Connect(configuration);
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();