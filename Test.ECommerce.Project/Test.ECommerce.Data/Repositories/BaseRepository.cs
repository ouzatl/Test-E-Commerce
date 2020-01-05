﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;

namespace Test.ECommerce.Data.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(TestECommerceContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext can not be null.");

            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }
        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }
        public T GetById(string id)
        {
            return _dbSet.Find(id);
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
        }
        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public void Delete(T entity)
        {
            if (entity.GetType().GetProperty("IsActive") != null)
            {
                T _entity = entity;

                _entity.GetType().GetProperty("IsActive").SetValue(_entity, false);

                this.Update(_entity);
            }
            else
            {
                EntityEntry dbEntityEntry = _dbContext.Entry(entity);

                if (dbEntityEntry.State != EntityState.Deleted)
                {
                    dbEntityEntry.State = EntityState.Deleted;
                }
                else
                {
                    _dbSet.Attach(entity);
                    _dbSet.Remove(entity);
                }
            }
        }
        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null) return;
            else
            {
                if (entity.GetType().GetProperty("IsActive") != null)
                {
                    T _entity = entity;
                    _entity.GetType().GetProperty("IsActive").SetValue(_entity, false);

                    this.Update(_entity);
                }
                else
                {
                    Delete(entity);
                }
            }
        }
        public void Delete(string id)
        {
            var entity = GetById(id);
            if (entity == null) return;
            else
            {
                if (entity.GetType().GetProperty("IsActive") != null)
                {
                    T _entity = entity;
                    _entity.GetType().GetProperty("IsActive").SetValue(_entity, false);

                    this.Update(_entity);
                }
                else
                {
                    Delete(entity);
                }
            }
        }
    }
}