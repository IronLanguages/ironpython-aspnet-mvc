# IronPython-AspNet-Mvc
Create AspNet MVC based websites and applications with IronPython to combine the power of the .Net Framework and Python.

The following things are currently supported:

1. Application Startup
2. Creating controller and actions
3. Using different layout techniques
4. Use bundles for embedding Script and Styles
5. Use method decorators for routing and http-method filter
6. Base implementation of the filter system

## Simple example
This example shows some very basic usage. It creates an application and one `Home` controller which will be registered automatically.

```python
# ------------------------------------------------
# This is the root of any IronPython based
# AspNet MVC application.
# ------------------------------------------------

import aspnet

# Define "root" class of the MVC-System
class App(aspnet.Application):
    
    # Start IronPython asp.net mvc application. 
    # Routes and other stuff can be registered here
    def start(self):
        
        # Register all routes
        aspnet.Routing.register_all()

        # Set layout
        aspnet.Views.set_layout('~/Views/Shared/_Layout.cshtml')

class HomeController(aspnet.Controller):

    def index(self):
        return self.view("~/Views/Home/Index.cshtml")

```

## Creating a controller

For creating a controller, just let your class derive from `aspnet.Controller`:

```python
class ProductController(aspnet.Controller):
    
    # Index endpoint which is available over: http://yoursite.com/product/index
    def index(self):
        return self.view()
```

All controller can be automatically registered using: `aspnet.Routing.register_all()` on application startup.

## Making a action accepts only post

To make an action only accept post request, just use the `httpPost` decorator:

```python
class ProductController(aspnet.Controller):
    
    @aspnet.Filter.httpPost
    def add(self, model):
        # ....
```

## Bundles

Register bundles to load them in your views:

```python
# Load style bundle
bundle = aspnet.StyleBundle('~/Content/css')
bundle.include("~/Content/css/all.css")

aspnet.Bundles.add(bundle)
```
