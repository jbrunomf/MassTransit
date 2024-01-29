using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configuração do MassTransit
var configuration = builder.Configuration;
var connection = configuration.GetSection("MassTransitAzure").GetValue<string>("ConnectionString") ?? string.Empty;


builder.Services.AddMassTransit((x =>
{
    x.UsingAzureServiceBus((context, cfg) =>
    {
        cfg.Host(connection);
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
