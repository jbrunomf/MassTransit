using Consumer;
using Consumer.Events;
using Core;
using MassTransit;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;
        var nomeFila = configuration.GetSection("MassTransitAzure").GetValue<string>("NomeFila") ?? string.Empty;
        var connectionString = configuration.GetSection("MassTransitAzure").GetValue<string>("ConnectionString") ?? string.Empty;

        services.AddHostedService<Worker>();

        services.AddMassTransit(x =>
        {
            x.UsingAzureServiceBus((context, cfg) =>
            {
                cfg.Host(connectionString);

                cfg.ReceiveEndpoint(nomeFila, e =>
                {
                    e.Consumer<PedidoCriadoConsumidor>();
                });
            });
        });
    })
    .Build();

host.Run();
