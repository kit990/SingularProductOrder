using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    /*
     This will be a Generic class which will enable me to abide by the DRY principle
     Whenever we pass in an object it will inherit from Base Entity class. So whenever
     we reference the Id, the generic class knows what that is.

         */
    public class InMemoryRepository<T> where T : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;

        //Constructor
        public InMemoryRepository()
        {
            //reflection to get the actual name of the clss
            className = typeof(T).Name;
            items = cache[className] as List<T>;

            if (items == null)
            {
                items = new List<T>();
            }
        }
        public void Commit()
        {
            cache[className] = items;
        }

        public void Insert(T t)
        {
            items.Add(t);
        }

        public void Update(T t)
        {
            T tToUpdate = items.Find(c => c.Id == t.Id);

            if (tToUpdate != null)
            {
                tToUpdate = t;
            }
            else
            {
                throw new Exception(className + " Not found");
            }
        }

        public T Find(string Id)
        {
            T tToFind = items.Find(p => p.Id == Id);

            if (tToFind != null)
            {
                return tToFind;
            }
            else
            {
                throw new Exception(className + " Not found");
            }
        }

        //Return a list of products that can be queried
        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        public void Delete(string Id)
        {
            T tToDelete = items.Find(c => c.Id == Id);

            if (tToDelete != null)
            {
                items.Remove(tToDelete);
            }
            else
            {
                throw new Exception(className + " Not found");
            }
        }

    }
}
