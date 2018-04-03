namespace BigShop.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private BigShopDbContext dbContext;

        public BigShopDbContext DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }
    }
}