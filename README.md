#What is it?
==============
A boiler plate solution for an [N-Tiered](http://msdn.microsoft.com/en-us/library/bb384587.aspx), [Service Stack](http://www.servicestack.net/) enabled Application.  I built this primarily for my own use but figured others could get some mileage out of it. It offers:
 + [Entity Framework](http://msdn.microsoft.com/en-us/data/ef.aspx) and [OrmLite](https://github.com/ServiceStack/ServiceStack.OrmLite) already configured (swappable via IOC)
 + A base Repository (Data-layer) and Service (Business-layer) with [CRUD](http://en.wikipedia.org/wiki/Create,_read,_update_and_delete) operations configured. This allows models the ability to inherit the CRUD operations quickly
 + Custom Resharper file templates to add model repositories, services, and service stack rest-services.  This allows you to add a model with CRUD operations at every layer in just a few clicks!
 + [Twitter bootstrap](http://twitter.github.com/bootstrap/) with original [.less](http://lesscss.org/) files and .js files configured with ServiceStack's bundler so that files can easily be added/modified/removed/ using the original mixins, variables, and plug-ins.
 + [Elmah](http://code.google.com/p/elmah/) configured for error logging (visit /elmah.axd when application is running)
 + Service-stack's [miniprofiler](http://miniprofiler.com/) enabled for profile local requests
 + [AngularJS](http://angularjs.org/) is currently configured with loading and error messages wired into all http requests.  Should be fairly simple to swap angular for other MV* javascript libraries (this wasn't my primary motivation for this)

#How to use
Simply download the zip file and extract the source.  Then start coding!  I've found that this [visual studio project renamer](http://normankosmal.com/wordpress/?page_id=184) is good at renaming projects (you probably won't want the generic 'application' title I've given it) though there's still a couple files you'll need to modify after the tool runs in the web project but they're pretty easy to find.  To add the custom templates, go to the Templates Explorer from the Resharper menu, click Import, and add the custom-template.DotSettings file.  Make sure to add these templates to the c# quicklist for quick access!

Feel free to contribute to the project! Here's what I'm trying to add in the near future

  + Add basic CRUD unit testing to both angularjs and the service stack Rest Services. Make custom templates for these.
  + Create a simple window's phone project that uses the REST API.
  + Create a branch that swaps out ASP.NET's MVC for Service Stack's razor view engine to be run entirely in Mono.
