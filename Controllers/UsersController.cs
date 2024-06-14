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
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUser _uRepo;
        private readonly IMapper _mapper;

        public UsersController(IUser uRepo, IMapper mapper)
        {
            _uRepo = uRepo;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public IActionResult GetUser(int id)
        {
            User user = _uRepo.GetUser(id);

            UserDTO userDto = _mapper.Map<UserDTO>(user);

            try
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = userDto });
            
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message });

            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateUser([FromBody] RegisterDTO user)
        {
            try
            {
                var res = _uRepo.CreateUser(user);
                return StatusCode(StatusCodes.Status201Created, new { mensaje = "ok", response = res});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message });

            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateUser(int id, [FromBody] UserDTO userdto)
        {
            try
            {
                var res = _uRepo.UpdateUser(id, userdto);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" , response = res});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message });

            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var res = _uRepo.DeleteUser(id);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = res });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = ex.Message });

            }
        }

    }
}
