#What is it?
==============
A boiler plate solution for an [N-Tiered](http://msdn.microsoft.com/en-us/library/bb384587.aspx), [Service Stack](http://www.servicestack.net/) enabled Application.  I built this primarily for my own use but figured others could get some mileage out of it. It offers:
 + [Entity Framework](http://msdn.microsoft.com/en-us/data/ef.aspx) and [OrmLite](https://github.com/ServiceStack/ServiceStack.OrmLite) already configured (swap one for the other using [Funq](http://funq.codeplex.com/) in the apphost.cs file)
 + A base Repository (Data-layer) and Service (Business-layer) with [CRUD](http://en.wikipedia.org/wiki/Create,_read,_update_and_delete) operations configured. This allows models the ability to inherit CRUD operations quickly
 + Custom Resharper file templates to add model repositories, services, and service stack rest-services.  This allows you to add a model with CRUD operations at every layer in just a few clicks!
 + [Twitter bootstrap](http://twitter.github.com/bootstrap/) with original [.less](http://lesscss.org/) files and .js files configured with ServiceStack's [bundler](https://github.com/ServiceStack/Bundler) so that files can easily be added/modified/removed/ using the original mixins, variables, and plug-ins. Minification comes automatically when in release mode.
 + [Elmah](http://code.google.com/p/elmah/) configured for error logging (visit /elmah.axd when application is running)
 + Service-stack's [miniprofiler](http://miniprofiler.com/) enabled for profiling local requests
 + [AngularJS](http://angularjs.org/) is currently configured with loading and error messages wired into all http requests.  Should be fairly simple to swap angular for other MV* javascript libraries (this wasn't my primary motivation for this).  
 + In memory caching configured.  The Todo RestService comes with examples of using the cache although the caching is not included with the RestService custom template as caching implementation will always vary by application.

#Why?
One of my goals for 2013 is to create and experiment with more side projects. Starting with a visual studio project template leaves a ton of work to add/remove features and nuget packages that can take hours just to setup. I wanted to have a clean boiler plate solution with what I thought were the best .Net tools that also contained good coding practices that I could scale and easily use in a variety of different projects. It'd also be a great opportunity to get other's input to see what other tools and features I'm missing that could be added to the boiler plate solution so that I could learn from others.

#How to use
Simply download the zip file and extract the source.  Then start coding!  I've found that this [visual studio project renamer](http://normankosmal.com/wordpress/?page_id=184) is good at renaming projects (you probably won't want the generic 'application' title I've given it) though there's still a couple files you'll need to modify after the tool runs in the web project but they're pretty easy to find.  To add the custom templates, go to the Templates Explorer from the Resharper menu, click Import, and add the custom-template.DotSettings file.  Make sure to add these templates to the c# quicklist for quick access! 

###Adding Models and configuring the CRUD operations
Add and configure your model in the Application.Models Layer.  Then use the custom resharper file templates to quickly:
 + Add the model repository interface to the Application.DataInterface project
 + Add the model repository to the Application.Datacontext project
 + Add the model service interface to the Application.Business.BusinessLogic.Contract
 + Add the model service to the Application.Business.BusinessLogic
 + Add the model rest service to the Application.Web.RestService.
If anyone knows how I could combine this into one step that'd be a great step into making this process even quicker.  My goal is to get this to be similar to Ruby on Rails where a model is added and have everything else automagically configured.  Of course it's easy to add implementation in the services and repositories created.

If you have any questions using the project let me know and I'll try to update documentation for it.

Also feel free to contribute and improve the project! Here's what I'm trying to add in the near future

  + Add basic CRUD unit/implentation testing to both angularjs and the service stack Rest Services. Make custom templates for these.
  + Create a branch that swaps out ASP.NET's MVC for Service Stack's razor view engine to be run entirely in Mono.
