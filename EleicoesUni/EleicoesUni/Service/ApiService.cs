using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace EleicoesUni.Service
{
    public class ApiService<T>
    {
        HttpClient client;
        IEnumerable<T> objects;

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public ApiService()
        {
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (message, certificate, chain, sslPolicyErrors) => true;
            client = new HttpClient();
            client.BaseAddress = new Uri("https://eleicoesuni.000webhostapp.com/api/");

            objects = new List<T>();
        }

        public async Task<IEnumerable<T>> GetObjectsAsync(bool forceRefresh = false)
        {
            try
            {
                if (forceRefresh && IsConnected)
                {
                    var json = await client.GetStringAsync($"{typeof(T).Name.ToLower()}s/listar");
                    var apiJson = await Task.Run(() => JsonConvert.DeserializeObject<ApiResponse<T>>(json));
                    if (apiJson.Tipo == "sucesso")
                    {
                        objects = apiJson.Resposta;
                    }
                }
                return objects;
            } catch
            {
                return objects;
            }
            
        }

        public async Task<T> GetObjectAsync(int id)
        {
            if (id > 0 && IsConnected)
            {
                var json = await client.GetStringAsync($"{typeof(T).Name.ToLower()}s/listar/{id}");
                var apiJson = JsonConvert.DeserializeObject<ApiResponseUnic<T>>(json);
                if (apiJson.Tipo == "sucesso" && apiJson.Resposta != null)
                    return apiJson.Resposta;
            }
            return default(T);
        }

        public async Task<bool> AddObjectAsync(T obj)
        {
            if (obj == null || !IsConnected)
                return false;

            var serializedObject = JsonConvert.SerializeObject(obj);

            var response = await client.PostAsync($"{typeof(T).Name.ToLower()}s/cadastrar", new StringContent(serializedObject, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateObjectAsync(T obj, int id)
        {
            if (obj == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(obj);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"{typeof(T).Name.ToLower()}s/atualizar/{id}"), byteContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteObjectAsync(int id)
        {
            if (id > 0 && !IsConnected)
                return false;

            var response = await client.DeleteAsync($"{typeof(T).Name.ToLower()}s/deletar/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
