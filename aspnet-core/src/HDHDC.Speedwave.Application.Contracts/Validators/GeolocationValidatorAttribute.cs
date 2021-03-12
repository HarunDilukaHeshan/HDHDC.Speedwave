using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HDHDC.Speedy.Validators
{
    public class GeolocationValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            string geolocation = value.ToString();
            var latLonArr = geolocation.Split(':');
            var lat = latLonArr[0];
            var lon = latLonArr[1];

            if (!(lat.Substring(lat.IndexOf('.')).Length >= 5 && Convert.ToDouble(lat) > -90 && Convert.ToDouble(lat) < 90))
                return new ValidationResult("Invalid geolocation format");

            if (!(lon.Substring(lon.IndexOf('.')).Length >= 5 && Convert.ToDouble(lon) > -180 && Convert.ToDouble(lon) < 180))
                return new ValidationResult("Invalid geolocation format");

            return ValidationResult.Success;
        }
    }
}
