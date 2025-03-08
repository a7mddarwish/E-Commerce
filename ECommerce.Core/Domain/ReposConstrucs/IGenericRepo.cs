using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Domain.ReposConstrucs
{
    public interface IGenericRepo<T> where T : class 
    {

       // دي العمليات اللي هتتعامل مع الداتا بيز بشكل مباشر عشان كدا عملناها async 
       public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);



       public void Add(T entity);
       public void Update(T entity);
       public void Delete(T entity);
       public Task<int> SaveChangesAsync();

       ////public void AddRange(IEnumerable<T> entities);
       ////public void DeleteRange(IEnumerable<T> entities);

        


    }
}
