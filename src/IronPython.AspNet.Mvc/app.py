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

class HomeController(aspnet.Controller):

    def index(self):
        return self.view("~/Views/Index.cshtml");
