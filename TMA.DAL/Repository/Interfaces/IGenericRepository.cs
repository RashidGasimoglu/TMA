using System;
using System.Collections.Generic;
using System.Text;

namespace TMA.DAL.Repository.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public TEntity AddItem(TEntity item);
        public List<TEntity> GetList();
        public TEntity GetById(int id);
        public TEntity Update(TEntity item);
        public void Delete(int id);
    }
}
