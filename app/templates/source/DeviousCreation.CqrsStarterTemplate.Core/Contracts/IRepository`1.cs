namespace DeviousCreation.CqrsStarterTemplate.Core.Contracts
{
    public interface IRepository<T>
        where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}