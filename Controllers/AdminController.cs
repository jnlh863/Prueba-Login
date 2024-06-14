using AutoMapper;
using MealMasterAPI.Models;
using MealMasterAPI.Models.Dtos;
using MealMasterAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MealMasterAPI.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    [EnableCors("ReglasCORS")]
    [ApiController]
    [Route("api/admin")]
    public class AdminController : Controller
    {
        private readonly IUser _uRepo;
        private readonly IMapper _mapper;

        public AdminController(IUser uRepo, IMapper mapper)
        {
            _uRepo = uRepo;
            _mapper = mapper;
        }


        [HttpPost("{id}/role")]
        public IActionResult AssignRole(int id, [FromBody] string role)
        {
            try
            {
                var res = _uRepo.UpdateRol(id, role);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = res });
            }
            catch (Exception res)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = res });

            }
        }

        [HttpGet("users")]
        public IActionResult GetUsers(int id)
        {
            try
            {
                var listUsers = _uRepo.GetUsers();

                return Ok(listUsers);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message });

            }
        }


    }
}
