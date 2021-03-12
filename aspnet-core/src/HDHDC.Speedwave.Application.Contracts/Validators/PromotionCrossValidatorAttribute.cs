using HDHDC.Speedwave.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HDHDC.Speedwave.Validators
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PromotionCrossValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var createDtoType = typeof(PromotionCreateDto);

            if (createDtoType.IsInstanceOfType(value))
            {
                var dto = value as PromotionCreateDto;
                if ((dto.IsOneTime && dto.NoOfTimes == 1) || (!dto.IsOneTime && dto.NoOfTimes > 1))
                {
                    if (DateTime.Compare(DateTime.Now, dto.ExpireDate) < 0)
                        return ValidationResult.Success;
                    else
                        return new ValidationResult("Invalid expire date");
                }
                else
                    return new ValidationResult("There are incompatible values between IsOneTime and NoOfTimes");
            }
            else
                throw new PromotionCrossValidatorException();
        }
    }

    public class PromotionCrossValidatorException : Exception
    {
        public PromotionCrossValidatorException()
            : this("This attribute cannot be used on classes other than PromotionCreateDto and PromotionUpdateDto")
        { }

        public PromotionCrossValidatorException(string message)
            : this(message, null)
        { }
        public PromotionCrossValidatorException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
