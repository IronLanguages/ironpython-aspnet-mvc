using IronPython.Runtime;
using IronPython.Runtime.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IronPython.AspNet.Mvc
{
    /// <summary>
    /// Python soloplan api
    /// </summary>
    public static partial class AspNetMvcAPI
    {
        /// <summary>
        /// Module which represents the main asp.net mvc ironpython application
        /// </summary>
        [PythonType("Application")]
        public class Application
        {
            /// <summary>
            /// Module description
            /// </summary>
            public const string __doc__ = "Module which represents the main asp.net mvc ironpython application";

            /// <summary>
            /// Start the current application
            /// </summary>
            public virtual void start()
            {

            }
        }
    }
}