using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Text;

namespace HDHDC.Speedwave.Validators
{
    public class Base64ImageValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            try
            {
                var b64Str = RemoveHeading(value.ToString());
                var bytes = Convert.FromBase64String(b64Str);
                using var st = new MemoryStream(bytes);
                var img = Image.FromStream(st);
            }
            catch (Exception)
            {
                return new ValidationResult("Invalid base64 image");
            }

            return ValidationResult.Success;
        }

        protected string RemoveHeading(string base64)
        {
            var indx = base64.IndexOf(',');
            var b64 = base64;

            if (indx > -1)
            {
                b64 = base64.Substring(indx + 1);
            }

            return b64;
        }
    }
}
