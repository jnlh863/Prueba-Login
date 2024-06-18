using AutoMapper;
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
        private readonly ISendEmail _sendRepo;

        public AuthenticatorController(IUser uRepo, ISendEmail sendRepo)
        {
            _uRepo = uRepo;
            _sendRepo = sendRepo;
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message });

            }
        }

        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword([FromBody] ForgotPassDTO req)
        {
            try
            {
                var token = _uRepo.RequestPasswordResetToken(req);
                var resetLink = Url.Action("ResetPassword", "Authenticator", new { token = token.Token, email = req.email }, Request.Scheme);

                _sendRepo.SendPasswordResetEmail(req.email, resetLink);

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = "The email sends in your bandage" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = "Hubo un error" });

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
                    return BadRequest(new { mensaje = "No se pudo restablecer la contraseña" });
                }
                return RedirectToAction("ResetPasswordConfirmation");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });

            }
        }

        [HttpGet("ResetPasswordConfirmation")]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }


    }
}
