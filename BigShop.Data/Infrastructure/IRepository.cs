using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BigShop.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);

        void Update(T entity);

        T Delete(T entity);

        T Delete(int id);

        void DeleteMulti(Expression<Func<T, bool>> where);

        T GetSignleById(int id);

        T GetSignleByCondition(Expression<Func<T, bool>> expression, string[] includes = null);

        IQueryable<T> GetAll(string[] includes = null);

        IEnumerable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null);

        IQueryable<T> GetMultiPaging(Expression<Func<T, bool>> filter, out int total, int page = 0, int pageSize = 20, string[] includes = null);

        int Count(Expression<Func<T, bool>> where);

        bool CheckContains(Expression<Func<T, bool>> predicate);
    }
}