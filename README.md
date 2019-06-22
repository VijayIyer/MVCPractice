### An MVC project with Entity Framework as Data Access Layer. The Entity Framework is DB first, created from AdventureWorks sample database.<br/><br/>*Features -*
#### [1](https://github.com/VijayIyer/MVCPractice/blob/master/MVCPractice/Controllers/ProductsController.cs).Here CRUD operations are done on a few of the entities through various views - Index to list items,Create,Edit,Delete. 
#### [2](https://github.com/VijayIyer/MVCPractice/blob/master/MVCPractice/Views/Products/Index.cshtml).In Products Index view, Pagination has been added using PagedListMVC library.Also search bar is added to search for a product entity by name.
#### [3](https://github.com/VijayIyer/MVCPractice/blob/master/MVCPractice/ProductPartial.cs).Custom DataAnnotation Validations have been added to the Products Model  - in ProductPartial.cs file. One of them is to check whether Product Number adheres to a certain format , this is checked using regular expressions. The Other is for remote validation , to check whether Product Name is unique by cross verifying with database. Both are server validations which fire on postback of the page.
#### [4](https://github.com/VijayIyer/MVCPractice/blob/master/MVCPractice/Views/Products/ProductsTable.cshtml).Ajax Request - for paging and searching operations. These are also firing the Index Action method in ProductsController, but the request is Asynchronous with loading indicator in view till request is complete. This is accomplished through Ajax Helper and jQuery script in Index View in Products.
#### [5](https://github.com/VijayIyer/MVCPractice/blob/master/MVCPractice/CustomerValidator.cs).Fluent Validation - FluentValidationMVC is a library which makes it easy to create rules for the models. To accomplish this seperate class file is created for Customer.cs - CustomerValidator . Few rules are created on Customer Model using Fluent Validation - checking format of emails , and checking that last name is not equal to first name. Here we see how easy it is to create complex validation rules.
#### [6](https://github.com/VijayIyer/MVCPractice/blob/master/MVCPractice/CustomHelpers/CustomHelpers.cs).A custom HTML Helper is created - CustomHelper. Here , there is one helper method which creates a datalist for Color Property in Product Entity.
### <br/>*Features planned -*
#### 1.Unit Testing and Integration Testing projects.
#### 2.Adding Service Layer and Repository Layer.
#### 3.Adding View Models for the various views when Controller is sufficiently complex. In products view , ViewBag is used.
#### 4.Async Partial Views - if there is large data for single item , then load it partially quickly , then continue to load after full web page is loaded. For eg.  - in products list view , each product could be spinning widget, where thumbnail photo(not yet shown in view) could be loaded asynchronously), while all records are loaded.
#### 5.Making all the Views for one of the Controllers Asynchronous by loading partial views into the Layout view - better User experience. 
