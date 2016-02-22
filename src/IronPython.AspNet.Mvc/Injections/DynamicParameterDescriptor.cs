using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IronPython.AspNet.Mvc
{
    public class DynamicParameterDescriptor : ParameterDescriptor
    {
        public override ActionDescriptor ActionDescriptor
        {
            get;
        }

        public override string ParameterName
        {
            get;
        }

        public override Type ParameterType
        {
            get;
        }
    }
}