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
    // ResourceBuilder is the builder for the resource object.
    public class ResourceBuilder: Builder
    {
        private readonly Resource resource;

        // Constructor to initialize ResourceBuilder with a type (kind).
        public ResourceBuilder(string kind)
        {
            resource = new Resource
            {
                Type = kind,
                Properties = new Dictionary<string, object>()
            };
        }

        // WithId sets the id of the resource.
        public ResourceBuilder WithId(string id)
        {
            resource.Id = id;
            return this;
        }

        // WithProperty sets a property of the resource.
        public ResourceBuilder WithProperty(string key, object value)
        {
            if (resource.Properties != null) resource.Properties[key] = value;
            return this;
        }

        // Build constructs and returns the final Resource object.
        public Resource Build()
        {
            var instance = new Resource
            {
                Id = resource.Id,
                Type = resource.Type,
                Properties = DeepCopy(resource.Properties)
            };
            return instance;
        }
    }
}
