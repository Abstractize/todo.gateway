using Yarp.ReverseProxy.Configuration;

namespace Routing.Auth;

public static class AuthClusterConfig
{
    public static ClusterConfig[] GetClusters() => [
        new ClusterConfig
        {
            ClusterId = "auth-cluster",
            Destinations = new Dictionary<string, DestinationConfig>
            {
                ["auth-service"] = new() { Address = "http://auth-service:8080" }
            }
        }
    ];
}