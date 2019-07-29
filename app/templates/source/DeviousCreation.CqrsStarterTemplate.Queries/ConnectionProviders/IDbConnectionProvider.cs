using System.Data;

namespace DeviousCreation.CqrsStarterTemplate.Queries.ConnectionProviders
{
    public interface IDbConnectionProvider
    {
        IDbConnection Connection { get; }
    }
}