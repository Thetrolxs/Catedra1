using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catedra1.src.Models;

namespace Catedra1.src.Repositories.Interfaces
{
    public interface IGenderRepository
    {
        Task<IEnumerable<Gender>> GetGenders();
        Task<bool> ValidatedGenderId(int id);
    }
}