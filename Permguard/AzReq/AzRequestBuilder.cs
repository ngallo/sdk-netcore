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
using System;
using System.Collections.Generic;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Policydecisionpoint;
using Permguard;

namespace Permguard.AzReq
{
    public class AZRequestBuilder
    {
        private AZRequest azRequest;

        // Constructor to initialize AZRequestBuilder with provided values.
        public AZRequestBuilder(long zoneId, string ledgerId)
        {
            azRequest = new AZRequest
            {
                AuthorizationModel = new AZModel
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

        // WithPrincipal sets the principal of the AZRequest.
        public AZRequestBuilder WithPrincipal(Principal principal)
        {
            azRequest.AuthorizationModel.Principal = principal;
            return this;
        }

        // WithRequestId sets the request Id of the AZRequest.
        public AZRequestBuilder WithRequestId(string requestId)
        {
            azRequest.RequestId = requestId;
            return this;
        }

        // WithSubject sets the subject of the AZRequest.
        public AZRequestBuilder WithSubject(Subject subject)
        {
            azRequest.Subject = subject;
            return this;
        }

        // WithResource sets the resource of the AZRequest.
        public AZRequestBuilder WithResource(Resource resource)
        {
            azRequest.Resource = resource;
            return this;
        }

        // WithAction sets the action of the AZRequest.
        public AZRequestBuilder WithAction(Action action)
        {
            azRequest.Action = action;
            return this;
        }

        // WithContext sets the context of the Evaluation.
        public AZRequestBuilder WithContext(Dictionary<string, object> context)
        {
            azRequest.Context = context;
            return this;
        }

        // WithEntitiesMap sets the entities map to the AZRequest.
        public AZRequestBuilder WithEntitiesMap(string schema, List<Dictionary<string, object>> entities)
        {
            azRequest.AuthorizationModel.Entities.Schema = schema;
            azRequest.AuthorizationModel.Entities.Items = new List<Dictionary<string, object>>();
            foreach (var entity in entities)
            {
                azRequest.AuthorizationModel.Entities.Items.Add(entity);
            }
            return this;
        }

        // WithEntitiesItems sets the entities items to the AZRequest.
        public AZRequestBuilder WithEntitiesItems(string schema, List<Dictionary<string, object>> entities)
        {
            azRequest.AuthorizationModel.Entities.Schema = schema;
            azRequest.AuthorizationModel.Entities.Items = entities ?? new List<Dictionary<string, object>>();
            return this;
        }

        // WithEvaluation adds an evaluation to the AZRequest.
        public AZRequestBuilder WithEvaluation(Evaluation evaluation)
        {
            azRequest.Evaluations.Add(evaluation);
            return this;
        }

        // Build builds the AZRequest object.
        public AZRequest Build()
        {
            return azRequest;
        }
    }
}
