using IronPython.Runtime;
using IronPython.Runtime.Operations;
using IronPython.Runtime.Types;
using Microsoft.Scripting;
using Microsoft.Scripting.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;

namespace IronPython.AspNet.Mvc
{
    /// <summary>
    /// Python asp.net mvc api
    /// </summary>
    public static partial class AspNetMvcAPI
    {
        /// <summary>
        /// Handle routing options
        /// </summary>
        [PythonType("Routing")]
        public static class Routing
        {
            /// <summary>
            /// Module description
            /// </summary>
            public const string __doc__ = "Module which represents the main asp.net mvc ironpython application";

            private static PythonDictionary __controllers;
            private static IDictionary<PythonFunction, string> __httpMethodFunctionDictionary;

            /// <summary>
            /// Create routing system
            /// </summary>
            static Routing()
            {
                __controllers = new PythonDictionary();

                // Add all available methods
                __httpMethodFunctionDictionary = new Dictionary<PythonFunction, string>();
            }

            #region [Routing Methods (GET/POST/PUT/DELETE)]

            /// <summary>
            /// Make a method only receive Get requests
            /// </summary>
            /// <returns>Rule for checking requests</returns>
            public static object httpGet(object function)
            {
                __httpMethodFunctionDictionary.Add(function as PythonFunction, "GET");
                return function;
            }

            /// <summary>
            /// Make a method only accept Post requests
            /// </summary>
            /// <param name="func">Function parameter</param>
            /// <returns></returns>
            public static object httpPost(object function)
            {
                __httpMethodFunctionDictionary.Add(function as PythonFunction, "POST");
                return function;
            }

            /// <summary>
            /// Make a method only accept Put requests
            /// </summary>
            /// <returns></returns>
            public static object httpPut(object function)
            {
                __httpMethodFunctionDictionary.Add(function as PythonFunction, "PUT");
                return function;
            }

            /// <summary>
            /// Make a request only accept delete requests
            /// </summary>
            /// <returns></returns>
            public static object httpDelete(object function)
            {
                __httpMethodFunctionDictionary.Add(function as PythonFunction, "DELETE");
                return function;
            }
            #endregion

            /// <summary>
            /// Detect and register all available controller in the system
            /// </summary>
            /// <param name="context">CodeContext, passed from IronPython automatically</param>
            public static void register_all(CodeContext context)
            {
                // Get all Controller in the current scope
                var items = MvcApplication.Host.DefaultScope.ScriptScope.GetItems();

                var pythonController = DynamicHelpers.GetPythonTypeFromType(typeof(AspNetMvcAPI.Controller));

                foreach (var item in items)
                {
                    // Check type and get controller
                    if (item.Value is PythonType)
                    {
                        var pt = (PythonType)item.Value;
                        var baseType = pt.__getattribute__(context, "__base__");

                        // If is controlelr
                        if (baseType == pythonController)
                        {
                            // Add to controller list
                            __controllers.Add(item.Key.Replace("Controller", ""), pt);
                        }
                    }
                }

                // Register routes
                var routes = System.Web.Routing.RouteTable.Routes;
                routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


                routes.MapRoute(
                    name: "Default",
                    url: "{controller}/{action}/{id}",
                    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );

                IControllerFactory factory = new CustomControllerFactory();
                ControllerBuilder.Current.SetControllerFactory(factory);
            }

            /// <summary>
            /// List of available controller
            /// </summary>
            public static PythonDictionary controllers
            {
                get
                {
                    return __controllers;
                }
            }

            /// <summary>
            /// Contains all decorated actions
            /// </summary>
            public static IDictionary<PythonFunction, string> httpMethodFunctionDictionary
            {
                get
                {
                    return __httpMethodFunctionDictionary;
                }
            }
        }
    }
}