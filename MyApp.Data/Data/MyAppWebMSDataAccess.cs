using MyApp.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyApp.Data.Data
{
    public class MyAppWebMSDataAccess : IDataAccess
    {
        private readonly MyAppWebMSContext _myAppWebMSContext;

        public MyAppWebMSDataAccess(MyAppWebMSContext myAppWebMSContext)
        {
            _myAppWebMSContext = myAppWebMSContext;
        }


        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : ModelBase
        {
            return _myAppWebMSContext.Set<TEntity>().Select(p => p);
        }

        public TEntity GetById<TEntity>(int id) where TEntity : ModelBase
        {
            return GetAll<TEntity>().FirstOrDefault(p => p.Id.Equals(id));
        }

        public TEntity Create<TEntity>(TEntity entity) where TEntity : ModelBase
        {
            var items = this.GetAll<TEntity>();

            // Mimic database auto ID assigning
            entity.Id = items.Max(p => p.Id) + 1;

            _myAppWebMSContext.Set<TEntity>().Add(entity);

            return entity;
        }

        public TEntity Update<TEntity>(TEntity entity) where TEntity : ModelBase
        {
            var originalEtity = GetAll<TEntity>().FirstOrDefault(p => p.Id.Equals(entity.Id));

            if (originalEtity == null)
            {
                throw new NullReferenceException("The entity does not exist in the data store");
            }

            // remove the original entity
            _myAppWebMSContext.Set<TEntity>().Remove(originalEtity);

            // add the updated entity
            _myAppWebMSContext.Set<TEntity>().Add(entity);

            return entity;
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : ModelBase
        {
            var originalEtity = GetAll<TEntity>().FirstOrDefault(p => p.Id.Equals(entity.Id));

            if (originalEtity == null)
            {
                throw new NullReferenceException("The entity does not exist in the data store");
            }

            // remove the original entity
            _myAppWebMSContext.Set<TEntity>().Remove(originalEtity);
        }
    }



}
