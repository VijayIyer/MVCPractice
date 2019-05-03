An MVC project with Entity Framework as Data Access Layer. The Entity Framework is DB first, created from AdventureWorks sample database. 
Features - 
1.Here CRUD operations are done on a few of the entities through various views - Index to list items , Create , Edit , Delete. 
2.In Products Index view , Pagination has been added using PagedListMVC library. Also search bar is added to search for a product entity by name. 
3.Custom DataAnnotation Validations have been added to the Products Model  - in ProductPartial.cs file. One of them is to check whether Product Number adheres to a certain format , this is checked using regular expressions. The Other is for remote validation , to check whether Product Name is unique by cross verifying with database. Both are server validations which fire on postback of the page. 
4.
