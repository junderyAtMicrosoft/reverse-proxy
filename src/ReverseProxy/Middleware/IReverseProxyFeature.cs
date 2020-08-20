// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;
using Microsoft.ReverseProxy.RuntimeModel;
using Microsoft.ReverseProxy.Service.Proxy.Infrastructure;
using Microsoft.ReverseProxy.Service.RuntimeModel.Transforms;

namespace Microsoft.ReverseProxy.Middleware
{
    /// <summary>
    /// Store current ClusterConfig and Tracks proxy cluster destinations that are available to handle the current request.
    /// </summary>
    public interface IReverseProxyFeature
    {
        /// <summary>
        /// Route config for the current endpoint
        /// </summary>
        IRouteConfig RouteConfig { get; }

        /// <summary>
        /// Cluster config for the the current request.
        /// </summary>
        IClusterConfig ClusterConfig { get; }

        /// <summary>
        /// Cluster destinations that can handle the current request.
        /// </summary>
        IReadOnlyList<IDestination> AvailableDestinations { get; set; }
    }

    public interface IDestination : IReadOnlyList<IDestination>
    {
        string DestinationId { get; }

        string Address { get; }

        int PendingRequestCount { get; }

        void BeginProxyRequest();

        void EndProxyRequest();
    }

    public interface IClusterConfig
    {
        string ClusterId { get; }

        ClusterConfig Value { get; }

        IProxyHttpClientFactory ProxyHttpClientFactory { get; }

        void BeginProxyRequest();

        void EndProxyRequest();
    }

    public interface IRouteConfig
    {
        string RouteId { get; }

        Transforms Transforms { get; }
    }
}
