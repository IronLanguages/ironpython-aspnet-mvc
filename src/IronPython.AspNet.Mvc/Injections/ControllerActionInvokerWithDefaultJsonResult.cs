using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IronPython.AspNet.Mvc
{
    public class ControllerActionInvokerWithDefaultJsonResult : ControllerActionInvoker
    {
        protected override ActionDescriptor FindAction(ControllerContext controllerContext, ControllerDescriptor controllerDescriptor, string actionName)
        {
            return new DynamicActionDescriptor(controllerContext, controllerDescriptor, actionName);
        }
    }
}