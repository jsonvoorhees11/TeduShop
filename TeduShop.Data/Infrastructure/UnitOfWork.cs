namespace TeduShop.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private TeduShopDbContext _dbContext;
        private readonly IDbFactory _dbFactory;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this._dbFactory = dbFactory;
        }

        public TeduShopDbContext DbContext
        {
            get { return _dbContext ?? _dbFactory.Init(); }
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }
    }
}