using Netwise.Model;

namespace Netwise.Interfaces
{
    public interface IService
    {
        Task<CatFact> GetFact();
    }
}