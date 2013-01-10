#What is it?
==============
A boiler plate solution for an [N-Tiered](http://msdn.microsoft.com/en-us/library/bb384587.aspx), [Service Stack](http://www.servicestack.net/) enabled Application.  I built this primarily for my own use but figured others could get some mileage out of it. It offers:
 + Entity Framework and OrmLite already configured (swappable)
 + A base Repository (Data-layer) and base Service (Business-layer) with CRUD operations configured. This allows models the ability to inherit the CRUD operations quickly
 + Custom Resharper file templates to add model repositories, services, and service stack rest-services
 + Twitter bootstrap with original .less files and .js files configured with ServiceStack's bundler so that files can easily be added/modified/removed/ using the original mixins, variables, and plug-ins.
 + Elmah configured for error logging (visit /elmah.axd when application is running)
 + Service-stack's miniprofiler wired for profile local requests
 + AngularJS is currently configured with loading and error messages wired into all http requests.  Should be fairly simple to swap angular for other MV* javascript libraries (this wasn't my primary motivation for this)

#How to use
Simply
