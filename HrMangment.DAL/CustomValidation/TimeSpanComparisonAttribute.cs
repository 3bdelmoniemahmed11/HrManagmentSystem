using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.DAL.CustomValidation
{
    public class TimeSpanComparisonAttribute: ValidationAttribute
    {
        private readonly string otherProperty;

        public TimeSpanComparisonAttribute(string otherProperty)
        {
            this.otherProperty = otherProperty;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(otherProperty);

            if (property == null)
            {
                return new ValidationResult($"Unknown property: {otherProperty}");
            }

            var otherValue = property.GetValue(validationContext.ObjectInstance);

            if (value is TimeSpan timeSpan1 && otherValue is TimeSpan timeSpan2)
            {
                if (timeSpan1.TotalHours > timeSpan2.TotalHours)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(ErrorMessage ?? $"The  {validationContext.DisplayName} must be greater than {otherProperty}.");
        }
    }
}
