using AutoMapper;
using MealMasterAPI.Models;
using MealMasterAPI.Models.Dtos;
using MealMasterAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace MealMasterAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticatorController : ControllerBase
    {
        private readonly IUser _uRepo;

        public AuthenticatorController(IUser uRepo)
        {
            _uRepo = uRepo;
        }


        [HttpPost("login")]
        public IActionResult LoginUser([FromBody] LoginDTO login)
        {
            UserTokenDTO tk = _uRepo.LoginUser(login);

            try
            {
                return StatusCode(StatusCodes.Status202Accepted, new { mensaje = "ok", response = tk });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message });

            }
        }
    }
}
