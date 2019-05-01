using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation.Attributes;
namespace MVCPractice
{
    [Validator(typeof(CustomerValidator))]
    public partial class Customer
    {
        public string FullName
        {
            get
            {
                return FirstName + " "+MiddleName+" "+ LastName;
            }
            set
            {
                value = FirstName + " " + MiddleName + " " + LastName;
            }
        }
    }

}