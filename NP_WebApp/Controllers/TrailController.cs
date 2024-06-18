using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using NP_WebAPP.Models.ViewModels;
using NP_WebApp.Models;
using NP_WebApp.Repository.IRepository;


namespace NP_WebApp.Controllers
{
    public class TrailController : Controller
    {
        private readonly ITrailRepository _trailRepository;
        private readonly INationalParkRepository _nationalParkRepository;
        public TrailController(ITrailRepository trailRepository,INationalParkRepository nationalParkRepository)
        {
            _trailRepository = trailRepository;
            _nationalParkRepository = nationalParkRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region APIs
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _trailRepository.GetAllAsync(SD.TrailAPIPath) });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _trailRepository.DeleteAsync(SD.TrailAPIPath, id);
            if (status)
                return Json(new { success = true, message = "data successfully deleted"});
            return Json(new { success = false, message = "something went wrong" });
        }
        #endregion
        public async Task<IActionResult> Upsert(int? id)
        {
            var nationalParks = await _nationalParkRepository.GetAllAsync(SD.NationalParkAPIPath);
            TrailVM trailVM = new TrailVM()
            {
                Trail=new Trail(),
                nationalParkList=nationalParks.Select(np=>new SelectListItem()
                {
                    Text=np.Name,
                    Value=np.Id.ToString()
                })
            };
            if (id == null) return View(trailVM);
            trailVM.Trail = await _trailRepository.GetAsync(SD.TrailAPIPath,id.GetValueOrDefault());
            if (trailVM.Trail == null) return NotFound();
            return View(trailVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(TrailVM trailVM)
        {
            if (ModelState.IsValid)
            {
                if (trailVM.Trail.Id == 0)
                    await _trailRepository.CreateAsync(SD.TrailAPIPath, trailVM.Trail);
                else
                    await _trailRepository.UpdateAsync(SD.TrailAPIPath, trailVM.Trail);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var nationalParks = await _nationalParkRepository.GetAllAsync(SD.NationalParkAPIPath);
                trailVM = new TrailVM()
                {
                    Trail=new Trail(),
                    nationalParkList=nationalParks.Select(np=>new SelectListItem()
                    {
                        Text=np.Name,
                        Value=np.Id.ToString()
                    })
                };
                return View(trailVM);
            }
        }
    }
}
