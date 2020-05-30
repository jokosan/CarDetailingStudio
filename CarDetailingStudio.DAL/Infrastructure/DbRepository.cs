using CarDetailingStudio.DAL.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;

namespace CarDetailingStudio.DAL.Infrastructure
{
    public class DbRepository<T> : IGetRepository<T>, IDefaultRepository<T>, IGetInclude<T> where T : class
    {

        internal carWashEntities _carWashEntitiesContext;
        internal DbSet<T> DbSeT;

        public DbRepository(carWashEntities entities)
        {
            _carWashEntitiesContext = entities;
            DbSeT = entities.Set<T>();
        }

        public virtual IEnumerable<T> Get()
        {
            Lazy<T> lazy = new Lazy<T>();

            var query = DbSeT.AsEnumerable<T>().AsQueryable();

            return query;
        }

        public virtual T GetById(int? id)
        {
            return DbSeT.Find(id);
        }

        public virtual void Insert(T entity)
        {
            DbSeT.Add(entity);
        }

        public void Insert(List<T> entity)
        {
            DbSeT.AddRange(entity);
        }

        public virtual void Delete(object id)
        {
            T entityToDelete = DbSeT.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (_carWashEntitiesContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSeT.Attach(entityToDelete);
            }

            DbSeT.Remove(entityToDelete);
        }

        public virtual void Update(T entityToUpdate)
        {
            _carWashEntitiesContext.Set<T>().AddOrUpdate(entityToUpdate);
            //  _carWashEntitiesContext.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void AttachStubs(object[] stubs)
        {
            if (stubs == null)
            {
                throw new ArgumentNullException("stubs");
            }

            foreach (var stub in stubs)
                _carWashEntitiesContext.Set(stub.GetType()).Attach(stub);
        }

        public IEnumerable<T> GetWhere(Func<T, bool> predicate)
        {
            var result = DbSeT.AsQueryable();
            result = result.Where(predicate).AsQueryable();
            return result;
            // return DbSeT.AsEnumerable<T>().AsQueryable().Where(predicate);
        }

        public IEnumerable<T> GetWhere(Func<T, bool> predicate, string children)
        {
            var result = DbSeT.AsQueryable();
            result = result.Include(children).Where(predicate).AsQueryable();
            return result;
        }


        public IEnumerable<T> QueryObjectGraph(Expression<Func<T, bool>> filter, string children)
        {
            return DbSeT.Include(children).Where(filter);
        }

        public IEnumerable<T> QueryObjectGraph(Expression<Func<T, bool>> filter, List<string> children)
        {
            foreach (var property in children)
            {
                DbSeT.Include(property.ToString());
            }

            return DbSeT.Where(filter);
        }

        public IEnumerable<T> GetInclude(string children)
        {
            var query = DbSeT.Include(children).AsEnumerable<T>().AsQueryable();

            return query;
        }

        public T IdInclude(int id)
        {
            return DbSeT.Find(id);
        }

        public IEnumerable<T> Item(Expression<Func<T, bool>> wherePredicate, params Expression<Func<T, object>>[] includeProperties)
        {
            foreach (var property in includeProperties)
            {
                DbSeT.Include(property.ToString());
            }
            return DbSeT.Where(wherePredicate);
        }
    }
}


