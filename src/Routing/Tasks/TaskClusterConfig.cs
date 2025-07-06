using Yarp.ReverseProxy.Configuration;

namespace Routing.Tasks;

public static class TaskClusterConfig
{
    public static ClusterConfig[] GetClusters() => [
        new ClusterConfig
        {
            ClusterId = "task-cluster",
            Destinations = new Dictionary<string, DestinationConfig>
            {
                { "task-service", new DestinationConfig { Address = "http://task-service:8080" } }
            }
        },
    ];
}