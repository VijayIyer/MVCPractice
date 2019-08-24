### An MVC project with EF DB first. The DB was is an Azure SQL Database, created from Sample AdventureWorks Database.<br/><br/>*Features -*
 * [CRUD Operations](https://github.com/VijayIyer/MVCPractice/blob/master/MVCPractice/Controllers/ProductsController.cs). on few of the entities  - Index,Create,Edit views.<br/><br/>
 * In Products Index view, [Pagination](https://github.com/VijayIyer/MVCPractice/blob/master/MVCPractice/Views/Products/Index.cshtml) has been added using PagedListMVC library.Also search bar is added to search for a product entity by name.<br/><br/>
 * [Custom DataAnnotation Validations](https://github.com/VijayIyer/MVCPractice/blob/master/MVCPractice/ProductPartial.cs) have been added to the Products Model  - in ProductPartial.cs file. One of them is to check whether Product Number adheres to a certain format , this is checked using regular expressions. The Other is for remote validation , to check whether Product Name is unique by cross verifying with database. Both are server validations which fire on postback of the page.<br/><br/>
* [4](https://github.com/VijayIyer/MVCPractice/blob/master/MVCPractice/Views/Products/ProductsTable.cshtml).Ajax Request - for paging and searching operations. These are also firing the Index Action method in ProductsController, but the request is Asynchronous with loading indicator in view till request is complete. This is accomplished through Ajax Helper class and jQuery script in Index View in Products.<br/><br/>
 * [5](https://github.com/VijayIyer/MVCPractice/blob/master/MVCPractice/CustomerValidator.cs).Fluent Validation - FluentValidationMVC is a library which makes it easy to create rules for the models. To accomplish this seperate class file is created for Customer.cs - CustomerValidator . Few rules are created on Customer Model using Fluent Validation - checking format of emails , and checking that last name is not equal to first name. Here we see how easy it is to create complex validation rules.<br/><br/>
 * [6](https://github.com/VijayIyer/MVCPractice/blob/master/MVCPractice/CustomHelpers/CustomHelpers.cs).A custom HTML Helper is created - CustomHelper. Here , there is one helper method which creates a datalist for Color Property in Product Entity.<br/><br/>
 
### <br/>*Features planned -*
 1.Unit Testing and Integration Testing projects.<br/>
 2.Adding Service Layer and Repository Layer.<br/>
 3.Adding View Models for the various views when Controller is sufficiently complex. In products view , ViewBag is used.<br/>
 4.Async Partial Views - if there is large data for single item , then load it partially quickly , then continue to load after full web page is loaded. For eg.  - in products list view , each product could be spinning widget, where thumbnail photo(not yet shown in view) could be loaded asynchronously), while all records are loaded.<br/>
 5.Making all the Views for one of the Controllers Asynchronous by loading partial views into the Layout view - better User experience. <br/>
