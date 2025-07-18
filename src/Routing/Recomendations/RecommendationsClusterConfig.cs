using Yarp.ReverseProxy.Configuration;

namespace Routing.Recomendations;

public static class RecommendationsClusterConfig
{
    public static ClusterConfig[] GetClusters() => [
        new ClusterConfig
        {
            ClusterId = "recommendations-cluster",
            Destinations = new Dictionary<string, DestinationConfig>
            {
                ["recommendations-service"] = new (){ Address = "http://recommendations-service:8080" }
            }
        },
    ];
}