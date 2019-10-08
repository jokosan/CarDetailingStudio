using CarDetailingStudio.DAL.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.DAL.Infrastructure
{
    public class DbRepository<T> : IGenericRepository<T> where T : class
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
            if (entityToUpdate == null)
            {
                throw new ArgumentException("Cannot add a null entity.");
            }
            var entry = _carWashEntitiesContext.Entry<T>(entityToUpdate);

            //if (entry.State == EntityState.Detached)
            //{
            //    var set = _carWashEntitiesContext.Set<T>();
            //    T attachedEntity = set.Local.SingleOrDefault(e => e.Id == entityToUpdate.Id); // You need to have access to key

            //    if (attachedEntity != null)
            //    {
            //        var attachedEntry = _carWashEntitiesContext.Entry(attachedEntity);
            //        attachedEntry.CurrentValues.SetValues(entityToUpdate);
            //    }
            //    else
            //    {
            //        entry.State = EntityState.Modified; // This should attach entity
            //    }
            //}
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
    }
}

