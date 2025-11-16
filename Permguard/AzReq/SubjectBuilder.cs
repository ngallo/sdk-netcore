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
    public static class SubjectDefault
    {
        public const string UserType = "user";
    }

    // SubjectBuilder is the builder for the subject object.
    public class SubjectBuilder: Builder
    {
        private readonly Subject subject;

        // Constructor to initialize SubjectBuilder with an Id
        public SubjectBuilder(string id)
        {
            subject = new Subject
            {
                Id = id,
                Type = SubjectDefault.UserType,
                Properties = new Dictionary<string, object>()
            };
        }

        // WithType sets the kind of the subject.
        public SubjectBuilder WithType(string kind)
        {
            subject.Type = kind;
            return this;
        }

        // WithSource sets the source of the subject.
        public SubjectBuilder WithSource(string source)
        {
            subject.Source = source;
            return this;
        }

        // WithProperty sets a property of the subject.
        public SubjectBuilder WithProperty(string key, object value)
        {
            if (subject.Properties != null) subject.Properties[key] = value;
            return this;
        }

        // Build constructs and returns the final Subject object.
        public Subject Build()
        {
            var instance = new Subject
            {
                Id = subject.Id,
                Type = subject.Type,
                Source = subject.Source,
                Properties = DeepCopy(subject.Properties)
            };
            return instance;
        }
    }
}
