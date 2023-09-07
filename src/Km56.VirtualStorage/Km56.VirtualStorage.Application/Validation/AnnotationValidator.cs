using System.ComponentModel.DataAnnotations;

namespace km56.VirtualStorage.Application.Validation
{
    internal class AnnotationValidator<T> 
        where T : class
    {
        public List<ValidationResult> ValidationResults { get; set; }

        public string ErrorMessages 
        { 
            get 
            {
                string errorMessages = string.Empty;
                if (ValidationResults != null) 
                { 
                    foreach (ValidationResult validationResult in ValidationResults) 
                    {
                        errorMessages = errorMessages + validationResult.ErrorMessage + "; "; 
                    }
                }

                errorMessages.TrimEnd().TrimEnd(';');

                return errorMessages;
            } 
        }

        public AnnotationValidator()
        { 
            ValidationResults = new List<ValidationResult>();
        }

        public bool IsValid(T entity)
        { 
            bool isValid = false;
            ValidationResults = new List<ValidationResult>();

            if (entity == null) 
            {
                ValidationResults.Add(new ValidationResult("NULL object"));
            }
            else 
            {
                var context = new ValidationContext(entity);
                isValid = Validator.TryValidateObject(entity, context, ValidationResults, true);
            }

            return isValid;
        }
    }
}
