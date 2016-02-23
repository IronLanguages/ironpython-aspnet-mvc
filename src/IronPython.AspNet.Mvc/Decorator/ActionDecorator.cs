using IronPython.Runtime;
using IronPython.Runtime.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IronPython.AspNet.Mvc.Decorator
{
    /// <summary>
    /// Represents all decorators on an action in an IronPython controller
    /// </summary>
    public class ActionDecorator
    {
        private string _httpMethod;

        /// <summary>
        /// Create new action decorator
        /// </summary>
        /// <param name="pythonType"></param>
        public ActionDecorator(PythonFunction pythonType)
        {

        }

        /// <summary>
        /// Save the http method for the current 
        /// </summary>
        public string httpMethod
        {
            get
            {
                return _httpMethod;
            }

            set
            {
                _httpMethod = value;
            }
        }
    }
}