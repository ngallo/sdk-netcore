// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using Permguard;
using Permguard.AzReq;

var principal = new PrincipalBuilder("admin").WithSource("netcore").Build();
        
var client = new AzClient(new AzConfig().
    WithEndpoint(new AzEndpoint("http", 9094, "127.0.0.1")));

List<string> roles = new List<string> { "admininistrators", "editors" };
        
var request = new AzAtomicRequestBuilder(981332208784,
        "a1072aac70c3439dbe015fb36fd24750",
        "platform-viewer-simple",
        "Resource::bucket",
        "Action::list")
    .WithRequestId(Guid.NewGuid().ToString())
    // Principal
    .WithPrincipal(principal)
    // Entities
    .WithEntitiesItems("cedar", new List<Dictionary<string, object>?>())
    // Subject
    .WithSubjectType("workload")
    .WithSubjectSource("netcore")
    .WithSubjectProperty("groups", roles)
    // Resource
    .WithResourceId("12345")
    .Build();
        
var options = new JsonSerializerOptions { Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping, WriteIndented = true };
string jsonString = JsonSerializer.Serialize(request, options);
Console.WriteLine(jsonString);

var response = client.CheckAuth(request);
System.Console.WriteLine(response?.Decision);
Console.WriteLine("Hello, World!");