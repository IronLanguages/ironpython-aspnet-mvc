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
        /// Managed bundles for loading resources
        /// </summary>
        [PythonType("Bundles")]
        public static class Bundles
        {
            /// <summary>
            /// Module description
            /// </summary>
            public const string __doc__ = "Managed bundles for loading resources";

            /// <summary>
            /// Register a bundle in the global bundle collection
            /// </summary>
            /// <param name="bundle">Bundle instance</param>
            public static void add(Bundle bundle)
            {
                BundleTable.Bundles.Add(bundle._bundle);
            }
        }

        /// <summary>
        /// Custom bundle-base
        /// </summary>
        [PythonType("Bundle")]
        public class Bundle
        {
            private System.Web.Optimization.Bundle __bundle;

            /// <summary>
            /// Create bundle instance
            /// </summary>
            /// <param name="bundle">Original bundle</param>
            internal Bundle(System.Web.Optimization.Bundle bundle)
            {
                this.__bundle = bundle;
            }

            /// <summary>
            /// Include single paths
            /// </summary>
            /// <param name="virtualPaths">Path List</param>
            /// <returns>Current bundle instance</returns>
            public Bundle include(params string[] virtualPaths)
            {
                __bundle.Include(virtualPaths);
                return this;
            }

            /// <summary>
            /// Include complete directory
            /// </summary>
            /// <param name="directoryVirtualPath"></param>
            /// <param name="searchPattern"></param>
            /// <returns>Current bundle instance</returns>
            public Bundle include_directory(string directoryVirtualPath, string searchPattern)
            {
                __bundle.IncludeDirectory(directoryVirtualPath, searchPattern);
                return this;
            }

            /// <summary>
            /// Include complete directory with subdirectories
            /// </summary>
            /// <param name="directoryVirtualPath"></param>
            /// <param name="searchPattern"></param>
            /// <param name="searchSubdirectories"></param>
            /// <returns>Current bundle instance</returns>
            public Bundle include_directory(string directoryVirtualPath, string searchPattern, bool searchSubdirectories)
            {
                __bundle.IncludeDirectory(directoryVirtualPath, searchPattern, searchSubdirectories);
                return this;
            }

            /// <summary>
            /// Access mvc bundle instance
            /// </summary>
            public System.Web.Optimization.Bundle _bundle
            {
                get
                {
                    return __bundle;
                }
            }
        }

        /// <summary>
        /// Script bunde
        /// </summary>
        [PythonType("ScriptBundle")]
        public class ScriptBundle : Bundle
        {
            /// <summary>
            /// Create script bundle
            /// </summary>
            /// <param name="virtual_path">Path to the script</param>
            public ScriptBundle(string virtual_path) 
                : base(new System.Web.Optimization.ScriptBundle(virtual_path))
            {

            }

            /// <summary>
            /// Create script bundle
            /// </summary>
            /// <param name="virtual_path">Path to the script</param>
            /// <param name="cdn_path">CDN base path</param>
            public ScriptBundle(string virtual_path, string cdn_path)
                : base(new System.Web.Optimization.ScriptBundle(virtual_path, cdn_path))
            {

            }
        }

        /// <summary>
        /// Create style bundle
        /// </summary>
        [PythonType("StyleBundle")]
        public class StyleBundle : Bundle
        {
            /// <summary>
            /// Create style bundle
            /// </summary>
            /// <param name="virtual_path">Path to the style</param>
            public StyleBundle(string virtual_path) 
                : base(new System.Web.Optimization.ScriptBundle(virtual_path))
            {

            }

            /// <summary>
            /// Create style bundle
            /// </summary>
            /// <param name="virtual_path">Path to the style</param>
            /// <param name="cdn_path">CDN base path</param>
            public StyleBundle(string virtual_path, string cdn_path)
                : base(new System.Web.Optimization.ScriptBundle(virtual_path, cdn_path))
            {

            }
        }
    }
}