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
        /// Handle action filter
        /// </summary>
        [PythonType("Filter")]
        public static class Filter
        {
            /// <summary>
            /// Module description
            /// </summary>
            public const string __doc__ = "Module for handling action filter";
            
            private static IDictionary<PythonFunction, Decorator.ActionDecorator> __actionDecorators;

            /// <summary>
            /// Create routing system
            /// </summary>
            static Filter()
            {
                // Create list
                __actionDecorators = new Dictionary<PythonFunction, Decorator.ActionDecorator>();
            }

            #region [Routing Methods (GET/POST/PUT/DELETE)]

            /// <summary>
            /// Make a method only receive Get requests
            /// </summary>
            /// <returns>Rule for checking requests</returns>
            public static object httpGet(object function)
            {
                return register_http_method(function, "GET");
            }

            /// <summary>
            /// Make a method only accept Post requests
            /// </summary>
            /// <param name="func">Function parameter</param>
            /// <returns></returns>
            public static object httpPost(object function)
            {
                return register_http_method(function, "POST");
            }

            /// <summary>
            /// Make a method only accept Put requests
            /// </summary>
            /// <returns></returns>
            public static object httpPut(object function)
            {
                return register_http_method(function, "PUT");
            }

            /// <summary>
            /// Make a request only accept delete requests
            /// </summary>
            /// <returns></returns>
            public static object httpDelete(object function)
            {
                return register_http_method(function, "DELETE");
            }

            /// <summary>
            /// Generic way to register a function
            /// </summary>
            /// <param name="function">Function object</param>
            /// <param name="method">Method to register</param>
            /// <returns>Function object</returns>
            public static object register_http_method(object function, string method)
            {
                if (function is PythonFunction)
                {
                    Decorator.ActionDecorator value = null;
                    if (__actionDecorators.TryGetValue((PythonFunction)function, out value))
                    {
                        value.httpMethod = method;
                    }
                    else
                    {
                        value = new Decorator.ActionDecorator((PythonFunction)function);
                        value.httpMethod = method;
                        __actionDecorators.Add((PythonFunction)function, value);
                    }
                }
                else
                {
                    throw new Exception("Http-Decorator must be applied on a function: " + (function ?? "").ToString());
                }

                return function;
            }
            #endregion

            /// <summary>
            /// Contains all decorated actions
            /// </summary>
            public static IDictionary<PythonFunction, Decorator.ActionDecorator> actionDecorators
            {
                get
                {
                    return __actionDecorators;
                }
            }
        }
    }
}