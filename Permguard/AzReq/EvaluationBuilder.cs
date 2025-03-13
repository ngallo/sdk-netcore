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
        private Evaluation azEvaluation;
        
        public EvaluationBuilder(Subject subject, Resource resource, Action action)
        {
            azEvaluation = new Evaluation()
            {
                Subject = subject,
                Resource = resource,
                Action = action
            };
        }

        // WithRequestId sets the request Id of the Evaluation.
        public EvaluationBuilder WithRequestId(string requestId)
        {
            azEvaluation.RequestId = requestId;
            return this;
        }

        // WithContext sets the context of the Evaluation.
        public EvaluationBuilder WithContext(Dictionary<string, object>? context)
        {
            azEvaluation.Context = context;
            return this;
        }

        // Build constructs and returns the final Evaluation object.
        public Evaluation Build()
        {
            var instance = new Evaluation
            {
                Subject = azEvaluation.Subject,
                Resource = azEvaluation.Resource,
                Action = azEvaluation.Action,
                RequestId = azEvaluation.RequestId,
                Context = DeepCopy(azEvaluation.Context)
            };
            return instance;
        }

        // Helper method to deep copy the context dictionary.
        private Dictionary<string, object>? DeepCopy(Dictionary<string, object>? source)
        {
            if (source == null)
            {
                return null;
            }

            var copy = new Dictionary<string, object>();
            foreach (var kvp in source)
            {
                copy[kvp.Key] = Value.ForString(kvp.Value.ToString()); // Assumes the values are primitives or deep copies themselves.
            }
            return copy;
        }
    }
}
