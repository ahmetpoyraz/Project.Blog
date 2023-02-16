using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data
{
    public interface IGenericRepository<T>
    {
        public Task<T> GetAsync(int id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<int> InsertAsync(T entity);
        public Task<bool> UpdateAsync(T entity);
        public Task<bool> DeleteAsync(int id, int userId);
    }
}
