using System.Text.Encodings.Web;
using System.Text.Json;
using Grpc.Net.Client;

namespace Permguard;

public class AzClient
{
    public AzConfig Config { get; set; }
    
    public bool LogJsonRequest { get; set; }
    
    public AzClient(AzConfig config)
    {
        this.Config = config;
    }

    public AZResponse CheckAuth(AZRequest request)
    {
        if (this.Config == null)
        {
            throw new NullReferenceException("Please provide config");
        }
        string urlString = $"{this.Config.Endpoint.Schema}://{this.Config.Endpoint.Endpoint}:{this.Config.Endpoint.Port}";
        using var channel = GrpcChannel.ForAddress(urlString);
        Policydecisionpoint.V1PDPService.V1PDPServiceClient client = new(channel);
        var destination = MapService.MapAZRequestToGrpcAuthorizationCheckRequest(request);
        if (this.LogJsonRequest)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            string json = JsonSerializer.Serialize(request, options);
            Console.WriteLine(json);
        }
        var result = client.AuthorizationCheck(destination);
        return MapService.MapGrpcAuthorizationCheckResponseToAZResponse(result);
    }
}