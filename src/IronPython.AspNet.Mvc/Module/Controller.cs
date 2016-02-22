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

            protected override void OnActionExecuted(ActionExecutedContext filterContext)
            {
                base.OnActionExecuted(filterContext);

                var result = filterContext.Result as ViewResult;
                if (result != null && Views.is_layout_set() && string.IsNullOrWhiteSpace(result.MasterName))
                {
                    result.MasterName = Views.layout;
                }
            }
        }

        public class DynamicParameterDescriptor : ParameterDescriptor
        {
            public override ActionDescriptor ActionDescriptor
            {
                get;
            }

            public override string ParameterName
            {
                get;
            }

            public override Type ParameterType
            {
                get;
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

                //var desc = new DynamicParameterDescriptor();

                descriptor = new ParameterDescriptor[] 
                {
                //    desc
                };
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
            protected override ActionDescriptor FindAction(ControllerContext controllerContext, ControllerDescriptor controllerDescriptor, string actionName)
            {
                return new DynamicActionDescriptor(controllerContext, controllerDescriptor, actionName);
            }
        }
    }
}