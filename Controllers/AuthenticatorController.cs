using AutoMapper;
using MealMasterAPI.Excepcions;
using MealMasterAPI.Models;
using MealMasterAPI.Models.Dtos;
using MealMasterAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MealMasterAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticatorController : Controller
    {
        private readonly IUser _uRepo;

        public AuthenticatorController(IUser uRepo)
        {
            _uRepo = uRepo;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult LoginUser([FromBody] LoginDTO login)
        {
            try
            {
                UserTokenDTO tk = _uRepo.LoginUser(login);
                return StatusCode(StatusCodes.Status202Accepted, new { mensaje = "ok", response = tk });
            }
            catch (LoginException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "Hubo un error, intentelo de nuevo", response = ex.Message });

            }
        }

        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword([FromBody] ForgotPassDTO req)
        {
            try
            {
                var token = _uRepo.RequestPasswordResetToken(req);
                var resetLink = Url.Action("ResetPassword", "Authenticator", new { token = token.Token, email = req.email }, Request.Scheme);
                
                if (resetLink != null)
                {
                    _uRepo.SendPasswordResetEmail(req.email, resetLink);
                }
                else
                {
                    throw new EmailNotSendException();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = "The email sends in your bandage" });
            }
            catch (EmailNotSendException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "Hubo un error, intentelo de nuevo", response = ex.Message });

            }
        }

        [HttpGet("ResetPassword")]
        public IActionResult ResetPassword(string token, string email)
        {
            try
            {
                var model = new ResetPassDTO { token = token, email = email };
                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });

            }

        }

        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword([FromBody] ResetPassDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = _uRepo.ResetPassword(model.email, model.token, model.confirmpassword);
                if (!result)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { response = "Hubo un error, intentelo de nuevo" });
                }

                return StatusCode(StatusCodes.Status200OK, new { response = "La contraseña se actualizo correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });

            }
        }


    }
}
