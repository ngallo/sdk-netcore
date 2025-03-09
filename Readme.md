# Permguard DotNet Core SDK

[![GitHub License](https://img.shields.io/github/license/permguard/permguard-go)](https://github.com/permguard/permguard-go?tab=Apache-2.0-1-ov-file#readme)
[![X (formerly Twitter) Follow](https://img.shields.io/twitter/follow/permguard)](https://x.com/intent/follow?original_referer=https%3A%2F%2Fdeveloper.x.com%2F&ref_src=twsrc%5Etfw%7Ctwcamp%5Ebuttonembed%7Ctwterm%5Efollow%7Ctwgr%5ETwitterDev&screen_name=Permguard)

[![Documentation](https://img.shields.io/website?label=Docs&url=https%3A%2F%2Fwww.permguard.com%2F)](https://www.permguard.com/)
[![Build, test and publish the artifacts](https://github.com/permguard/permguard-go/actions/workflows/permguard-go-ci.yml/badge.svg)](https://github.com/permguard/permguard-go/actions/workflows/permguard-go-ci.yml)

<p align="left">
  <img src="https://raw.githubusercontent.com/permguard/permguard-assets/main/pink-txt//1line.svg" class="center" width="400px" height="auto"/>
</p>

The Permguard DotNet Core SDK provides a simple and flexible client to perform authorization checks against a Permguard Policy Decision Point (PDP) service using gRPC.
Plase refer to the [Permguard Documentation](https://www.permguard.com/) for more information.

---

## Prerequisites

- **Net Core 8**

---

## Installation

## 📦 Install NuGet Packages

1. [Using Visual Studio (GUI)](https://learn.microsoft.com/en-us/nuget/consume-packages/install-use-packages-visual-studio)
2. [Using NuGet CLI (Command Line)](https://learn.microsoft.com/en-us/nuget/install-nuget-client-tools)
3. [Using .NET CLI (for .NET Core/.NET 5+)](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-add-package)

#### nuget install Permguard
#### or
#### dotnet add package Permguard

---

## Usage Example

Below is a sample Go code demonstrating how to create a Permguard client, build an authorization request using a builder pattern, and process the authorization response:

```csharp
using System.Diagnostics;
using Permguard;
using Permguard.AzReq;

//Create client
PdpClient client=new PdpClient("http://localhost:9094");
//Create request
var builder = new AZRequestBuilder(742774374902, "3cdfd72ea6404624aeabf7b7bf043d31").WithRequestID("123457");
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
    .WithResource(new ResourceBuilder("MagicFarmacia::Platform::Subscription").WithID("e3a786fd07e24bfa95ba4341d3695ae8").WithProperty("isEnabled", true).Build())
    .WithEntitiesMap("cedar", listEntities).WithContext(context);
try
{
    //Build request
    var request = builder.Build();
    //Used to che cexecution time
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
```

---

## Version Compatibility

Our SDK follows a versioning scheme aligned with the PermGuard server versions to ensure seamless integration. The versioning format is as follows:

**SDK Versioning Format:** `x.y.z`

- **x.y**: Indicates the compatible PermGuard server version.
- **z**: Represents the SDK's patch or minor updates specific to that server version.

**Compatibility Examples:**

- `SDK Version 1.3.0` is compatible with `PermGuard Server 1.3`.
- `SDK Version 1.3.1` includes minor improvements or bug fixes for `PermGuard Server 1.3`.

**Incompatibility Example:**

- `SDK Version 1.3.0` **may not be guaranteed** to be compatible with `PermGuard Server 1.4` due to potential changes introduced in server version `1.4`.

**Important:** Ensure that the major and minor versions (`x.y`) of the SDK match those of your PermGuard server to maintain compatibility.

---

Created by [Nitro Agility](https://www.nitroagility.com/).