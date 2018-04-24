using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            get { return _dbContext ?? (_dbContext = new TeduShopDbContext()); }
        }
        public void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}
