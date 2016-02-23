using IronPython.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Threading;
using Simplic.Dlr;

namespace IronPython.AspNet.Mvc
{
    /// <summary>
    /// Python aspnet api
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

            #region [View Methods]
            public object view(CodeContext context, string view_name)
            {
                var view = View(view_name);
                return view;
            }

            public object view(CodeContext context, string view_name, object model)
            {
                var view = View(view_name, model);
                return view;
            }

            public object view(CodeContext context)
            {
                var view = View();
                return view;
            }
            #endregion

            #region [CreateActionInvoker]
            /// <summary>
            /// Create invoker which can resolver IronPython actions
            /// </summary>
            /// <returns>New invoker instance</returns>
            protected override IActionInvoker CreateActionInvoker()
            {
                return new DynamicControllerActionInvoker();
            }
            #endregion

            #region [OnActionExecuted]
            /// <summary>
            /// Handle executed actions and set layout and other stuff
            /// </summary>
            /// <param name="filterContext"></param>
            protected override void OnActionExecuted(ActionExecutedContext filterContext)
            {
                base.OnActionExecuted(filterContext);

                var result = filterContext.Result as ViewResult;
                if (result != null && Views.is_layout_set() && string.IsNullOrWhiteSpace(result.MasterName))
                {
                    result.MasterName = Views.layout;
                }
            }
            #endregion

            /// <summary>
            /// Represents the controller class as a Dlr-Class
            /// </summary>
            internal DlrClass __dlrControllerClass
            {
                get;
                set;
            }
        }
    }
}