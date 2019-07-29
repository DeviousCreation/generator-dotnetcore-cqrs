using System;
using System.Collections.Generic;
using MediatR;

namespace DeviousCreation.CqrsStarterTemplate.Core.Contracts
{
    public interface IEntity
    {
        List<INotification> DomainEvents { get; }

        Guid Id { get; }

        void AddDomainEvent(INotification eventItem);

        bool Equals(object obj);

        int GetHashCode();

        bool IsTransient();

        void RemoveDomainEvent(INotification eventItem);
    }
}