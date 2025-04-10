﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VideoGames.Data.Abstract
{
    public interface IGenericRepository<T> where T : class
    {

        Task<T> GetByIdAsync(int id);

        Task<T> GetByIdAsync(
            Expression<Func<T,bool>> predicate,
            params Func<IQueryable<T>, IQueryable<T>>[] includes);

        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            params Func<IQueryable<T>, IQueryable<T>>[] includes);

        Task<T> FindAsync(Expression<Func<T, bool>> predicate);

        Task<T> AddAsync(T entity);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

        void Update(T entity);


        
        void Delete(T entity);


       
        Task<int> CountAsync();

       
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

    }
}
