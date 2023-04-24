using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.API
{
    public interface IRepository<TEntity>
    {
        Task<HttpResponseMessage> Get(string token, string path,string query="");
        Task<HttpResponseMessage> Post(TEntity request,string path, string token);
        Task<HttpResponseMessage> Delete(string path, string token);
        Task<HttpResponseMessage> Put(TEntity data, string path, string token);
        Task<HttpResponseMessage> Send(HttpRequestMessage data);
    }
}
