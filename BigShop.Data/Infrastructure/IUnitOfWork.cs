namespace BigShop.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}