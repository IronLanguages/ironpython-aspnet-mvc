using IronPython.Runtime;
using IronPython.Runtime.Types;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// 
        /// </summary>
        [PythonType("Views")]
        public static class Views
        {
            /// <summary>
            /// Module description
            /// </summary>
            public const string __doc__ = "";

            private static string __layout;

            /// <summary>
            /// Set default layout
            /// </summary>
            /// <param name="context"></param>
            /// <param name="layout">Layout path</param>
            public static void set_layout(CodeContext context, string layout)
            {
                __layout = layout;
            }

            /// <summary>
            /// Return true, of a default layout is set programatically
            /// </summary>
            /// <returns></returns>
            public static bool is_layout_set()
            {
                return !string.IsNullOrWhiteSpace(__layout);
            }

            /// <summary>
            /// Path to the default layout
            /// </summary>
            public static string layout
            {
                get
                {
                    return __layout;
                }
            }
        }
    }
}