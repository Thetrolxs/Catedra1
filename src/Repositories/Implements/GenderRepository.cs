using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catedra1.src.Data;
using Catedra1.src.Models;
using Catedra1.src.Repositories.Interfaces;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace Catedra1.src.Repositories.Implements
{
    public class GenderRepository : IGenderRepository
    {
        private readonly DataContext _context;
        public GenderRepository(DataContext context){
            _context = context;
        }
        public async Task<IEnumerable<Gender>> GetGenders()
        {
            var genders = await _context.Genders.ToListAsync();
            return genders;
        }

        public async Task<bool> ValidatedGenderId(int id)
        {
            var existingGender = await _context.Genders.FindAsync(id);
            if(existingGender == null){
                return false;
            }

            return true;
        }
    }
}