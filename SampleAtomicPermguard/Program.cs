using Permguard;
using Permguard.AzReq;

try
{
    var config = new AzConfig().WithEndpoint(new AzEndpoint("http", 9094, "localhost"));
    var client = new AzClient(config);

    var listEntities = new List<Dictionary<string, object>>
    {
        new()
        {
            { "uid", new Dictionary<string,object>
                {
                    { "type", "MagicFarmacia::Platform::BranchInfo" },
                    { "id", "subscription" }
                }
            },
            { "attrs", new Dictionary<string, object> { { "active", true } } },
            { "parents", new List<object>() }
        }
    };
    var request = new AZAtomicRequestBuilder(285374414806,
            "f81aec177f8a44a48b7ceee45e05507f",
            "platform-creator",
            "MagicFarmacia::Platform::Subscription",
            "MagicFarmacia::Platform::Action::creat4")
        .WithRequestId("31243")
        .WithPrincipal(new PrincipalBuilder("amy.smith@acmecorp.com").WithSource("keycloak").WithKind("user").Build())
        .WithSubjectKind("role-actor").WithSubjectSource("keycloak").WithSubjectProperty("isSuperUser", true)
        .WithResourceId("e3a786fd07e24bfa95ba4341d3695ae8").WithResourceProperty("isEnabled", true).WithActionProperty("isEnabled", true)
        .WithEntitiesMap("cedar", listEntities).WithContextProperty("isSubscriptionActive", true).WithContextProperty("time", "2025-01-23T16:17:46+00:00").Build();
    var response = client.CheckAuth(request);
    if (response.Decision) {
        Console.WriteLine("✅ Authorization Permitted");
    }
    else
    {
        Console.WriteLine("❌ Authorization Denied");
        Console.WriteLine($"-> Reason Admin: {response.Context.ReasonAdmin.Message}");
        Console.WriteLine($"-> Reason User: {response.Context.ReasonUser.Message}");
        foreach (var eval in response.Evaluations)
        {
            Console.WriteLine($"-> Reason Admin: {eval.Context.ReasonAdmin.Message}");
            Console.WriteLine($"-> Reason User: {eval.Context.ReasonUser.Message}");
        }
    }
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}