﻿using DynamicAuth_RBAC_.Models;
using System.Linq.Expressions;

namespace DynamicAuth_RBAC_.Repositories
{
    public interface IRepository<T> where T : BaseModel
    {
        Task<T> First(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        Task<T> GetByIDAsync(int id);
        Task<T> GetWithTrackinByIDAsync(int id);
        Task<T> AddAsync(T entity);
        T Update(T entity);
        void Delete(T entity);
        void Delete(int id);
        Task SaveChangesAsync();
    }
}
