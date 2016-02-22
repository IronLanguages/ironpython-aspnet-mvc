using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IronPython.AspNet.Mvc
{
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

            // If memebrs match, return
            if (members.Contains(actionName))
            {
                return new DynamicActionDescriptor(controllerContext, controllerDescriptor, actionName);
            }
            else
            {
                // Search with ignore-case mode
                foreach (var member in members)
                {
                    if (member.ToLower() == actionName.ToLower())
                    {
                        return new DynamicActionDescriptor(controllerContext, controllerDescriptor, member);
                    }
                }
            }

            return null;
        }
    }
}