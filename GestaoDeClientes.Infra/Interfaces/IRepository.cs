using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeClientes.Infra.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string id);
        Task<T> GetByIdAsync(string id);
        Task<T> GetByNomeAsync(string nome);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
