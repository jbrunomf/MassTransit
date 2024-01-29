﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using MassTransit;

namespace Consumer.Events
{
    public class PedidoCriadoConsumidor : IConsumer<Pedido>
    {
        public Task Consume(ConsumeContext<Pedido> context)
        {
            Console.WriteLine(context.Message);
            return Task.CompletedTask;
        }
    }
}
