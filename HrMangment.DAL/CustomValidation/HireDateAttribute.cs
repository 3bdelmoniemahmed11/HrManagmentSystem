using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrMangment.DAL.CustomValidation
{
    internal class HireDateAttribute : ValidationAttribute
    {
        private readonly DateTime companyStartDate;
        public HireDateAttribute(string  _companyStartDate)
        {

            companyStartDate =DateTime.Parse(_companyStartDate);
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (value is DateTime hireDate)
                {

                    if (hireDate >= companyStartDate)
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        return new ValidationResult("Hire date cant be before company start date");
                    }

                }
            }
            return new ValidationResult("Error! Contact the Admin");
        }
    }
}
