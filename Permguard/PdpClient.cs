using System.Text.Encodings.Web;
using System.Text.Json;
using Grpc.Net.Client;

namespace Permguard;

public class PdpClient
{
    public string Url { get; set; }
    
    public bool LogJsonRequest { get; set; }
    
    public PdpClient(string url)
    {
        this.Url = url;
    }

    public AZResponse CheckAuth(AZRequest request)
    {
        if (string.IsNullOrEmpty(this.Url))
        {
            throw new NullReferenceException("Please provide url");
        }
        using var channel = GrpcChannel.ForAddress(this.Url);
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