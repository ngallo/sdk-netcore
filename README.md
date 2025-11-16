# The official .NET Core SDK for Permguard

[![GitHub License](https://img.shields.io/github/license/permguard/sdk-netcore)](https://github.com/permguard/sdk-netcore?tab=Apache-2.0-1-ov-file#readme)
[![X (formerly Twitter) Follow](https://img.shields.io/twitter/follow/permguard)](https://x.com/intent/follow?original_referer=https%3A%2F%2Fdeveloper.x.com%2F&ref_src=twsrc%5Etfw%7Ctwcamp%5Ebuttonembed%7Ctwterm%5Efollow%7Ctwgr%5ETwitterDev&screen_name=Permguard)

[![Documentation](https://img.shields.io/website?label=Docs&url=https%3A%2F%2Fwww.permguard.com%2F)](https://www.permguard.com/)
[![Build, test and publish the artifacts](https://github.com/permguard/sdk-netcore/actions/workflows/sdk-netcore-ci.yml/badge.svg)](https://github.com/permguard/sdk-netcore/actions/workflows/sdk-netcore-ci.yml)

[![Watch the video on YouTube](https://raw.githubusercontent.com/permguard/permguard-assets/refs/heads/main/video/permguard-thumbnail-preview.png)](https://youtu.be/cH_boKCpLQ8?si=i1fWFHT5kxQQJoYN)

[Watch the video on YouTube](https://youtu.be/cH_boKCpLQ8?si=i1fWFHT5kxQQJoYN)


The Permguard DotNet Core SDK provides a simple and flexible client to perform authorization checks against a Permguard Policy Decision Point (PDP) service using gRPC.
Plase refer to the [Permguard Documentation](https://www.permguard.com/) for more information.

---

## Prerequisites

- **Net Core 8**

---

## 📦 Install NuGet Packages

```bash
dotnet add package Permguard
```

---

## Usage Example

Below is a sample Go code demonstrating how to create a Permguard client, build an authorization request using a builder pattern, and process the authorization response:

```csharp
using Permguard;
using Permguard.AzReq;

try
{
    // Create a new Permguard client
    var client = new AzClient(new AzConfig().WithEndpoint(new AzEndpoint("http", 9094, "localhost")));
    
    // Create the Principal
    var principal = new PrincipalBuilder("amy.smith@acmecorp.com")
        .WithSource("keycloak")
        .WithType("user")
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
            "MagicFarmacia::Platform::Action::create")
        // RequestID
        .WithRequestId("31243")
        // Principal
        .WithPrincipal(principal)
        // Entities
        .WithEntitiesMap("cedar", entities)
        // Subject
        .WithSubjectType("workload")
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
    if (response == null)
    {
        Console.WriteLine("❌ Failed to check auth.");
        throw new Exception("Failed to check auth response");
    }
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
    Console.WriteLine("❌ Failed to check auth.");
    throw;
}
```

---

## Version Compatibility

Our SDK follows a versioning scheme aligned with the AuthZServer versions to ensure seamless integration. The versioning format is as follows:

**SDK Versioning Format:** `x.y.z`

- **x.y**: Indicates the compatible AuthZServer version.
- **z**: Represents the SDK's patch or minor updates specific to that server version.

**Compatibility Examples:**

- `SDK Version 1.3.0` is compatible with `AuthZServer 1.3`.
- `SDK Version 1.3.1` includes minor improvements or bug fixes for `AuthZServer 1.3`.

**Incompatibility Example:**

- `SDK Version 1.3.0` **may not be guaranteed** to be compatible with `AuthZServer 1.4` due to potential changes introduced in server version `1.4`.

**Important:** Ensure that the major and minor versions (`x.y`) of the SDK match those of your AuthZServer to maintain compatibility.

---

Created by [Nitro Agility](https://www.nitroagility.com/).