using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcCoreProject_Iqbal.CustomValidation
{
    public class PersonalValidation: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string message = value.ToString();
                if (message.Contains("R-"))
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("Must be use R- ");
        }
    }
}