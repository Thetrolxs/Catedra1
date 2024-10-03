using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catedra1.src.DTO;
using Catedra1.src.Models;

namespace Catedra1.src.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<bool> AddUser(User user);
        Task<User?> GetUserByRut(string rut);
        Task<User?> GetUserById(int id);
        Task<bool> VerifyRut(string rut);
        Task<bool> VerifyEmail(string email);
        Task<bool> VerifyUser(int id);
        Task<bool> EditUser(int id, EditUserDto user);


    }
}