using System.Diagnostics.CodeAnalysis;

namespace DddInfrastructure
{
    [SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "Requred for design purposes")]
    public interface IAsyncRepository<T> where T : IAggregateRoot { }
}
