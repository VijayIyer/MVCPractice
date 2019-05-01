using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVCPractice.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class CustomProductValidationsAttribute:ValidationAttribute
    {
        public CustomProductValidationsAttribute(char inputstring):base("{0} cannot be in input string")
        {
            _inputstring = inputstring;
        }
        protected override ValidationResult IsValid(object value,ValidationContext validationcontext)
        {
            if (value.ToString().Contains(_inputstring))
            {
                var errormessage = FormatErrorMessage(validationcontext.DisplayName);
                return new ValidationResult(errormessage);
            }
            return ValidationResult.Success;
        }
        private readonly char _inputstring;
    }
}