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
    // ActionBuilder is the builder for the action object.
    public class ActionBuilder: Builder
    {
        private readonly Action action;

        // Constructor to initialize ActionBuilder with a name.
        public ActionBuilder(string name)
        {
            action = new Action
            {
                Name = name
            };
        }

        // WithProperty sets a property of the action.
        public ActionBuilder WithProperty(string key, object value)
        {
            action.Properties ??= new Dictionary<string, object>();
            action.Properties[key] = value;
            return this;
        }

        // Build constructs and returns the final Action object.
        public Action? Build()
        {
            var instance = new Action
            {
                Name = action.Name,
                Properties = DeepCopy(action.Properties)
            };
            return instance;
        }
    }
}
