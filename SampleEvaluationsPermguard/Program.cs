using Permguard;
using Permguard.AzReq;

AzClient client = new AzClient(new AzConfig().WithEndpoint(new AzEndpoint("http", 9094, "localhost")));
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
var context = new Dictionary<string, object>();
context.Add("time", "2025-01-23T16:17:46+00:00");
context.Add("isSubscriptionActive", true);
Evaluation evaluation=new Evaluation();
evaluation.RequestId = "134";
builder.WithSubject(new SubjectBuilder("platform-creator")
    .WithSource("keycloak")
    .WithKind("role-actor")
    .WithProperty("isSuperUser", true)
    .Build());
evaluation.Resource = new ResourceBuilder("MagicFarmacia::Platform::Subscription")
        .WithId("e3a786fd07e24bfa95ba4341d3695ae8")
        .WithProperty("isEnabled", true)
        .Build();
evaluation.Action = new ActionBuilder("MagicFarmacia::Platform::Action::create")
    .WithProperty("isEnabled", true)
    .Build();
Evaluation evaluation2=new Evaluation();
evaluation2.RequestId = "435";
evaluation2.Resource = new ResourceBuilder("MagicFarmacia::Platform::Subscription")
    .WithId("e3a786fd07e24bfa95ba4341d3695ae8")
    .WithProperty("isEnabled", true)
    .Build();
evaluation2.Action = new ActionBuilder("MagicFarmacia::Platform::Action::create")
    .WithProperty("isEnabled", false)
    .Build();
builder.WithPrincipal(new PrincipalBuilder("amy.smith@acmecorp.com")
        .WithSource("keycloak")
        .WithKind("user").Build())
    .WithEntitiesMap("cedar", listEntities)
    .WithContext(context).
    WithEvaluation(evaluation)
    .WithEvaluation(evaluation2);
try
{
    var request = builder.Build();
    var response = client.CheckAuth(request);
    Console.WriteLine(response);
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}