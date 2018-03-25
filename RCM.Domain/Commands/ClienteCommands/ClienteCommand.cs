﻿using RCM.Domain.Core.Commands;
using RCM.Domain.Models.ClienteModels;

namespace RCM.Domain.Commands.ClienteCommands
{
    public abstract class ClienteCommand : Command
    {
        public Cliente Cliente { get; }

        public ClienteCommand(Cliente cliente) 
        {
            Cliente = cliente;
        }
    }
}
