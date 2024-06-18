using NP_WebAPI.Models;

namespace NP_WebAPI.Data.Repository.IRepository
{
    public interface ITrailRepository
    {
        ICollection<Trail> GetTrailsNationalPark(int nationalParkId);
        ICollection<Trail> GetTrails();
        Trail GetTrail(int trailId);
        bool TrailExists(int trailId);
        bool TrailExists(string trailName);
        bool CreateTrail(Trail trail);
        bool UpdateTrail(Trail trail);
        bool DeleteTrail(Trail trail);
        bool Save();
    }
}
