using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Catedra1.src.Helpers;

namespace Catedra1.src.DTO
{
    public class EditUserDto
    {
        [RutValidator]
        public string Rut {get; set;} = string.Empty;

        [Required(ErrorMessage ="El Nombre es obligatorio.")]
        [MinLength(3, ErrorMessage = "El nombre debe tener al menos 3 caracteres.")]
        [MaxLength(100, ErrorMessage = "El nombre debe tener a lo más 100 caracteres.")]
        public string Name {get; set;} = string.Empty;

        [Required(ErrorMessage ="El Email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El Email no tiene un formato valido.")]
        public string Email {get; set;} = string.Empty;

        [Required(ErrorMessage ="El Género es obligatorio.")]
        [GenderValidator]
        public string GenderId {get; set;} = string.Empty;

        [Required(ErrorMessage ="La fecha de nacimiento es obligatoria.")]
        [DataType(DataType.Date)]
        
        public string Birthday {get; set;} = string.Empty;
    }
}