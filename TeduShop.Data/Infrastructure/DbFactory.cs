using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private TeduShopDbContext _dbContext;
        public TeduShopDbContext Init()
        {
            return _dbContext ?? (_dbContext = new TeduShopDbContext());
        }
        protected override void DisposeCore()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
        }
    }
}
