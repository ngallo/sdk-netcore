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

using System.Text.Json.Serialization;

namespace Permguard.AzReq
{
    public class PolicyStore
    {
        [JsonPropertyName("kind")]
        public string Kind { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }
    }

    public class Entities
    {
        [JsonPropertyName("schema")]
        public string Schema { get; set; }

        [JsonPropertyName("items")]
        public List<Dictionary<string, object>?> Items { get; set; }
    }

    public class Evaluation
    {
        [JsonPropertyName("request_id")]
        public string RequestId { get; set; }

        [JsonPropertyName("subject")]
        public Subject? Subject { get; set; }

        [JsonPropertyName("resource")]
        public Resource? Resource { get; set; }

        [JsonPropertyName("action")]
        public Action? Action { get; set; }

        [JsonPropertyName("context")]
        public Dictionary<string, object>? Context { get; set; }
    }

    public class AZModel
    {
        [JsonPropertyName("zone_id")]
        public long ZoneId { get; set; }

        [JsonPropertyName("principal")]
        public Principal? Principal { get; set; }

        [JsonPropertyName("policy_store")]
        public PolicyStore? PolicyStore { get; set; }

        [JsonPropertyName("entities")]
        public Entities? Entities { get; set; }
    }

    public class AZRequest
    {
        [JsonPropertyName("authorization_model")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public AZModel AuthorizationModel { get; set; }

        [JsonPropertyName("request_id")]
        public string? RequestId { get; set; }

        [JsonPropertyName("subject")]
        public Subject? Subject { get; set; }

        [JsonPropertyName("resource")]
        public Resource? Resource { get; set; }

        [JsonPropertyName("action")]
        public Action? Action { get; set; }

        [JsonPropertyName("context")]
        public Dictionary<string, object>? Context { get; set; }

        [JsonPropertyName("evaluations")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<Evaluation> Evaluations { get; set; }
    }

    public class Principal
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }
    }

    public class Subject
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }

        [JsonPropertyName("properties")]
        public Dictionary<string, object>? Properties { get; set; }
    }

    public class Resource
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("properties")]
        public Dictionary<string, object>? Properties { get; set; }
    }

    public class Action
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("properties")]
        public Dictionary<string, object>? Properties { get; set; }
    }

    public class ReasonResponse
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }

    public class ContextResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("reason_admin")]
        public ReasonResponse? ReasonAdmin { get; set; }

        [JsonPropertyName("reason_user")]
        public ReasonResponse? ReasonUser { get; set; }
    }

    public class EvaluationResponse
    {
        [JsonPropertyName("request_id")]
        public string RequestId { get; set; }

        [JsonPropertyName("decision")]
        public bool Decision { get; set; }

        [JsonPropertyName("context")]
        public ContextResponse? Context { get; set; }
    }

    public class AZResponse
    {
        [JsonPropertyName("request_id")]
        public string RequestId { get; set; }

        [JsonPropertyName("decision")]
        public bool Decision { get; set; }

        [JsonPropertyName("context")]
        public ContextResponse? Context { get; set; }

        [JsonPropertyName("evaluations")]
        public List<EvaluationResponse> Evaluations { get; set; }
    }
}
