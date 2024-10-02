using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catedra1.src.Helpers
{
    public class DateValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var valueString = value?.ToString();

            if (valueString != null){
                if(!DateTime.TryParse(valueString, out DateTime date))
                {
                    return new ValidationResult("El formato de la Fecha de Nacimiento no es valido.");
                }

                
                if(date > DateTime.Today)
                {
                    return new ValidationResult("La Fecha de Nacimiento debe ser menor que la fecha actual.");
                }   
                
            }

            return ValidationResult.Success;
        }    
    }
}