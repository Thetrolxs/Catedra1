using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catedra1.src.Helpers
{
    public class GenderValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var valueString = value?.ToString();

            if (valueString != null){
                if(!int.TryParse(valueString, out int GenderId))
                {
                    return new ValidationResult("El Género no es válido.");
                }
            }  

            return ValidationResult.Success;
        }        
    }
}