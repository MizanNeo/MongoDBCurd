using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NeoSOFT.Infrastructure.Contract
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(string id, T entity);
        Task<bool> DeleteAsync(string id);
        Task<List<T>> FindAsync(Expression<Func<T, bool>> expression);
       
    }
}
