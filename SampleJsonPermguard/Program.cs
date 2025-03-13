// Copyright 2025 Nitro Agility S.r.l.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// SPDX-License-Identifier: Apache-2.0

using System.Text.Json;
using Permguard;
using Permguard.AzReq;

try
{
    // Create a new Permguard client
    var client = new AzClient(new AzConfig().WithEndpoint(new AzEndpoint("http", 9094, "localhost")));
    
    // Load the json file
    var filePath = Path.Combine(AppContext.BaseDirectory, "../../../ok_onlyone1.json");
    if (!File.Exists(filePath))
    {
        Console.WriteLine("❌ Failed to load the json file");
        throw new Exception("Failed to load the json file");
    }
    var jsonContent = File.ReadAllText(filePath);
    var request = JsonSerializer.Deserialize<AzRequest>(jsonContent);
    
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