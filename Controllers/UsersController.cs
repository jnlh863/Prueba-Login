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

        [HttpGet("{id:guid}")]
        public IActionResult GetUser(Guid id)
        {
            try
            {
                User user = _uRepo.GetUser(id);

                UserDTO userDto = _mapper.Map<UserDTO>(user);

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

        [HttpPut("{id:guid}")]
        public IActionResult UpdateUser(Guid id, [FromBody] UserDTO userdto)
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
