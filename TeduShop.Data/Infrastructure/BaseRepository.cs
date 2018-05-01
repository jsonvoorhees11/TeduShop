using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Data.Infrastructure
{
    public abstract class BaseRepository<T>:IRepository<T> where T:class
    {
        #region Properties
        private TeduShopDbContext _dataContext;
        private readonly IDbSet<T> _dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected TeduShopDbContext DbContext
        {
            get { return _dataContext ?? (_dataContext= DbFactory.Init()) ; }
        }
        #endregion

        protected BaseRepository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            _dbSet = DbContext.Set<T>();
        }

        #region Implementation
        public virtual T Add(T entity)
        {
            return _dbSet.Add(entity);
        }
        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual T Delete(T entity)
        {
            return _dbSet.Remove(entity);
        }

        public virtual T Delete(int id)
        {
            var entity = _dbSet.Find(id);
            return _dbSet.Remove(entity);
        }

        public virtual void DeleteMulti(Expression<Func<T,bool>> where)
        {
            IEnumerable<T> objects = _dbSet.Where<T>(where).AsEnumerable();
            foreach (var item in objects)
            {
                _dbSet.Remove(item);
            }
        }

        public virtual T GetSingleById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T,bool>> where,string includes)
        {
            return _dbSet.Where<T>(where);
        }


        public virtual int Count(Expression<Func<T,bool>> where)
        {
            return _dbSet.Count<T>(where);
        }

        public IEnumerable<T> GetAll(string[] includes=null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = _dataContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                {
                    query = query.Include(include);

                }
                return query.AsQueryable();
            }

            return _dataContext.Set<T>().AsQueryable();
        }

        public T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = _dataContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.FirstOrDefault(expression);
            }
            return _dataContext.Set<T>().FirstOrDefault(expression);
        }

        public virtual IEnumerable<T> GetMulti(Expression<Func<T,bool>> predicate, string[] includes=null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = _dataContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.Where<T>(predicate).AsQueryable<T>();
            }

            return _dataContext.Set<T>().Where(predicate).AsQueryable<T>();
        }

        public virtual IEnumerable<T> GetMultiPaging(Expression<Func<T,bool>> predicate, out int total,
            int index=0,int size=20, string[] includes = null)
        {
            int skipCount = index * size;
            IQueryable<T> resetSet;

            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if(includes!=null && includes.Count() > 0)
            {
                var query = _dataContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                {
                    query.Include(include);
                }
                resetSet = predicate != null ? query.Where<T>(predicate).AsQueryable() : query.AsQueryable();
            }
            else
            {
                resetSet = predicate != null ? _dataContext.Set<T>().Where<T>(predicate).AsQueryable() : _dataContext.Set<T>().AsQueryable();
            }

            resetSet = skipCount == 0 ? resetSet.Take(size) : resetSet.Skip(skipCount).Take(size);
            total = resetSet.Count();
            return resetSet.AsQueryable();
        }

        public bool CheckContains(Expression<Func<T,bool>> predicate)
        {
            return _dataContext.Set<T>().Count<T>(predicate) > 0;
        }

        #endregion
    }
}