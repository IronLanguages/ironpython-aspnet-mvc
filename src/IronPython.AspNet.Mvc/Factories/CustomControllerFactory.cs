using Simplic.Dlr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace IronPython.AspNet.Mvc
{
    /// <summary>
    /// Factory for creating IronPython based controller
    /// </summary>
    public class CustomControllerFactory : IControllerFactory
    {
        #region [CreateController]
        /// <summary>
        /// Create an instance of an IronPython controller
        /// </summary>
        /// <param name="requestContext">Request containing information</param>
        /// <param name="controllerName">Name of the controller to create</param>
        /// <returns>Instance of a controller if found, else null</returns>
        public IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            var rawCtrl = AspNetMvcAPI.Routing.controllers.Where(item => item.Key != null)
                .FirstOrDefault(item => item.Key.ToString().Replace("Controller", "") == controllerName);

            if (rawCtrl.Key == null)
            {
                return null;
            }

            // Create dlr class
            var type = (rawCtrl.Value as Runtime.Types.PythonType);
            var dlrClass = new DlrClass(MvcApplication.Host.DefaultScope, type);

            // Cast to returable controller and set dlr meta class
            var controller = (AspNetMvcAPI.Controller)dlrClass.Instance;
            controller.__dlrControllerClass = dlrClass;

            requestContext.RouteData.Values["controller"] = controllerName;

            return controller;
        }
        #endregion

        #region [GetControllerSessionBehavior]
        /// <summary>
        /// Session behaviour
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        public System.Web.SessionState.SessionStateBehavior GetControllerSessionBehavior(
           System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }
        #endregion

        #region [ReleaseController]
        /// <summary>
        /// Release the created controller
        /// </summary>
        /// <param name="controller">Controller instance</param>
        public void ReleaseController(IController controller)
        {
            IDisposable disposable = controller as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
        #endregion
    }
}