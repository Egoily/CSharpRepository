using System;

namespace ee.Core.Net
{
    //
    // Summary:
    //     Place on an action to expose it directly via a route.
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class ResourceRouteAttribute : Attribute
    {
        //
        // Summary:
        //     Initializes a new instance of RouteAttribute class.
        public ResourceRouteAttribute()
        {

        }
        //
        // Summary:
        //     Initializes a new instance of the RouteAttribute class.
        //
        // Parameters:
        //   template:
        //     The route template describing the URI pattern to match against.
        public ResourceRouteAttribute(string template, string method)
        {
            this.Resource = template;
            this.Method = method;
        }

        //
        // Returns:
        //     Returns System.String.
        public string Name { get; set; }
        //
        // Returns:
        //     Returns System.Int32.
        public int Order { get; set; }
        //
        // Returns:
        //     Returns System.String.
        public string Resource { get; }

        public string Method { get; }
    }
}
