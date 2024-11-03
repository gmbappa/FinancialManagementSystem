using API.Models;
using Core.Entities;
using Infrastructure.Helper;
using Infrastructure.Security;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;


namespace API.Controllers.v1
{
   
    [RoutePrefix("api/v{apiVersion}/Authentication")]
    public class AuthenticationController : ApiController
    {
        private readonly JwtTokenService _jwtService;
        private readonly DataAccessHelper _dataAccessHelper;

        public AuthenticationController(JwtTokenService jwtService, DataAccessHelper dataAccessHelper)
        {
            _jwtService = jwtService;
            _dataAccessHelper = dataAccessHelper;
        }

        [HttpPost]
        [Route("Login")]
        public IHttpActionResult Login([FromBody] LoginRequest request)
        {
            var user = IsValidUser(request.Username, request.Password);
            if (user.Any())
            {
                var token = _jwtService.GenerateToken(request.Username,user.FirstOrDefault().Role);
                return Ok(new { token });
            }
            return Unauthorized();
        }
       
        private IEnumerable<User> IsValidUser(string username, string password)
        {
            var parameters = new
            {
                Name = username,
                Password = password                
            };
           return _dataAccessHelper.QueryData<User>("sp_User", parameters).ToList();            
        }

        [HttpPost]
        [Route("ValidateToken")]
        public IHttpActionResult ValidateToken([FromBody] TokenRequest request)
        {
            bool result = false;
            TokenRequest tokenResponse = JsonConvert.DeserializeObject<TokenRequest>(request.Token);
            result = _jwtService.ValidateToken(tokenResponse.Token);
            return Ok(new { result });
        }
    }

   
   
}