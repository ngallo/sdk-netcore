namespace Permguard;

public class AzEndpoint
{
    public string Schema { get; set; }
    public int Port { get; set; }
    public string Endpoint { get; set; }

    public AzEndpoint(string schema, int port, string endpoint)
    {
        this.Schema = schema;
        this.Port = port;
        this.Endpoint = endpoint;
    }
}

public class AzConfig
{
    public AzEndpoint Endpoint { get; set; }
    
    public AzConfig WithEndpoint(AzEndpoint endpoint )
    {
        this.Endpoint = endpoint;
        return this;
    }
}