Manage your data with Javascript easily.

##Features
* Tracks objects and persists changes to server
* Can work with Knockout and Angular objects (Backbone soon, and others can be implemented easily)
* Can work with Q and jQuery promises
* Supports inheritance
* Supports aggregate functions
* Can work without metadata
* Can work with Mvc and WebApi Controllers
* Supports property, entity validations
* Can use existing data annotation validations (carries multi-language resource messages to client)
* Can query server with Http POST
* Can be extended to support almost every library (client and server side), flexible architecture
* Auto fix navigation properties (after foreign key set, entity attach etc..)
* Can check-auto convert values for it's proper data types
* Can be internationalized (for internal messages, validation messages etc..)

##Current prerequisities
* Entity Framework (more coming soon)
* WebApi or Asp.Net Mvc project for service
* Knockout.js or Angular.js for providing observable objects (more coming soon)
* JQuery for ajax operations

##Usage
* Create a Controller and inherit from BeetleApiController, generic parameter tells we are using Entity Framework context handler with TestEntities context (DbContext).
```cs
public class BeetleTestController : BeetleApiController<EFContextHandler<TestEntities>> {
		
		[HttpGet]
		public IQueryable<Entity> Entities() {
			return ContextHandler.Context.Entities;
		}
}
```
* Configure routing
```cs
public static class BeetleWebApiConfig {

    public static void RegisterBeetlePreStart() {
        GlobalConfiguration.Configuration.Routes.MapHttpRoute("BeetleApi", "api/{controller}/{action}");
    }
}
```
* Create an entity manager
```javascript
var manager = new EntityManager('api/Test');
```
* Create a query
```javascript
var query = manager.createQuery('Entities').where('e => e.Name != null');
```
* Execute the query and edit the data
```javascript
manager.executeQuery(query)
    .then(function (data) {
        self.entities = data;
        data[0].UserNameCreate = 'Test Name';
    })
```
* Execute local query
```javascript
var hasCanceled = self.entities.asQueryable().any('e => e.IsCanceled == true').execute();
// q is shortcut for asQueryable, x is shortcut for execute
var hasDeleted = self.entities.q().any('e => e.IsDeleted').x();
// alias is optional
var hasDeleted = self.entities.q().any('IsDeleted').x();

// with beetle.arrayExtensions.js we can write queries like these;
// this query will be executed immediately and returns true or false
var hasExpired = self.entities.any('IsExpired == true');
// below query will be executed after it's length property is accessed (like LINQ GetEnumerator)
var news = self.entities.where('IsNew');
```
* Add a new entity
```javascript
var net = manager.createEntity('EntityType', {Id: beetle.helper.createGuid()});
net.Name = 'Test EntityType';
```
* Delete an entity
```javascript
manager.deleteEntity(net);
```
* Save all changes
```javascript
```
