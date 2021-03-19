using CarDetailingStudio.DAL.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

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

        public virtual async  Task<IEnumerable<T>> Get()
        {
            Lazy<T> lazy = new Lazy<T>();

            var query = await DbSeT.AsEnumerable<T>().AsQueryable().AsNoTracking().ToListAsync();

            return query;
        }

        public virtual async Task<T> GetById(int? id)
        {
            return await DbSeT.FindAsync(id);
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

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
        {                  
            return await DbSeT.Where(predicate).AsNoTracking().AsQueryable().ToListAsync();
        }
       
        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate, string children)
        {
             return await DbSeT.Include(children).Where(predicate).AsQueryable().ToListAsync();            
        }

        public async Task<IEnumerable<T>> QueryObjectGraph(Expression<Func<T, bool>> filter)
        {
            return await DbSeT.Where(filter).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> QueryObjectGraph(Expression<Func<T, bool>> filter, string children)
        {
            return await DbSeT.Include(children).Where(filter).AsNoTracking().ToListAsync();
        }            

        public async Task<IEnumerable<T>> QueryObjectGraph(Expression<Func<T, bool>> filter, string childrenOne, string childrenTwo)
        {
            return await DbSeT.Include(childrenOne).Include(childrenTwo).Where(filter).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> QueryObjectGraph(Expression<Func<T, bool>> filter, string childrenOne, string childrenTwo, string childrenThree)
        {
            return await DbSeT.Include(childrenOne).Include(childrenTwo).Include(childrenThree).Where(filter).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetInclude(string children)
        {
            return await DbSeT.Include(children).AsNoTracking().AsEnumerable<T>().AsQueryable().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetInclude(string childrenOne, string childrenTwo)
        {
            return await DbSeT.Include(childrenOne).Include(childrenTwo).AsNoTracking().AsEnumerable<T>().AsQueryable().ToListAsync();
        }

        public async Task<T> IdInclude(int id)
        {
            return await DbSeT.FindAsync(id);
        }
    }
}