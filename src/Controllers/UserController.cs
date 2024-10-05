using AutoMapper;
using Catedra1.src.DTO;
using Catedra1.src.Helpers;
using Catedra1.src.Models;
using Catedra1.src.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Catedra1.src.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase 
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IGenderRepository _genderRepository;

        public UserController(IUserRepository repository, IMapper mapper, IGenderRepository genderRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _genderRepository = genderRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetUsers()
        {
            var result = _repository.GetAllUsers().Result;
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] EditUserDto register)
        {
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            try{
                var mappedUser = _mapper.Map<User>(register);
                if(!_genderRepository.ValidatedGenderId(mappedUser.GenderId).Result){
                    throw new Exception("El genero no es valido.");
                }
                if(!_repository.VerifyRut(mappedUser.Rut).Result){
                    return Conflict("El RUT ya existe.");
                }
                if(!_repository.VerifyEmail(mappedUser.Email).Result){
                    throw new Exception("El email ingresado ya existe.");
                }

                await _repository.AddUser(mappedUser);

                return Ok("Usuario creado exitosamente");
            } catch {
                return BadRequest("Alguna validación no fue cumplida");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] EditUserDto editUser)
        {

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var valor = await _repository.EditUser(id, editUser);
            if(!valor)
            {
                return NotFound("Usuario no encontrado");
            }

            return Ok("Datos editados con exito.");

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _repository.GetUserById(id);
            if(user == null)
            {
                return NotFound("Usuario no encontrado.");
            }
            
            _repository.Delete(id);
            return Ok("Usuario eliminado con exito");

        }
    }
}
