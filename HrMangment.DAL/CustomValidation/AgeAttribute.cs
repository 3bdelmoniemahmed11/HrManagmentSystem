using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.DAL.CustomValidation
{
    public class AgeAttribute : ValidationAttribute
    {
        private readonly int minimumAge;
        public AgeAttribute(int _minmumAge)
        {

            minimumAge = _minmumAge;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (value is DateTime birthDate)
                {

                    DateTime today = DateTime.Today;
                    DateTime minmumDate = today.AddYears(-minimumAge);

                    if (birthDate >= minmumDate)
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        return new ValidationResult($"Employee Age must be greater than {minimumAge}");
                    }

                }

            }
            return new ValidationResult($"Error! Contact the Admin");
        }
       
    }
}
