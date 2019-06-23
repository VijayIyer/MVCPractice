//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCPractice
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using System.Linq.Expressions;
    using System.Reflection;
    using MVCPractice.Validations;
    
    public partial class Product : IValidatableObject
    {
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.SalesOrderDetails = new HashSet<SalesOrderDetail>();
            this.ModifiedDate = System.DateTime.Now;
            this.SellStartDate = System.DateTime.Now;
            this.rowguid = Guid.NewGuid();
        }

        public int ProductID { get; set; }
        [StringLength(20)]
        [Required(ErrorMessage = "Please do not leave Product Name Empty")]
        [CustomProductValidations("*",ErrorMessage = "Custom check for special characters showed an unwanted character")]
        [RegularExpression("^[^@!#()&]*$", ErrorMessage = "Entered Product Name not in correct format")]
        [Display(Name ="Product Name")]
        [Unique("Name","ProductID",ErrorMessage ="The Product Name should be unique")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Please do not leave Product Number Empty")]
        [RegularExpression("^[a-z|A-Z]{2}-[a-zA-Z]\\w{3}-\\d{2}", ErrorMessage ="Entered Product Number not in correct format")]
        [Unique("ProductNumber","ProductID",ErrorMessage ="Product Number should be unique")]
        public string ProductNumber { get; set; }
        [DisplayFormat(NullDisplayText = "Multi")]
        public string Color { get; set; }
        public decimal StandardCost { get; set; }
        public decimal ListPrice { get; set; }
        public string Size { get; set; }
        [Display(Name="Product Weight")]
        
        [Range(1,1000)]
        public Nullable<decimal> Weight { get; set; }
        [Display(Name="Product Category")]
        public Nullable<int> ProductCategoryID { get; set; }
        public Nullable<int> ProductModelID { get; set; }
       
        public System.DateTime SellStartDate { get; set; }
        public Nullable<System.DateTime> SellEndDate { get; set; }
        public Nullable<System.DateTime> DiscontinuedDate { get; set; }
        public byte[] ThumbNailPhoto { get; set; }
        [RegularExpression(".*.(jpg|png|gif|bmp)$", ErrorMessage ="File is not of correct format")]
        public string ThumbnailPhotoFileName { get; set; }
        public System.Guid rowguid { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ProductModel ProductModel { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            if (ListPrice > StandardCost)
            {
                yield return new ValidationResult("List Price cannot be same as Standard Cost.");
            }
        }
    }
}
