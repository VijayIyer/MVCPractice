using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
namespace MVCPractice
{
    public partial class Product
    {
        public class CustomProductValidationsAttribute : ValidationAttribute
        {
            public CustomProductValidationsAttribute(string inputstring) : base("{0} cannot contain * in input string")
            {
                _inputstring = inputstring;
            }
            protected override ValidationResult IsValid(object value, ValidationContext validationcontext)
            {
                if (value.ToString().Contains(_inputstring))
                {
                    var errormessage = FormatErrorMessage(validationcontext.DisplayName);
                    return new ValidationResult(errormessage);
                }
                return ValidationResult.Success;
            }
            private readonly string _inputstring;
        }
        public class UniqueAttribute : ValidationAttribute
        {
            public UniqueAttribute(string PropertyName)
            {
                _propertyname = PropertyName;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationcontext)
            {
                var context = new AdventureWorksEntities();
                var source = context.Products;
                var idName = _propertyname;
                var idValue = value;

                var param = Expression.Parameter(typeof(Product));
                var condition =
                    Expression.Lambda<Func<Product, bool>>(
                        Expression.Equal(
                            Expression.Property(param, idName),
                            Expression.Constant(idValue, typeof(string))
                        ),
                        param
                    ); // for LINQ to SQl/Entities skip Compile() call



                bool unique = source.Any(condition);
                if (unique)
                {
                    var errormessage = FormatErrorMessage(validationcontext.DisplayName);
                    return new ValidationResult(errormessage);
                }
                return ValidationResult.Success;
            }
            private readonly string _propertyname;
        }
    }
}