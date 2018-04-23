using System.Linq;
using MyShop.Core.Models;

namespace MyShop.Core.Contracts
{
    //This can now be implemented by all other projects
    public interface IRepository<T> where T : BaseEntity
    {
        //Method Signatures
        IQueryable<T> Collection();
        void Commit();
        void Delete(string Id);
        T Find(string Id);
        void Insert(T t);
        void Update(T t);
    }
}