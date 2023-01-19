using System;
using System.ComponentModel.DataAnnotations;

namespace LightNote.Api.CustomValidators
{
	public class NonEmptyStringCollectionAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var list = value as IEnumerable<string>;
            if (list == null)
            {
                return new ValidationResult("The value is not a list of strings.");
            }
            if (list.Any(string.IsNullOrWhiteSpace))
            {
                return new ValidationResult("The list contains empty strings.");
            }
            return ValidationResult.Success;
        }
    }
}

