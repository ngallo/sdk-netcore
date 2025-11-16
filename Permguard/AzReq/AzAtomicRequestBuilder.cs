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
    // AzAtomicRequestBuilder is the builder for the AzAtomicRequest object.
    public class AzAtomicRequestBuilder: Builder
    {
        private string? requestId;
        private Principal? principal;
        private readonly SubjectBuilder azSubjectBuilder;
        private readonly ResourceBuilder azResourceBuilder;
        private readonly ActionBuilder azActionBuilder;
        private readonly ContextBuilder azContextBuilder;
        private readonly AzRequestBuilder azRequestBuilder;

        // Constructor
        public AzAtomicRequestBuilder(long zoneId, string ledgerId, string subjectId, string resourceKind,
            string actionName)
        {
            azRequestBuilder = new AzRequestBuilder(zoneId, ledgerId);
            azSubjectBuilder = new SubjectBuilder(subjectId);
            azResourceBuilder = new ResourceBuilder(resourceKind);
            azActionBuilder = new ActionBuilder(actionName);
            azContextBuilder = new ContextBuilder();
        }

        // WithEntitiesMap sets the entities map to the AzRequest.
        public AzAtomicRequestBuilder WithEntitiesMap(string schema, List<Dictionary<string, object>?> entities)
        {
            azRequestBuilder.WithEntitiesMap(schema, entities);
            return this;
        }

        // WithEntitiesItems sets the entities items to the AzRequest.
        public AzAtomicRequestBuilder WithEntitiesItems(string schema, List<Dictionary<string, object>?>? entities)
        {
            azRequestBuilder.WithEntitiesItems(schema, entities);
            return this;
        }

        // WithRequestId sets the Id of the AzRequest.
        public AzAtomicRequestBuilder WithRequestId(string requestId)
        {
            this.requestId = requestId;
            return this;
        }

        // WithPrincipal sets the principal of the AzRequest.
        public AzAtomicRequestBuilder WithPrincipal(Principal principal)
        {
            this.principal = principal;
            return this;
        }

        // WithSubjectType sets the kind of the subject for the AzRequest.
        public AzAtomicRequestBuilder WithSubjectType(string kind)
        {
            azSubjectBuilder.WithType(kind);
            return this;
        }

        // WithSubjectSource sets the source of the subject for the AzRequest.
        public AzAtomicRequestBuilder WithSubjectSource(string source)
        {
            azSubjectBuilder.WithSource(source);
            return this;
        }

        // WithSubjectProperty sets a property of the subject for the AzRequest.
        public AzAtomicRequestBuilder WithSubjectProperty(string key, object value)
        {
            azSubjectBuilder.WithProperty(key, value);
            return this;
        }

        // WithResourceId sets the Id of the resource for the AzRequest.
        public AzAtomicRequestBuilder WithResourceId(string id)
        {
            azResourceBuilder.WithId(id);
            return this;
        }

        // WithResourceProperty sets a property of the resource for the AzRequest.
        public AzAtomicRequestBuilder WithResourceProperty(string key, object value)
        {
            azResourceBuilder.WithProperty(key, value);
            return this;
        }

        // WithActionProperty sets a property of the action for the AzRequest.
        public AzAtomicRequestBuilder WithActionProperty(string key, object value)
        {
            azActionBuilder.WithProperty(key, value);
            return this;
        }

        // WithContextProperty sets a property of the context for the AzRequest.
        public AzAtomicRequestBuilder WithContextProperty(string key, object value)
        {
            azContextBuilder.WithProperty(key, value);
            return this;
        }

        // Build builds the AzAtomicRequest object.
        public AzRequest? Build()
        {
            var subject = azSubjectBuilder.Build();
            var resource = azResourceBuilder.Build();
            var action = azActionBuilder.Build();
            var context = azContextBuilder.Build();

            azRequestBuilder
                .WithPrincipal(principal)
                .WithRequestId(requestId)
                .WithSubject(subject)
                .WithResource(resource)
                .WithAction(action)
                .WithContext(context);

            return azRequestBuilder.Build();
        }
    }
}