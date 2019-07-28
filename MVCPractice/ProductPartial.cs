using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
namespace MVCPractice
 
{
    public partial class Product
    {
        public class CustomProductValidationsAttribute : ValidationAttribute,IClientValidatable
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

            public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
            {
                var rule = new ModelClientValidationRule();
               
                rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());
                rule.ValidationParameters.Add("inputstring", _inputstring);
                rule.ValidationType = "custom";
                yield return rule;
            }

            private readonly string _inputstring;
        }
        public class UniqueAttribute : ValidationAttribute
        {
            public UniqueAttribute(string PropertyName,string FieldName)
            {
                _propertyname = PropertyName;
                _fieldname = FieldName;
               
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationcontext)
            {
                var context = new AdventureWorksEntities();
                var source = context.Products;
                var idName = _propertyname;
                var idValue = value;

                object instance = validationcontext.ObjectInstance;
                Type type = instance.GetType();
               PropertyInfo property = type.GetProperty(_fieldname);
                object propertyValue = property.GetValue(instance);

                var param = Expression.Parameter(typeof(Product));
                var condition =
                    Expression.Lambda<Func<Product, bool>>(
                        Expression.Equal(
                            Expression.Property(param, idName),
                            Expression.Constant(idValue, typeof(string))
                        ),
                        param
                    ); // for LINQ to SQl/Entities skip Compile() call



                bool unique = context.Products.Any(condition) && !context.Products.Any(a => a.ProductID == (int)propertyValue);
                if (unique)
                {
                    var errormessage = FormatErrorMessage(validationcontext.DisplayName);
                    return new ValidationResult(errormessage);
                }
                return ValidationResult.Success;
            }
          
            private readonly string _propertyname;
            private readonly string _fieldname;
        }
    }
}