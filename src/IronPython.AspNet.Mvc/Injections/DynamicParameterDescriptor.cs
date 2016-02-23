using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IronPython.AspNet.Mvc
{
    /// <summary>
    /// Represents a dynamic parameter of an IronPython action
    /// </summary>
    public class DynamicParameterDescriptor : ParameterDescriptor
    {
        #region Private Member
        private ActionDescriptor actionDescriptor;
        private string parameterName;
        private Type parameterType;
        private object defaultValue;
        #endregion

        #region Constructor
        /// <summary>
        /// Create new descriptor
        /// </summary>
        /// <param name="actionDescriptor">Instance of the action descriptor</param>
        /// <param name="parameterName">Name of the parameter</param>
        /// <param name="parameterType">Type, mostly object</param>
        /// <param name="defaultValue">Default value, by default null</param>
        public DynamicParameterDescriptor(ActionDescriptor actionDescriptor, string parameterName, Type parameterType, object defaultValue)
        {
            this.actionDescriptor = actionDescriptor;
            this.parameterName = parameterName;
            this.parameterType = parameterType;
            this.defaultValue = defaultValue;
        }
        #endregion

        #region Public Member
        /// <summary>
        /// Action descriptor instance, must not be null
        /// </summary>
        public override ActionDescriptor ActionDescriptor
        {
            get
            {
                return actionDescriptor;
            }
        }

        /// <summary>
        /// Name of the action parameter/argument
        /// </summary>
        public override string ParameterName
        {
            get
            {
                return parameterName;
            }
        }

        /// <summary>
        /// Type of the action parameter/argument
        /// </summary>
        public override Type ParameterType
        {
            get
            {
                return parameterType;
            }
        }

        /// <summary>
        /// Default value set in the IronPython action for a parameter/argument
        /// </summary>
        public override object DefaultValue
        {
            get
            {
                return defaultValue;
            }
        }
        #endregion
    }
}