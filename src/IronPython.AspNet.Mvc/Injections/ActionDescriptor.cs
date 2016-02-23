using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IronPython.AspNet.Mvc
{
    /// <summary>
    /// Action descriptor for invoking IronPython actions inside of IronPyton controller
    /// </summary>
    public class DynamicActionDescriptor : ActionDescriptor
    {
        #region Private Member
        private ControllerContext controllerContext;
        private ControllerDescriptor controllerDescriptor;
        private ParameterDescriptor[] descriptor;
        private string actionName;
        private IronPython.Runtime.Method methodInfo;
        #endregion

        /// <summary>
        /// Create AcrtionDescription for the created action
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="controllerDescriptor"></param>
        /// <param name="actionName">Name of the action, must not be the action from url</param>
        public DynamicActionDescriptor(ControllerContext controllerContext, ControllerDescriptor controllerDescriptor, string actionName)
        {
            // Set base information
            this.controllerContext = controllerContext;
            this.controllerDescriptor = controllerDescriptor;
            this.actionName = actionName;

            // Get all required method information
            methodInfo = MvcApplication.Host.ScriptEngine.Operations.GetMember
                (controllerContext.Controller, actionName) as IronPython.Runtime.Method;

            // Get parameter information
            var __func__ = methodInfo.__func__ as Runtime.PythonFunction;
            var paramNames = __func__.func_code.co_varnames;
            var paramCount = __func__.func_code.co_argcount - 1;
            var paramDefaults = __func__.func_defaults;

            // Create parameter descriptions
            var tempDescriptor = new List<ParameterDescriptor>();
            for (int i = 1; i <= paramCount; i++)
            {
                object defaultValue = null;

                if (paramDefaults != null)
                {
                    if (i > (paramCount - paramDefaults.Count))
                    {
                        defaultValue = paramDefaults[i - paramCount - paramDefaults.Count];
                    }
                }

                DynamicParameterDescriptor desc = new DynamicParameterDescriptor
                    (
                        this,
                        paramNames[i].ToString(),
                        defaultValue != null ? defaultValue.GetType() : typeof(object),
                        defaultValue
                    );

                tempDescriptor.Add(desc);
            }
            descriptor = tempDescriptor.ToArray();
        }

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

        #region [GetParameters]
        /// <summary>
        /// Get a list of all available parameter
        /// </summary>
        /// <returns>Parameter list</returns>
        public override ParameterDescriptor[] GetParameters()
        {
            return descriptor;
        }
        #endregion

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
    }
}