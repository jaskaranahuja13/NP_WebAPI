using NP_WebApp.Models;
using NP_WebApp.Repository.IRepository;

namespace NP_WebApp.Repository
{
    public class TrailRepository:Repository<Trail>,ITrailRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public TrailRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClientFactory= httpClientFactory;
        }
    }
}
