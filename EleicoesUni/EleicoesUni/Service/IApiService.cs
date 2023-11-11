using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EleicoesUni.Services
{
    public interface IApiService<T>
    {
        Task<IEnumerable<T>> GetObjectsAsync(bool forceRefresh = false);
        Task<T> GetObjectAsync(int id);
        Task<bool> AddObjectAsync(T obj);
        Task<bool> UpdateObjectAsync(T obj, int id);
        Task<bool> DeleteObjectAsync(int id);
    }
}
