// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace Microsoft.ReverseProxy.Middleware
{
    /// <summary>
    /// Store current ClusterConfig and Tracks proxy cluster destinations that are available to handle the current request.
    /// </summary>
    public class ReverseProxyFeature : IReverseProxyFeature
    {
        public IRouteConfig RouteConfig { get; set; }

        /// <summary>
        /// Cluster config for the the current request.
        /// </summary>
        public IClusterConfig ClusterConfig { get; set; }

        /// <summary>
        /// Cluster destinations that can handle the current request.
        /// </summary>
        public IReadOnlyList<IDestination> AvailableDestinations { get; set; }
    }
}
