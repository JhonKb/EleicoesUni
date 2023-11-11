using System.Collections.Generic;
using System.Threading.Tasks;

namespace EleicoesUni.Services
{
    public interface ApiServiceData<T>
    {
        Task<IEnumerable<T>> GetObjectAsync(bool forceRefresh = false);
        Task<T> GetObjectAsync(string id);
        Task<bool> AddObjectAsync(T obj);
        Task<bool> UpdateObjectAsync(T obj);
        Task<bool> DeleteObjectAsync(string id);
    }
}
