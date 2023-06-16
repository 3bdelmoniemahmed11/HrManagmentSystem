using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.DAL.CustomValidation
{
    public class DateComparsionAttribute:ValidationAttribute
    {

        private readonly string otherProperty;

        public DateComparsionAttribute(string otherProperty)
        {
            this.otherProperty = otherProperty;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(otherProperty);

            if (property == null)
            {
                return new ValidationResult($"Unknown property: {otherProperty}");
            }

            var otherValue = property.GetValue(validationContext.ObjectInstance);
            if(value is DateTime dateOne && otherValue is DateTime dateTwo  )
            {
                if(dateOne>=dateTwo)
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult(ErrorMessage ?? $"The  {validationContext.DisplayName} must be greater than {otherProperty}.");

          
        }
    }
}
