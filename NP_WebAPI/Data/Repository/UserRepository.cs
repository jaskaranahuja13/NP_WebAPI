using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NP_WebAPI.Data.Repository.IRepository;
using NP_WebAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NP_WebAPI.Data.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly AppSettings _appSettings;
        public UserRepository(ApplicationDbContext context,IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public User Authenticate(string username, string password)
        {
            var userInDb = _context.Users.FirstOrDefault(u => u.UserName == username && u.Password==password) ;
            if (userInDb == null) return null;
            //JWT Token  //Password 123456 username admin
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject= new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,userInDb.Id.ToString()),
                    new Claim(ClaimTypes.Role,userInDb.Role)
                }),
                Expires=DateTime.UtcNow.AddDays(7),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            userInDb.Token = tokenHandler.WriteToken(token);



            //
            userInDb.Password = "";//if correct password empty
            return userInDb;
        }
        public bool IsUniqueUser(string username)
        {
            //is userpassword correct or not
            var userInDb = _context.Users.FirstOrDefault(u => u.UserName == username);
            if (userInDb == null) return true;return false;
        }
        public User Register(string username, string password)
        {
            User user = new User()
            {
                UserName=username,
                Password=password,
                Role="Admin"
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
    }
}
