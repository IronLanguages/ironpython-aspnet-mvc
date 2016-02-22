using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IronPython.AspNet.Mvc
{
    public class DynamicParameterDescriptor : ParameterDescriptor
    {
        private ActionDescriptor actionDescriptor;
        private string parameterName;
        private Type parameterType;

        public DynamicParameterDescriptor(ActionDescriptor actionDescriptor, string parameterName, Type parameterType)
        {
            this.actionDescriptor = actionDescriptor;
            this.parameterName = parameterName;
            this.parameterType = parameterType;
        }

        public override ActionDescriptor ActionDescriptor
        {
            get
            {
                return actionDescriptor;
            }
        }

        public override string ParameterName
        {
            get
            {
                return parameterName;
            }
        }

        public override Type ParameterType
        {
            get
            {
                return parameterType;
            }
        }
    }
}