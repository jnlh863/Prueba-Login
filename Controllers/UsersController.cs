using AutoMapper;
using MealMasterAPI.Excepcions;
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

        [HttpGet("{id:guid}")]
        public IActionResult GetUser(Guid id)
        {
            try
            {
                User user = _uRepo.GetUser(id);

                UserDto userDto = _mapper.Map<UserDto>(user);

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = userDto });
            
            }catch(UserNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "Hubo un error, intentelo de nuevo", response = ex.Message });

            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateUser([FromBody] RegisterDto user)
        {
            try
            {
                var res = _uRepo.CreateUser(user);
                return StatusCode(StatusCodes.Status201Created, new { mensaje = "ok", response = res});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "Hubo un error, intentelo de nuevo", response = ex.Message });

            }
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateUser(Guid id, [FromBody] UserDto userdto)
        {
            try
            {
                var res = _uRepo.UpdateUser(id, userdto);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" , response = res});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "Hubo un error, intentelo de nuevo", response = ex.Message });

            }
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteUser(Guid id)
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
