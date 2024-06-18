using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NP_WebAPI.Data.Repository.IRepository;
using NP_WebAPI.Models;
using System.Security.Cryptography.X509Certificates;

namespace NP_WebAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody]User user)//data data post/submit hoke aaye then from body
        {
            if (ModelState.IsValid)
            {
                var isUniqueUser = _userRepository.IsUniqueUser(user.UserName);
                if (!isUniqueUser) return BadRequest("User in use!!");
                var userInfo = _userRepository.Register(user.UserName, user.Password);
                user = userInfo;
                if (userInfo == null) return BadRequest();
            }
            return Ok(user);
           
        }
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserVM userVM)
        {
            var user = _userRepository.Authenticate(userVM.UserName, userVM.Password);
            if (user == null) return BadRequest("Wrong user/pwd");
            return Ok(user);
        }

    }
}
