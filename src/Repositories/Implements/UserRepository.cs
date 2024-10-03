using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catedra1.src.Data;
using Catedra1.src.DTO;
using Catedra1.src.Models;
using Catedra1.src.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catedra1.src.Repositories.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if(user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync(); 
                return true; 
            }

            return true;
        }

        public async Task<bool> EditUser(int id, EditUserDto user)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if(existingUser == null){
                return false;
            }

            existingUser.Rut = user.Rut ?? existingUser.Rut;
            existingUser.Name = user.Name ?? existingUser.Name;
            existingUser.Email = user.Email ?? existingUser.Email;
            existingUser.GenderId = user.GenderId; 
            existingUser.Birthday = user.Birthday;

            _context.Entry(existingUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var users = await _context.Users.Include(u => u.Gender)
                                            .ToListAsync();
            return users;
        }

        public async Task<User?> GetUserById(int id)
        {
            var user = await _context.Users.Where(u => u.Id == id)
                                            .Include(u => u.Gender)
                                            .FirstOrDefaultAsync();
            return user;
        }

        public async Task<User?> GetUserByRut(string rut)
        {
            var user = await _context.Users.Where(u => u.Rut == rut)
                                            .Include(u => u.Gender)
                                            .FirstOrDefaultAsync();
            return user;
        }

        public async Task<bool> VerifyEmail(string email)
        {
            var user = await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            if(user == null){
                return false;
            }
            return true;
        }

        public async Task<bool> VerifyRut(string rut)
        {
            var user = await _context.Users.Where(u => u.Rut == rut).FirstOrDefaultAsync();
            if(user == null){
                return false;
            }
            return true;
        }

        public async Task<bool> VerifyUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if(user == null){
                return false;
            }
            return true;
        }
    }
}