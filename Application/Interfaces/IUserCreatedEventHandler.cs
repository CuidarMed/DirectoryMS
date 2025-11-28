using Application.Contracts.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserCreatedEventHandler
    {
        Task HandleAsync(UserCreatedEvent @event, CancellationToken cancellationToken = default);
    }
}
