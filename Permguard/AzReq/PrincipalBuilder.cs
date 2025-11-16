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
    public class PrincipalBuilder: Builder
    {
        private readonly Principal principal;

        // Constructor to initialize PrincipalBuilder with an Id.
        public PrincipalBuilder(string id)
        {
            principal = new Principal
            {
                Id = id,
                Type = "user" // Default type (PrincipalDefaultKind)
            };
        }

        // WithType sets the kind of the principal.
        public PrincipalBuilder WithType(string kind)
        {
            principal.Type = kind;
            return this;
        }

        // WithSource sets the source of the principal.
        public PrincipalBuilder WithSource(string source)
        {
            principal.Source = source;
            return this;
        }

        // Build constructs and returns the final Principal object.
        public Principal? Build()
        {
            var instance = new Principal
            {
                Id = principal.Id,
                Type = principal.Type,
                Source = principal.Source
            };
            return instance;
        }
    }
}
