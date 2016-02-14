using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: IronPython.Runtime.PythonModule("aspnet", typeof(IronPython.AspNet.Mvc.AspNetMvcAPI))]
namespace IronPython.AspNet.Mvc
{
    /// <summary>
    /// Python soloplan api
    /// </summary>
    public static partial class AspNetMvcAPI
    {
        /// <summary>
        /// Module description
        /// </summary>
        public const string __doc__ = "";
    }
}
