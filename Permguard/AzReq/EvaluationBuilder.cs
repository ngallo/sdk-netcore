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
using Google.Protobuf.WellKnownTypes;
using Policydecisionpoint;

namespace Permguard.AzReq
{
    // Evaluation class definition (assuming basic structure)
    

    // EvaluationBuilder is the builder for the Evaluation object.
    public class EvaluationBuilder
    {
        private EvaluationRequest azEvaluation;
        
        public EvaluationBuilder(Policydecisionpoint.Subject subject, Policydecisionpoint.Resource resource, Policydecisionpoint.Action action)
        {
            azEvaluation = new EvaluationRequest
            {
                Subject = subject,
                Resource = resource,
                Action = action
            };
        }

        // WithRequestID sets the request ID of the Evaluation.
        public EvaluationBuilder WithRequestID(string requestID)
        {
            azEvaluation.RequestID = requestID;
            return this;
        }

        // WithContext sets the context of the Evaluation.
        public EvaluationBuilder WithContext(Struct context)
        {
            azEvaluation.Context = context;
            return this;
        }

        // Build constructs and returns the final Evaluation object.
        public Policydecisionpoint.EvaluationRequest Build()
        {
            var instance = new EvaluationRequest
            {
                Subject = azEvaluation.Subject,
                Resource = azEvaluation.Resource,
                Action = azEvaluation.Action,
                RequestID = azEvaluation.RequestID,
                Context = DeepCopy(azEvaluation.Context)
            };
            return instance;
        }

        // Helper method to deep copy the context dictionary.
        private Struct DeepCopy(Struct source)
        {
            var copy = new Struct();
            foreach (var kvp in source.Fields)
            {
                copy.Fields[kvp.Key] = Value.ForString(kvp.Value.ToString()); // Assumes the values are primitives or deep copies themselves.
            }
            return copy;
        }
    }
}
