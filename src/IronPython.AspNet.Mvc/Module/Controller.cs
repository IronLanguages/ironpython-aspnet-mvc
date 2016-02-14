using IronPython.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Threading;

namespace IronPython.AspNet.Mvc
{
    /// <summary>
    /// Python soloplan api
    /// </summary>
    public static partial class AspNetMvcAPI
    {
        /// <summary>
        /// Represents an asp.net mvc IronPython controller
        /// </summary>
        [PythonType("Controller")]
        public class Controller : System.Web.Mvc.Controller
        {
            /// <summary>
            /// Module description
            /// http://localhost:38521/
            /// </summary>
            public const string __doc__ = "Module which represents the main asp.net mvc ironpython application";

            public object view(CodeContext context, string view_name)
            {
                var view = View(view_name);
                return view;
            }

            protected override IActionInvoker CreateActionInvoker()
            {
                return new ControllerActionInvokerWithDefaultJsonResult();
            }
        }

        public class DynamicActionDescriptor : ActionDescriptor
        {
            private ControllerContext controllerContext;
            private ControllerDescriptor controllerDescriptor;
            private ParameterDescriptor[] descriptor;
            private string actionName;

            public DynamicActionDescriptor(ControllerContext controllerContext, ControllerDescriptor controllerDescriptor, string actionName)
            {
                this.controllerContext = controllerContext;
                this.controllerDescriptor = controllerDescriptor;
                descriptor = new ParameterDescriptor[] { };
                this.actionName = actionName;
            }

            public override string ActionName
            {
                get
                {
                    return actionName;
                }
            }

            public override ControllerDescriptor ControllerDescriptor
            {
                get
                {
                    return controllerDescriptor;
                }
            }

            public override object Execute(ControllerContext controllerContext, IDictionary<string, object> parameters)
            {
                dynamic d = controllerContext.Controller;
                return d.index();
            }

            public override ParameterDescriptor[] GetParameters()
            {
                return descriptor;
            }
        }

        public class ControllerActionInvokerWithDefaultJsonResult : ControllerActionInvoker
        {
            public const string JsonContentType = "application/json";

            protected override ActionResult CreateActionResult(ControllerContext controllerContext, ActionDescriptor actionDescriptor, object actionReturnValue)
            {
                dynamic d = controllerContext.Controller;
                return d.index();
            }

            protected override void InvokeActionResult(ControllerContext controllerContext, ActionResult actionResult)
            {
                base.InvokeActionResult(controllerContext, actionResult);
            }

            protected override ActionDescriptor FindAction(ControllerContext controllerContext, ControllerDescriptor controllerDescriptor, string actionName)
            {
                return new DynamicActionDescriptor(controllerContext, controllerDescriptor, actionName);
            }

            protected override ActionResult InvokeActionMethod(ControllerContext controllerContext, ActionDescriptor actionDescriptor, IDictionary<string, object> parameters)
            {
                dynamic d = controllerContext.Controller;
                return d.index();
            }

            protected override ControllerDescriptor GetControllerDescriptor(ControllerContext controllerContext)
            {
                return base.GetControllerDescriptor(controllerContext);
            }

            protected override ActionExecutedContext InvokeActionMethodWithFilters(ControllerContext controllerContext, IList<IActionFilter> filters, ActionDescriptor actionDescriptor, IDictionary<string, object> parameters)
            {
                return base.InvokeActionMethodWithFilters(controllerContext, filters, actionDescriptor, parameters);
            }

            protected override ExceptionContext InvokeExceptionFilters(ControllerContext controllerContext, IList<IExceptionFilter> filters, Exception exception)
            {
                return base.InvokeExceptionFilters(controllerContext, filters, exception);
            }
        }
    }
}