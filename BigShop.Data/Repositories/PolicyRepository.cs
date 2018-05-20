using BigShop.Data.Infrastructure;
using BigShop.Model.Models;

namespace BigShop.Data.Repositories
{
    public interface IPolicyRepository : IRepository<Policy>
    {
    }

    public class PolicyRepository : RepositoryBase<Policy>, IPolicyRepository
    {
        public PolicyRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}