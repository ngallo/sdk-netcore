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

namespace Permguard.AzReq
{
    public class AzRequestBuilder: Builder
    {
        private readonly AzRequest? azRequest;

        // Constructor to initialize AzRequestBuilder with provided values.
        public AzRequestBuilder(long zoneId, string ledgerId)
        {
            azRequest = new AzRequest
            {
                AuthorizationModel = new AzModel
                {
                    ZoneId = zoneId,
                    PolicyStore = new PolicyStore
                    {
                        Kind = "ledger", 
                        Id = ledgerId
                    },
                    Entities = new Entities
                    {
                        Schema = "",
                        Items = new List<Dictionary<string, object>>()
                    }
                },
                Evaluations = new List<Evaluation>()
            };
        }

        // WithPrincipal sets the principal of the AzRequest.
        public AzRequestBuilder WithPrincipal(Principal? principal)
        {
            if (azRequest != null) azRequest.AuthorizationModel.Principal = principal;
            return this;
        }

        // WithRequestId sets the request Id of the AzRequest.
        public AzRequestBuilder WithRequestId(string? requestId)
        {
            if (azRequest != null) azRequest.RequestId = requestId;
            return this;
        }

        // WithSubject sets the subject of the AzRequest.
        public AzRequestBuilder WithSubject(Subject? subject)
        {
            if (azRequest != null) azRequest.Subject = subject;
            return this;
        }

        // WithResource sets the resource of the AzRequest.
        public AzRequestBuilder WithResource(Resource? resource)
        {
            if (azRequest != null) azRequest.Resource = resource;
            return this;
        }

        // WithAction sets the action of the AzRequest.
        public AzRequestBuilder WithAction(Action? action)
        {
            if (azRequest != null) azRequest.Action = action;
            return this;
        }

        // WithContext sets the context of the Evaluation.
        public AzRequestBuilder WithContext(Dictionary<string, object>? context)
        {
            if (azRequest != null) azRequest.Context = context;
            return this;
        }

        // WithEntitiesMap sets the entities map to the AzRequest.
        public AzRequestBuilder WithEntitiesMap(string schema, List<Dictionary<string, object>?> entities)
        {
            if (azRequest?.AuthorizationModel.Entities == null) return this;
            azRequest.AuthorizationModel.Entities.Schema = schema;
            azRequest.AuthorizationModel.Entities.Items = new List<Dictionary<string, object>?>();
            foreach (var entity in entities)
            {
                azRequest.AuthorizationModel.Entities.Items.Add(entity);
            }
            return this;
        }

        // WithEntitiesItems sets the entities items to the AzRequest.
        public AzRequestBuilder WithEntitiesItems(string schema, List<Dictionary<string, object>?>? entities)
        {
            if (azRequest?.AuthorizationModel.Entities == null) return this;
            azRequest.AuthorizationModel.Entities.Schema = schema;
            azRequest.AuthorizationModel.Entities.Items = entities ?? new List<Dictionary<string, object>?>();
            return this;
        }

        // WithEvaluation adds an evaluation to the AzRequest.
        public AzRequestBuilder WithEvaluation(Evaluation evaluation)
        {
            azRequest?.Evaluations.Add(evaluation);
            return this;
        }

        // Build builds the AzRequest object.
        public AzRequest? Build()
        {
            return azRequest;
        }
    }
}
