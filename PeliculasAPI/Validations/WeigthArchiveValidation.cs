using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Validations
{
    public class WeigthArchiveValidation :ValidationAttribute
    {
        private readonly int maxWeigthInMB;

        public WeigthArchiveValidation(int MaxWeigthInMB )
        {
            maxWeigthInMB = MaxWeigthInMB;
        }

        protected override ValidationResult IsValid(Object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            IFormFile formFile = value as IFormFile;
            if (formFile == null)
            
            {
                return ValidationResult.Success;
            }
            if (formFile.Length > maxWeigthInMB * 1024 * 1024)
            {
                return new ValidationResult($"El peso del archivo no debe ser mayor a {maxWeigthInMB}mb");
            }
            return ValidationResult.Success;
        }
    }
}
