using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HDHDC.Speedwave.Validators
{
    public class Base64StringValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            try
            {
                Convert.FromBase64String(value.ToString());
            }
            catch(Exception)
            {
                return new ValidationResult("Invalid base64 string");
            }

            return ValidationResult.Success;
        }
    }
}
