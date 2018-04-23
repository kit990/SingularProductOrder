using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;

namespace MyShop.DataAccess.SQL
{
    public class SQLRepository<T> where T : BaseEntity
    {

        internal DataContext context;
        internal DbSet<T> dbSet;

        //Constructor
        public SQLRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }
        public void Commit()
        {
            context.SaveChanges();
        }

        public void Insert(T t)
        {
            dbSet.Add(t);
        }

        public void Update(T t)
        {
            dbSet.Attach(t);
            context.Entry(t).State = EntityState.Modified;
        }

        public T Find(string Id)
        {
            return dbSet.Find(Id);
        }

        //Return a list of products that can be queried
        public IQueryable<T> Collection()
        {
            return dbSet;
        }

        public void Delete(string Id)
        {
            var t = Find(Id);
            if (context.Entry(t).State == EntityState.Detached)
            {
                dbSet.Attach(t);

                dbSet.Remove(t);
            }
        }
    }
}
