using Core;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Producer.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly IConfiguration _configuration;

        public PedidoController(IBus bus, IConfiguration configuration)
        {
            _bus = bus;
            _configuration = configuration;
        }


        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var nomeFila = _configuration.GetSection("MassTransit").GetValue<string>("QueueName") ?? string.Empty;

            var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{nomeFila}"));

            await endpoint.Send(new Pedido()
            {
                Id = Guid.NewGuid(),
                Usuario = new Usuario(Guid.NewGuid(), "João Bruno", "jbrunomf@outlook.com"),
                DataCriacao = DateTime.Now
            });

            return Ok();
        }
    }
}
