using Permguard;
using Permguard.AzReq;

AzClient client = new AzClient(new AzConfig().WithEndpoint(new AzEndpoint("http", 9094, "localhost")));
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
var builder = new AZAtomicRequestBuilder(882005116936, 
    "711299b99c12416396d674c0ec371f1d",
    "platform-creator",
    "MagicFarmacia::Platform::Subscription",
    "MagicFarmacia::Platform::Action::create")
    .WithRequestID("31243")
    .WithPrincipal(new PrincipalBuilder("amy.smith@acmecorp.com").WithSource("keycloak").WithKind("user").Build())
    .WithSubjectKind("role-actor").WithSubjectSource("keycloak").WithSubjectProperty("isSuperUser", true)
    .WithResourceID("e3a786fd07e24bfa95ba4341d3695ae8").WithResourceProperty("isEnabled", true).WithActionProperty("isEnabled", true)
    .WithEntitiesMap("cedar", listEntities).WithContextProperty("isSubscriptionActive", true).WithContextProperty("time", "2025-01-23T16:17:46+00:00");
try
{
    var request = builder.Build();
    client.LogJsonRequest = true;
    var response = client.CheckAuth(request);
    Console.WriteLine(response);
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}