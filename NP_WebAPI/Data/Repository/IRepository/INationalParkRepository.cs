using NP_WebAPI.Models;

namespace NP_WebAPI.Data.Repository.IRepository
{
    public interface INationalParkRepository
    {

        ICollection<NationalPark> GetNationalParks();//Display
        NationalPark GetNationalPark(int nationalParkId);
        bool NationalParkExists(int nationalParkId);
        bool NationalParkExists(string nationalParkName);
        bool CreateNationalPark(NationalPark nationalPark);
        bool UpdateNationalPark(NationalPark nationalPark);
        bool DeleteNationalPark(NationalPark nationalPark);
        bool Save();

    }
}
