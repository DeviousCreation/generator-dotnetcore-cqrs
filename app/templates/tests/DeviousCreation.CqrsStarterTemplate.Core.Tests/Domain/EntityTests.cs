using System;
using System.Collections.Generic;
using System.Text;
using DeviousCreation.CqrsStarterTemplate.Core.Domain;
using MediatR;
using Moq;
using Xunit;

namespace DeviousCreation.CqrsStarterTemplate.Core.Tests.Domain
{
    public class EntityTests
    {
        [Fact]
        public void IsTransient_NoIdSet_ExpectTrue()
        {
            var stubEntity = new StubEntity();
            Assert.True(stubEntity.IsTransient());
        }

        [Fact]
        public void IsTransient_IdSet_ExpectFalse()
        {
            var stubEntity = new StubEntity(Guid.NewGuid());
            Assert.False(stubEntity.IsTransient());
        }

        [Fact]
        public void AddDomainEvent_GivenValidItem_EventGetsAdded()
        {
            var notification = new Mock<INotification>();
            var stubEntity = new StubEntity(Guid.NewGuid());
            stubEntity.AddDomainEvent(notification.Object);
            Assert.Contains(notification.Object, stubEntity.DomainEvents);
        }

        [Fact]
        public void RemoveDomainEvent_GivenValidItem_EventGetsRemoved()
        {
            var notification = new Mock<INotification>();
            var stubEntity = new StubEntity(Guid.NewGuid());
            stubEntity.AddDomainEvent(notification.Object);
            stubEntity.RemoveDomainEvent(notification.Object);
            Assert.DoesNotContain(notification.Object, stubEntity.DomainEvents);
        }

        private sealed class StubEntity : Entity
        {
            public StubEntity()
            {
            }

            public StubEntity(Guid id)
            {
                this.Id = id;
            }
        }
    }
}