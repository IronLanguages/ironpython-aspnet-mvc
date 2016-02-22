using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IronPython.AspNet.Mvc
{
    public class DynamicActionDescriptor : ActionDescriptor
    {
        private ControllerContext controllerContext;
        private ControllerDescriptor controllerDescriptor;
        private ParameterDescriptor[] descriptor;
        private string actionName;

        public DynamicActionDescriptor(ControllerContext controllerContext, ControllerDescriptor controllerDescriptor, string actionName)
        {
            // Set base information
            this.controllerContext = controllerContext;
            this.controllerDescriptor = controllerDescriptor;
            this.actionName = actionName;

            // Get all required method information
            IronPython.Runtime.Method methodInfo = MvcApplication.Host.ScriptEngine.Operations.GetMember
                (controllerContext.Controller, actionName) as IronPython.Runtime.Method;

            // Get parameter information
            var __func__ = methodInfo.__func__ as Runtime.PythonFunction;
            var varnames = __func__.func_code.co_varnames;
            var paramCount = __func__.func_code.co_argcount - 1;
            
            // Create parameter descriptions
            var tempDescriptor = new List<ParameterDescriptor>();
            for (int i = 1; i <= paramCount; i++)
            {
                DynamicParameterDescriptor desc = new DynamicParameterDescriptor
                    (
                        this,
                        varnames[i].ToString(),
                        typeof(object)
                    );

                tempDescriptor.Add(desc);
            }
            descriptor = tempDescriptor.ToArray();
        }

        #region [ActionName]
        /// <summary>
        /// Getter for the action name
        /// </summary>
        public override string ActionName
        {
            get
            {
                return actionName;
            }
        }
        #endregion

        public override object[] GetCustomAttributes(bool inherit)
        {
            return base.GetCustomAttributes(inherit);
        }

        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return base.GetCustomAttributes(attributeType, inherit);
        }

        public override IEnumerable<FilterAttribute> GetFilterAttributes(bool useCache)
        {
            return base.GetFilterAttributes(useCache);
        }

        public override ICollection<ActionSelector> GetSelectors()
        {
            return base.GetSelectors();
        }

        public override ControllerDescriptor ControllerDescriptor
        {
            get
            {
                return controllerDescriptor;
            }
        }

        /// <summary>
        /// Invoke the ironpython writte action method
        /// </summary>
        /// <param name="controllerContext">Controller information</param>
        /// <param name="parameters">Parameter list</param>
        /// <returns>Result of the executed action</returns>
        public override object Execute(ControllerContext controllerContext, IDictionary<string, object> parameters)
        {
            try
            {
                // Try to execute the existing methods
                var tmpController = (controllerContext.Controller as AspNetMvcAPI.Controller);
                return tmpController.__dlrControllerClass.CallFunction(actionName, parameters.Select(item => item.Value).ToArray());
            }
            catch (NotImplementedException ex)
            {
                throw new NotImplementedException("Could not find action `" + actionName + "` in controller " + controllerContext.Controller.ToString(), ex);
            }
        }

        public override ParameterDescriptor[] GetParameters()
        {
            return descriptor;
        }
    }
}