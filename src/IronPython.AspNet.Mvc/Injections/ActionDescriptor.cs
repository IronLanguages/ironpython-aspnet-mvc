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
            this.controllerContext = controllerContext;
            this.controllerDescriptor = controllerDescriptor;

            //var desc = new DynamicParameterDescriptor();

            descriptor = new ParameterDescriptor[]
            {
                //    desc
            };
            this.actionName = actionName;
        }

        public override string ActionName
        {
            get
            {
                return actionName;
            }
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

        public override object Execute(ControllerContext controllerContext, IDictionary<string, object> parameters)
        {
            dynamic d = controllerContext.Controller;
            return d.index();
        }

        public override ParameterDescriptor[] GetParameters()
        {
            return descriptor;
        }
    }
}