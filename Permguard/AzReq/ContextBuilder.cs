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

namespace Permguard.AzReq
{
    // ContextBuilder is the builder for the context object.
    public class ContextBuilder
    {
        private Dictionary<string, object> context;

        // Constructor to initialize ContextBuilder.
        public ContextBuilder()
        {
            context = new Dictionary<string, object>();
        }

        // WithProperty sets a property of the context.
        public ContextBuilder WithProperty(string key, object value)
        {
            if (context == null)
            {
                context = new Dictionary<string, object>();
            }
            context[key] = value;
            return this;
        }

        // Build constructs and returns the context object (as a Dictionary).
        public Dictionary<string, object>? Build()
        {
            var instance = DeepCopy(context);
            return instance;
        }

        // Helper method to deep copy the dictionary.
        private Dictionary<string, object>? DeepCopy(Dictionary<string, object> source)
        {
            var copy = new Dictionary<string, object>();
            foreach (var kvp in source)
            {
                copy[kvp.Key] = kvp.Value; // Assumes the values are primitives or deep copies themselves.
            }
            return copy;
        }
    }
}