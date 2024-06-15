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
            try
            {
                UserTokenDTO tk = _uRepo.LoginUser(login);
                return StatusCode(StatusCodes.Status202Accepted, new { mensaje = "ok", response = tk });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message });

            }
        }

        [HttpPost("request-password-reset")]
        public IActionResult RequestPasswordReset([FromBody] ResetPassDTO reset)
        {
            UserTokenDTO token = _uRepo.RequestPasswordReset(reset);
            var resetLink = Url.Action("ResetPassword", "Account", new { token = token }, Request.Scheme);



        }









    }
}
