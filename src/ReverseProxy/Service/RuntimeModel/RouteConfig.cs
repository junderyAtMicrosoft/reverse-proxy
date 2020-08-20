// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using Microsoft.ReverseProxy.Abstractions;
using Microsoft.ReverseProxy.Middleware;
using Microsoft.ReverseProxy.Service.RuntimeModel.Transforms;

namespace Microsoft.ReverseProxy.RuntimeModel
{
    /// <summary>
    /// Immutable representation of the portions of a route
    /// that only change in reaction to configuration changes
    /// (e.g. rule, priority, action, etc.).
    /// </summary>
    /// <remarks>
    /// All members must remain immutable to avoid thread safety issues.
    /// Instead, instances of <see cref="RouteConfig"/> are replaced
    /// in their entirety when values need to change.
    /// </remarks>
    internal sealed class RouteConfig : IRouteConfig
    {
        public RouteConfig(
            string routeId,
            int configHash,
            int? priority,
            ClusterInfo cluster,
            IReadOnlyList<AspNetCore.Http.Endpoint> aspNetCoreEndpoints,
            Transforms transforms)
        {
            Endpoints = aspNetCoreEndpoints ?? throw new ArgumentNullException(nameof(aspNetCoreEndpoints));

            RouteId = routeId;
            ConfigHash = configHash;
            Priority = priority;
            Cluster = cluster;
            Transforms = transforms;
        }

        public string RouteId { get; }

        internal int ConfigHash { get; }

        public int? Priority { get; }

        // May not be populated if the cluster config is missing.
        internal ClusterInfo Cluster { get; }

        internal IReadOnlyList<AspNetCore.Http.Endpoint> Endpoints { get; }

        public Transforms Transforms { get; }

        public bool HasConfigChanged(ProxyRoute newConfig, ClusterInfo cluster)
        {
            return Cluster != cluster
                || !ConfigHash.Equals(newConfig.GetConfigHash());
        }
    }
}
