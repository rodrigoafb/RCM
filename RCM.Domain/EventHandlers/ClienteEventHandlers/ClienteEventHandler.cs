﻿using MediatR;
using RCM.Domain.Events.ClienteEvents;
using System.Threading;
using System.Threading.Tasks;

namespace RCM.Domain.EventHandlers.ClienteEventHandlers
{
    public class ClienteEventHandler : DomainEventHandler<ClienteEvent>,
                                       INotificationHandler<AddedClienteEvent>,
                                       INotificationHandler<UpdatedClienteEvent>,
                                       INotificationHandler<RemovedClienteEvent>
    {
        public Task Handle(AddedClienteEvent notification, CancellationToken cancellationToken)
        {
            return Response();
        }

        public Task Handle(UpdatedClienteEvent notification, CancellationToken cancellationToken)
        {
            return Response();
        }

        public Task Handle(RemovedClienteEvent notification, CancellationToken cancellationToken)
        {
            return Response();
        }
    }
}
