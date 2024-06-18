using Microsoft.EntityFrameworkCore;
using NP_WebAPI.Data.Repository.IRepository;
using NP_WebAPI.Models;

namespace NP_WebAPI.Data.Repository
{
    public class TrailRepository : ITrailRepository
    {
        private readonly ApplicationDbContext _context;
        public TrailRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CreateTrail(Trail trail)
        {
            _context.Trails.Add(trail);
            return Save();
        }

        public bool DeleteTrail(Trail trail)
        {
            _context.Trails.Remove(trail);
            return Save();
        }

        public Trail GetTrail(int trailId)
        {
            return _context.Trails.Find(trailId);
        }

        public ICollection<Trail> GetTrails()
        {
            return _context.Trails.Include(t => t.NationalPark).ToList();
        }

        public ICollection<Trail> GetTrailsNationalPark(int nationalParkId)
        {
            return _context.Trails.Include(t => t.NationalPark).Where(t => t.NationalParkId == nationalParkId).ToList();
        }

        public bool Save()
        {
            return _context.SaveChanges() == 1 ? true:false;
        }

        public bool TrailExists(int trailId)
        {
            return _context.Trails.Any(t => t.Id == trailId);
        }

        public bool TrailExists(string trailName)
        {
            return _context.Trails.Any(t => t.Name == trailName);
        }

        public bool UpdateTrail(Trail trail)
        {
            _context.Trails.Update(trail);
            return Save();
        }
    }
}
