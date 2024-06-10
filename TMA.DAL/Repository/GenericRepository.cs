using TMA.DAL.Data;
using TMA.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMA.DAL.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<TEntity> _entities;
        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = _dbContext.Set<TEntity>();
        }
        public TEntity AddItem(TEntity item)
        {
            _entities.Add(item);
            _dbContext.SaveChanges();
            return item;
        }

        public void Delete(int id)
        {
            var dbItem = _entities.Find(id);
            _entities.Remove(dbItem);
            _dbContext.SaveChanges();
        }

        public TEntity GetById(int id)
        {
            var dbItem = _entities.Find(id);
            return dbItem;
        }

        public List<TEntity> GetList()
        {
            var dbItem = _entities.ToList();
            return dbItem;
        }

        public TEntity Update(TEntity item)
        {
            _entities.Update(item);
            _dbContext.SaveChanges();
            return item;
        }
    }
}
