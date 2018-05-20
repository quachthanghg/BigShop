using BigShop.Data.Infrastructure;
using BigShop.Model.Models;

namespace BigShop.Data.Repositories
{
    public interface ISaleRepository : IRepository<Sale>
    {
    }

    public class SaleRepository : RepositoryBase<Sale>, ISaleRepository
    {
        public SaleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}