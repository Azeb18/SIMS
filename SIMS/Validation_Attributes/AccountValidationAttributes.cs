using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SIMS.Validators
{
    public class Sex : ValidationAttribute
    {
        public Sex() : base("{0} must be Male or Female.")
        { }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            char sex = (char)value;
            if(sex=='M' || sex=='m' || sex=='f' ||sex=='F')
            {
                return ValidationResult.Success;
            }
            var errorMessage = FormatErrorMessage(validationContext.DisplayName);
            return new ValidationResult(errorMessage);
        }
    }

    public class Name : ValidationAttribute
    {
        public Name()
            : base("{0} must contain only alphabetic characters.")
        { }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && Regex.IsMatch(value.ToString(), @"^[A-Za-z\s]+$"))
            {
                return ValidationResult.Success;
            }
            var errorMessage = FormatErrorMessage(validationContext.DisplayName);
            return new ValidationResult(errorMessage);
        }
    }

}