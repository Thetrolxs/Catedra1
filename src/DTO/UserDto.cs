using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catedra1.src.Models;

namespace Catedra1.src.DTO
{
    public class UserDto
    {
        public int Id {get; set;}
        public string Rut {get; set;} = string.Empty;
        public string Name {get; set;} = string.Empty;
        public string Email {get; set;} = string.Empty;
        public DateTime Birthday {get; set;}
        //Relations
        public int GenderId {get; set;} 
        public Gender Gender {get; set;} = null!;
        
    }
}