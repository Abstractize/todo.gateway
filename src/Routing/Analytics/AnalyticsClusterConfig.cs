using Yarp.ReverseProxy.Configuration;

namespace Routing.Analytics;

public static class RecommendationsClusterConfig
{
    public static ClusterConfig[] GetClusters() => [
        new ClusterConfig
        {
            ClusterId = "analytics-cluster",
            Destinations = new Dictionary<string, DestinationConfig>
            {
                ["analytics-service"] = new (){ Address = "http://analytics-service:8080" }
            }
        },
    ];
}