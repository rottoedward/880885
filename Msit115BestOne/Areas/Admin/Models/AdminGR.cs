using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Msit115BestOne.Models;
using System.Data.Entity;
using Msit115BestOne.Areas.Admin.Service;

namespace Msit115BestOne.Areas.Admin.Models
{
    public class AdminGR<T> : IAdminGR<T> where T : class
    {
        private ManPowerEntities db = null;
        private DbSet<T> Dbset = null;
        LogService log = new LogService();

        public AdminGR()
        {
            db = new ManPowerEntities();
            Dbset = db.Set<T>();
        }

        public void Create(T _entity)
        {
            Dbset.Add(_entity);
            db.SaveChanges();
        }

        public void Create(T _entity, string AdminName, string SaveData, string Orignal_Page)
        {
            Dbset.Add(_entity);
            db.SaveChanges();
            string Data_Action = "新增";
            log.LogInfo(AdminName, SaveData, Data_Action, Orignal_Page);
        }

        public void LogCreate(T _entity)
        {
            Dbset.Add(_entity);
            db.SaveChanges();
        }

        public void Delete(T _entity)
        {
            Dbset.Remove(_entity);
            db.SaveChanges();
        }

        public void Delete(T _entity, string AdminName, string SaveData, string Orignal_Page)
        {
            Dbset.Remove(_entity);
            db.SaveChanges();
            string Data_Action = "刪除";
            log.LogInfo(AdminName, SaveData, Data_Action, Orignal_Page);
        }

        public IEnumerable<T> GetAll()
        {
            return Dbset; //ToList
        }

        public T GetById(int id)
        {
            return Dbset.Find(id);
        }

        public T GetById(string id)
        {
            return Dbset.Find(id);
        }

        public void Update(T _entity)
        {
            db.Entry(_entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Update(T _entity, string AdminName, string SaveData, string Orignal_Page)
        {
            db.Entry(_entity).State = EntityState.Modified;
            db.SaveChanges();
            string Data_Action = "更新成功";
            log.LogInfo(AdminName, SaveData, Data_Action, Orignal_Page);
        }

    }
}