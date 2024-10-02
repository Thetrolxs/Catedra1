using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Catedra1.src.Helpers
{
    public class RutValidator : ValidationAttribute
    {        
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var rut = value as string;
            if (string.IsNullOrEmpty(rut))
            {
                return new ValidationResult("El Rut es obligatorio.");
            }

            var regex = new Regex(@"^\d{7,8}-[0-9kK]$");
            if (!regex.IsMatch(rut))
            {
                return new ValidationResult("El Rut no tiene un formato válido.");
            }

            if (!IsValidRut(rut))
            {
                return new ValidationResult("El Rut no es válido.");
            }

            return ValidationResult.Success;
        }

        private bool IsValidRut(string rut)
        {
            if (rut == null) return false;

            string[] parts = rut.Split('-');
            if (parts.Length != 2) return false;

            if (!int.TryParse(parts[0], out int rutNumber)) return false;

            char digitoVerificador = parts[1].ToLowerInvariant()[0];

            int[] coefficients = { 2, 3, 4, 5, 6, 7 };
            int sum = 0;
            int index = 0;

            while (rutNumber != 0)
            {
                sum += rutNumber % 10 * coefficients[index];
                rutNumber /= 10;
                index = (index + 1) % 6;
            }

            int result = 11 - (sum % 11);
            char verificador = result == 10 ? 'k' : result.ToString()[0];

            return verificador == digitoVerificador;
        }
    }
        
}
