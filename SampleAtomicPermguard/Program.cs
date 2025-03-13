using Permguard;
using Permguard.AzReq;

try
{
    // Create a new Permguard client
    var client = new AzClient(new AzConfig().WithEndpoint(new AzEndpoint("http", 9094, "localhost")));
    
    // Create the Principal
    var principal = new PrincipalBuilder("amy.smith@acmecorp.com")
        .WithSource("keycloak")
        .WithKind("user")
        .Build();

    // Create the entities
    var entities = new List<Dictionary<string, object>?>
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
   
    // Create a new authorization request
    var request = new AzAtomicRequestBuilder(285374414806,
            "f81aec177f8a44a48b7ceee45e05507f",
            "platform-creator",
            "MagicFarmacia::Platform::Subscription",
            "MagicFarmacia::Platform::Action::creat4")
        // RequestID
        .WithRequestId("31243")
        // Principal
        .WithPrincipal(principal)
        // Entities
        .WithEntitiesMap("cedar", entities)
        // Subject
        .WithSubjectKind("role-actor")
        .WithSubjectSource("keycloak")
        .WithSubjectProperty("isSuperUser", true)
        // Resource
        .WithResourceId("e3a786fd07e24bfa95ba4341d3695ae8")
        .WithResourceProperty("isEnabled", true)
        // Action
        .WithActionProperty("isEnabled", true)
        // Context
        .WithContextProperty("isSubscriptionActive", true)
        .WithContextProperty("time", "2025-01-23T16:17:46+00:00")
        .Build();
    
    // Check the authorization
    var response = client.CheckAuth(request);
    if (response.Decision) {
        Console.WriteLine("✅ Authorization Permitted");
    }
    else
    {
        Console.WriteLine("❌ Authorization Denied");
        if (response.Context != null) {
            if (response.Context?.ReasonAdmin != null)
            {
                Console.WriteLine($"-> Reason Admin: {response.Context?.ReasonAdmin?.Message}");
            }
            if (response.Context?.ReasonUser != null)
            {
                Console.WriteLine($"-> Reason User: {response.Context?.ReasonUser?.Message}");
            }
        }
        foreach (var eval in response.Evaluations)
        {
            if (eval.Decision)
            {
                Console.WriteLine("-> ✅ Authorization Permitted");
            }
            if (eval.Context != null) {
                if (eval.Context?.ReasonAdmin != null)
                {
                    Console.WriteLine($"-> Reason Admin: {eval.Context?.ReasonAdmin?.Message}");
                }
                if (eval.Context?.ReasonUser != null)
                {
                    Console.WriteLine($"-> Reason User: {eval.Context?.ReasonUser?.Message}");
                }
            }
        }
    }
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}