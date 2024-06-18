using Microsoft.AspNetCore.Mvc;

using NP_WebApp.Models;
using NP_WebApp.Models.ViewModels;
using NP_WebApp.Repository.IRepository;
using System.Diagnostics;

namespace NP_WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INationalParkRepository _nationalParkRepository;
        private readonly ITrailRepository _trailRepository;
        public HomeController(ILogger<HomeController> logger,ITrailRepository trailRepository,INationalParkRepository nationalParkRepository)
        {
            _logger = logger;
            _trailRepository = trailRepository;
            _nationalParkRepository = nationalParkRepository;
        }

        public async  Task<IActionResult> Index()
        {
            IndexVM indexVM = new IndexVM()
            {
                NationalParkList = await _nationalParkRepository.GetAllAsync(SD.NationalParkAPIPath),
                TrailList = await _trailRepository.GetAllAsync(SD.TrailAPIPath)
            };
            return View(indexVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}