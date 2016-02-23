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

    def page(self):
        # Works also with default paths
        return self.view()

    def paramSample(self, id, id2 = 'default-value for id2'):
        # Works also with default paths
        model = SampleModel()
        model.id = id
        model.id2 = id2
        return self.view("~/Views/Home/ParamSample.cshtml", model)

class SampleModel:
    id = 0
    id2 = ''

class ProductController(aspnet.Controller):

    def index(self):
        return self.view("~/Views/Product/Index.cshtml")
