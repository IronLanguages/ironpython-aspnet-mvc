using IronPython.Runtime.Types;
using Simplic.Dlr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace IronPython.AspNet.Mvc
{
    /// <summary>
    /// Application root
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        #region Private Member
        private static IronPythonLanguage language;
        private static DlrHost<IronPythonLanguage> host;
        private static AspNetMvcAPI.Application app;

        public static DlrHost<IronPythonLanguage> Host
        {
            get
            {
                return host;
            }
        }
        #endregion

        /// <summary>
        /// Start application and initialize all needed systems
        /// </summary>
        protected void Application_Start()
        {
            // Create scripting
            language = new IronPythonLanguage();
            host = new DlrHost<IronPythonLanguage>(language);
            host.AddImportResolver(new NoImporter());

            // Search paths
            Host.AddSearchPath(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath);

            // Load app.py and execute
            var source = Host.ScriptEngine.CreateScriptSourceFromString
                (
                    System.IO.File.ReadAllText(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "app.py")
                );
            source.Execute(Host.DefaultScope.ScriptScope);

            app = (AspNetMvcAPI.Application)Host.DefaultScope.CreateClassInstance("App").Instance;

            // Call start
            app.start();
        }
    }

    /// <summary>
    ///  No importer resolver
    /// </summary>
    public class NoImporter : IDlrImportResolver
    {
        public ResolvedType GetModuleInformation(string path)
        {
            return ResolvedType.None;
        }

        public string GetScriptSource(string path)
        {
            return null;
        }
    }
}
