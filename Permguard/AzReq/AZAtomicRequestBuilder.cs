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
    // AZAtomicRequestBuilder is the builder for the AZAtomicRequest object.
    public class AZAtomicRequestBuilder
    {
        private string requestID;
        private Permguard.Principal principal;
        private Permguard.AzReq.SubjectBuilder azSubjectBuilder;
        private Permguard.AzReq.ResourceBuilder azResourceBuilder;
        private Permguard.AzReq.ActionBuilder azActionBuilder;
        private Permguard.AzReq.ContextBuilder azContextBuilder;
        private Permguard.AzReq.AZRequestBuilder azRequestBuilder;

        // Constructor
        public AZAtomicRequestBuilder(long zoneID, string ledgerID, string subjectID, string resourceKind, string actionName)
        {
            azRequestBuilder = new AZRequestBuilder(zoneID, ledgerID);
            azSubjectBuilder = new SubjectBuilder(subjectID);
            azResourceBuilder = new ResourceBuilder(resourceKind);
            azActionBuilder = new ActionBuilder(actionName);
            azContextBuilder = new ContextBuilder();
        }

        // WithEntitiesMap sets the entities map to the AZRequest.
        public AZAtomicRequestBuilder WithEntitiesMap(string schema, List<Dictionary<string, object>> entities)
        {
            azRequestBuilder.WithEntitiesMap(schema, entities);
            return this;
        }

        // WithEntitiesItems sets the entities items to the AZRequest.
        public AZAtomicRequestBuilder WithEntitiesItems(string schema, List<Dictionary<string, object>> entities)
        {
            azRequestBuilder.WithEntitiesItems(schema, entities);
            return this;
        }

        // WithRequestID sets the ID of the AZRequest.
        public AZAtomicRequestBuilder WithRequestID(string requestID)
        {
            this.requestID = requestID;
            return this;
        }

        // WithPrincipal sets the principal of the AZRequest.
        public AZAtomicRequestBuilder WithPrincipal(Principal principal)
        {
            this.principal = principal;
            return this;
        }

        // WithSubjectKind sets the kind of the subject for the AZRequest.
        public AZAtomicRequestBuilder WithSubjectKind(string kind)
        {
            azSubjectBuilder.WithKind(kind);
            return this;
        }

        // WithSubjectSource sets the source of the subject for the AZRequest.
        public AZAtomicRequestBuilder WithSubjectSource(string source)
        {
            azSubjectBuilder.WithSource(source);
            return this;
        }

        // WithSubjectProperty sets a property of the subject for the AZRequest.
        public AZAtomicRequestBuilder WithSubjectProperty(string key, object value)
        {
            azSubjectBuilder.WithProperty(key, value);
            return this;
        }

        // WithResourceID sets the ID of the resource for the AZRequest.
        public AZAtomicRequestBuilder WithResourceID(string id)
        {
            azResourceBuilder.WithID(id);
            return this;
        }

        // WithResourceProperty sets a property of the resource for the AZRequest.
        public AZAtomicRequestBuilder WithResourceProperty(string key, object value)
        {
            azResourceBuilder.WithProperty(key, value);
            return this;
        }

        // WithActionProperty sets a property of the action for the AZRequest.
        public AZAtomicRequestBuilder WithActionProperty(string key, object value)
        {
            azActionBuilder.WithProperty(key, value);
            return this;
        }

        // WithContextProperty sets a property of the context for the AZRequest.
        public AZAtomicRequestBuilder WithContextProperty(string key, object value)
        {
            azContextBuilder.WithProperty(key, value);
            return this;
        }

        // Build builds the AZAtomicRequest object.
        public AZRequest Build()
        {
            var subject = azSubjectBuilder.Build();
            var resource = azResourceBuilder.Build();
            var action = azActionBuilder.Build();
            var context = azContextBuilder.Build();

            azRequestBuilder
                .WithPrincipal(principal)
                .WithRequestID(requestID)
                .WithSubject(subject)
                .WithResource(resource)
                .WithAction(action)
                .WithContext(context);

            return azRequestBuilder.Build();
        }
    }
}
