using System.Diagnostics;
using Permguard;
using Permguard.AzReq;

//Create client
AzClient client = new AzClient(new AzConfig().WithEndpoint(new AzEndpoint("http", 9094, "localhost")));
//Create request
var builder = new AZRequestBuilder(882005116936, "711299b99c12416396d674c0ec371f1d").WithRequestId("123457");
var listEntities = new List<Dictionary<string, object>>();
var entityProps = new Dictionary<string,object>();
var uidDict = new Dictionary<string,object>();
uidDict.Add("type", "MagicFarmacia::Platform::BranchInfo");
uidDict.Add("id", "subscription");
entityProps.Add("uid", uidDict);
var attrs = new Dictionary<string,object>();
attrs.Add("active", true);
entityProps.Add("attrs", attrs);
entityProps.Add("parents", new List<object>());
listEntities.Add(entityProps);
//Create context
var context = new Dictionary<string, object>();
context.Add("time", "2025-01-23T16:17:46+00:00");
context.Add("isSubscriptionActive", true);
//Create builder with Principal, Action, Subject, Resource and Entities
builder.WithPrincipal(new PrincipalBuilder("amy.smith@acmecorp.com").WithSource("keycloak").WithKind("user").Build())
    .WithAction(new ActionBuilder("MagicFarmacia::Platform::Action::create").WithProperty("isEnabled", true).Build())
    .WithSubject(new SubjectBuilder("platform-creator").WithSource("keycloak").WithKind("role-actor").WithProperty("isSuperUser", true).Build())
    .WithResource(new ResourceBuilder("MagicFarmacia::Platform::Subscription").WithId("e3a786fd07e24bfa95ba4341d3695ae8").WithProperty("isEnabled", true).Build())
    .WithEntitiesMap("cedar", listEntities).WithContext(context);
try
{
    //Build request
    var request = builder.Build();
    //Used to che execution time
    Stopwatch stopwatch = new Stopwatch();
    var response1 = client.CheckAuth(request);
    stopwatch.Start();
    var response = client.CheckAuth(request);
    stopwatch.Stop();
    Console.WriteLine($"Time taken: {stopwatch.ElapsedMilliseconds} ms");
    Console.WriteLine(response);
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}
