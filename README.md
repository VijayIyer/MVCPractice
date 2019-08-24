### An MVC project with EF DB first. The DB was is an Azure SQL Database, created from Sample AdventureWorks Database.<br/><br/>*Features -*
 * [**CRUD Operations**](https://github.com/VijayIyer/MVCPractice/blob/master/MVCPractice/Controllers/ProductsController.cs) - on few of the entities  - Index,Create,Edit views.<br/><br/>
 * [**Pagination**](https://github.com/VijayIyer/MVCPractice/blob/master/MVCPractice/Views/Products/Index.cshtml)In Products Index view,Pagination  has been added using PagedListMVC library.Also search bar is added to search for a product entity by name.<br/><br/>
 * [**Custom DataAnnotation Validations**](https://github.com/VijayIyer/MVCPractice/blob/master/MVCPractice/ProductPartial.cs) have been added to the Products Model  - in ProductPartial.cs file. One of them is to check whether Product Number adheres to a certain format , this is checked using regular expressions. The Other is for remote validation , to check whether Product Name is unique by cross verifying with database. Both are server validations which fire on postback of the page.<br/><br/>
* **Ajax Request** - for [paging and searching operations](https://github.com/VijayIyer/MVCPractice/blob/master/MVCPractice/Views/Products/ProductsTable.cshtml). These are also firing the Index Action method in ProductsController, but the request is Asynchronous with loading indicator in view till request is complete. This is accomplished through Ajax Helper class and jQuery script in Index View in Products.<br/><br/>
 * **Fluent Validation** - FluentValidationMVC is a library which makes it easy to create rules for the models. To accomplish this seperate class file is created for [Customer.cs - CustomerValidator](https://github.com/VijayIyer/MVCPractice/blob/master/MVCPractice/CustomerValidator.cs) . Few rules are created on Customer Model using Fluent Validation - checking format of emails , and checking that last name is not equal to first name. Here we see how easy it is to create complex validation rules.<br/><br/>
 * **Custom Html Helper** - Created a Custom Html Helper - [CustomHelper](https://github.com/VijayIyer/MVCPractice/blob/master/MVCPractice/CustomHelpers/CustomHelpers.cs). Here , there is one helper method which creates a datalist for Color Property in Product Entity.<br/><br/>
 * **Single Page Application behaviour** -  For Address Views, I have tried to implement a model similar to single page application by -  
   * keeping a [common view](https://github.com/VijayIyer/MVCPractice/blob/master/MVCPractice/Views/Addresses/Shared.cshtml) which will only consist of the Layout 
   * Keeping a blank div where the page content will be loaded after ajax loading is complete.
   * a [ViewBag property](https://github.com/VijayIyer/MVCPractice/blob/master/MVCPractice/Views/Addresses/Shared.cshtml#L47) holds the name of the view and on click of edit or create link, this property is modified and then passed to the get request, which will bring the appropriate edit or create view and replace the content in the blank div.
   * This way even though Address Table has a large number of records, the page is loaded first , then the respective view content is loaded. This gives the feel of a single page application.
 * **Widget like loading behaviour** -  [Here](https://github.com/VijayIyer/MVCPractice/blob/master/MVCPractice/Views/ProductModels/Index.cshtml#L172) I am asynchronously loading contents inside ever table td one by one by -
   *  Keeping a span which sends ajax requests - this will return a [partial view](https://github.com/VijayIyer/MVCPractice/blob/master/MVCPractice/Views/ProductModels/ProductModel.cshtml) which only consists of the content in that cell.In this case, for demonstration purposes only name is being loaded.
   * The model which is passed to the ProductModel.cshtml view is determined in [ProductModelController](https://github.com/VijayIyer/MVCPractice/blob/master/MVCPractice/Controllers/ProductModelsController.cs#L25)
 
 
### <br/>*Features planned -*
 * Unit Testing and Integration Testing projects.<br/>
 * Adding Service Layer and Repository Layer.<br/>
 * Adding View Models for the various views when Controller is sufficiently complex. In products view , ViewBag is used.<br/>
 * Making all the Views for one of the Controllers Asynchronous by loading partial views into the Layout view - better User experience. <br/>
