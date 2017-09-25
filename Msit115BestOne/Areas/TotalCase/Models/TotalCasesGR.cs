using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Msit115BestOne.Models;
using System.Data.Entity;

namespace Msit115BestOne.Areas.TotalCase.Models
{
    public class TotalCasesGR<T> : ITotalCasesGR<T> where T : class
    {
        private ManPowerEntities db = null;
        private DbSet<T> Dbset = null;

        public TotalCasesGR()
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
            return Dbset; //ToList
        }

        public T GetById(int id)
        {
            return Dbset.Find(id);
        }

        public void Update(T _entity)
        {
            db.Entry(_entity).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}