using Grpc.Net.Client;
using Permguard.AzReq;
using Permguard.Grpc;

namespace Permguard;

public class AzClient
{
    private readonly AzConfig config;
    
    public AzClient(AzConfig config)
    {
        this.config = config;
    }

    public AZResponse CheckAuth(AZRequest request)
    {
        if (config == null)
        {
            throw new NullReferenceException("Please provide config");
        }
        var urlString = $"{this.config.Endpoint.Schema}://{this.config.Endpoint.Endpoint}:{this.config.Endpoint.Port}";
        using var channel = GrpcChannel.ForAddress(urlString);
        Policydecisionpoint.V1PDPService.V1PDPServiceClient client = new(channel);
        var destination = MapService.MapAZRequestToGrpcAuthorizationCheckRequest(request);
        var result = client.AuthorizationCheck(destination);
        return MapService.MapGrpcAuthorizationCheckResponseToAZResponse(result);
    }
}