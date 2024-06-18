using AutoMapper;
using MealMasterAPI.Models;
using MealMasterAPI.Models.Dtos;
using MealMasterAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MealMasterAPI.Controllers
{
    [Authorize]
    [EnableCors("ReglasCORS")]
    [ApiController]
    [Route("api/users/profile")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfile _upRepo;

        public ProfileController(IProfile upRepo)
        {
            _upRepo = upRepo;
        }

        [HttpPost("{id:guid}")]
        public IActionResult CreateProfile(Guid id, [FromBody] ProfileDTO profile)
        {
            try
            {
                var res = _upRepo.CreateProfile(id, profile);
                return StatusCode(StatusCodes.Status201Created, new { mensaje = "ok", response = res });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message });

            }
        }


        [HttpGet("{id:guid}")]
        public IActionResult GetProfile(Guid id)
        {
            ProfileDTO profile = _upRepo.GetProfile(id);

            try
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = profile });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message });

            }
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateProfile(Guid id, [FromBody] ProfileDTO profiledto)
        {
            try
            {
                var res = _upRepo.UpdateProfile(id, profiledto);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = res });
            }
            catch (Exception ex)
            {
                if (ex.Message == "User not found.")
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { response = "User not found." });
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

    }
}
