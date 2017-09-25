using Msit115BestOne.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Msit115BestOne.Areas.ShoppingCart.Models
{
    public class Repository<T> : IRepository<T> where T:class
    {
        private ManPowerEntities db = null;
        private DbSet<T> Dbset = null;

        public Repository()
        {
            db = new ManPowerEntities();
            Dbset = db.Set<T>();
        }
        public void Create(T _entity)
        {
            Dbset.Add(_entity);
            db.SaveChanges();
        }

        public void Delete(T _entity)
        {
            Dbset.Remove(_entity);
            db.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return Dbset;
        }

        public T GetById(int id)
        {
            var aaa = Dbset.Find(id);
            return aaa;
        }

        public void Update(T _entity)
        {
            db.Entry(_entity).State = EntityState.Modified;
            db.SaveChanges();
        }  
    }
}