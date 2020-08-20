// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using Microsoft.ReverseProxy.Utilities;

namespace Microsoft.ReverseProxy.RuntimeModel
{
    /// <summary>
    /// Immutable representation of the portions of a destination
    /// that only change in reaction to configuration changes
    /// (e.g. address).
    /// </summary>
    /// <remarks>
    /// All members must remain immutable to avoid thread safety issues.
    /// Instead, instances of <see cref="DestinationConfig"/> are replaced
    /// in ther entirety when values need to change.
    /// </remarks>
    public sealed class DestinationConfig
    {
        public DestinationConfig(string destinationId, string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                throw new ArgumentNullException(nameof(address));
            }

            DestinationId = destinationId;
            Address = address;
        }

        public string DestinationId { get; }

        // TODO: Make this a Uri.
        public string Address { get; }
    }
}
