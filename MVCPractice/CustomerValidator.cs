using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
namespace MVCPractice
{
    
    public class CustomerValidator:AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.LastName).NotEqual(x => x.FirstName).WithMessage("Last Name should not be equal to first name");
            RuleFor(x => x.EmailAddress).Matches("^\\w+@\\w+.com$").WithName("Email");
        }
    }
}