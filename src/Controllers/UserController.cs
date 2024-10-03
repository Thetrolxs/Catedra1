using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catedra1.src.DTO;
using Catedra1.src.Models;
using Catedra1.src.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Catedra1.src.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetUsers()
        {
            var result = _repository.GetAllUsers().Result;
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] RegisterUserDto register)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isRutExists = await _repository.VerifyRut(register.Rut);
            if(isRutExists)
            {
                return Conflict("El rut ingresado ya se encuentra registrado.");
            }

            var isEmailExists = await _repository.VerifyEmail(register.Email);
            if(isEmailExists)
            {
                return Conflict("El correo ingresado ya se encuentra registrado.");
            }

            var newUser = new User
            {
                Rut = register.Rut,
                Name = register.Name,
                Email = register.Email,
                //arreglar gender
                Birthday = DateTime.Parse(register.Birthday)
            };

            var result = await _repository.AddUser(newUser);

            if(!result)
            {
                return StatusCode(500, "Hubo un error al crear al usuario.");
            }

            return CreatedAtAction(nameof(GetUsers), new {id = newUser.Id}, newUser);
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
