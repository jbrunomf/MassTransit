using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configuração do MassTransit
var configuration = builder.Configuration;
var servidor = configuration.GetSection("MassTransit").GetValue<string>("ServerName") ?? string.Empty;
var usuario = configuration.GetSection("MassTransit").GetValue<string>("Username") ?? string.Empty;
var password = configuration.GetSection("MassTransit").GetValue<string>("Password") ?? string.Empty;

builder.Services.AddMassTransit((x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(servidor, "/", c =>
        {
            c.Username(usuario);
            c.Password(password);
        });

        cfg.ConfigureEndpoints(context);
    });
}));


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
