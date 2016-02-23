using IronPython.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IronPython.AspNet.Mvc
{
    /// <summary>
    /// Create dynamic action invoker and resolve action-names
    /// </summary>
    public class DynamicControllerActionInvoker : ControllerActionInvoker
    {
        /// <summary>
        /// Select a member
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="controllerDescriptor"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        protected override ActionDescriptor FindAction(ControllerContext controllerContext, ControllerDescriptor controllerDescriptor, string actionName)
        {
            // Get all members in the controller
            var members = MvcApplication.Host.ScriptEngine.Operations.GetMemberNames(controllerContext.Controller);
            string resolvedActionName = "";

            // If memebrs match, return
            if (members.Contains(actionName))
            {
                resolvedActionName = actionName;
            }
            else
            {
                // Search with ignore-case mode
                foreach (var member in members)
                {
                    if (member.ToLower() == actionName.ToLower())
                    {
                        resolvedActionName = member;
                    }
                }
            }

            // If an action was found
            if (!string.IsNullOrWhiteSpace(resolvedActionName))
            {
                // Search for all method of this type
                var httpMethod = controllerContext.HttpContext.Request.HttpMethod; //GET POST DELETE PUT

                var member = MvcApplication.Host.ScriptEngine.Operations.GetMember(controllerContext.Controller, resolvedActionName);
                var methodInfo = member as IronPython.Runtime.Method;
                var pythonFunction = methodInfo.__func__ as PythonFunction;

                if (AspNetMvcAPI.Routing.httpMethodFunctionDictionary.ContainsKey(pythonFunction) 
                    && AspNetMvcAPI.Routing.httpMethodFunctionDictionary[pythonFunction] == httpMethod)
                {
                    return new DynamicActionDescriptor(controllerContext, controllerDescriptor, resolvedActionName, pythonFunction);
                }
                else if (httpMethod == "GET"
                    && !AspNetMvcAPI.Routing.httpMethodFunctionDictionary.ContainsKey(pythonFunction)) // No decorator equals GET-Method
                {
                    return new DynamicActionDescriptor(controllerContext, controllerDescriptor, resolvedActionName, pythonFunction);
                }
            }

            return null;
        }
    }
}