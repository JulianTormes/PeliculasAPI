using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Validations
{
    public class ArchiveTypeValidation: ValidationAttribute
    {
        private readonly string[] validTypes;

        public ArchiveTypeValidation(String[]ValidTypes)
        {
            validTypes = ValidTypes;
        }
        public ArchiveTypeValidation(GroupTypeArchive groupTypeArchive)
        {
            if (groupTypeArchive == GroupTypeArchive.Image)
            {
                validTypes = new string[] { "image/jpeg", "image/png", "image/gif" };
            }
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
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

            if (!validTypes.Contains(formFile.ContentType))
            {
                return new ValidationResult($"el tipo del archivo debe ser uno de los siguientes :{string.Join(",", validTypes)}");
            }

            return ValidationResult.Success;
        }
    }
}
