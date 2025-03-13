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

namespace Permguard;

public class AzEndpoint
{
    public string Schema { get; }
    public int Port { get; }
    public string Host { get; }

    public AzEndpoint(string schema, int port, string host)
    {
        Schema = schema ?? throw new ArgumentNullException(nameof(schema));
        Port = port;
        Host = host ?? throw new ArgumentNullException(nameof(host));
    }
}

public class AzConfig
{
    public AzEndpoint? Endpoint { get; private set; }

    public AzConfig WithEndpoint(AzEndpoint? endpoint)
    {
        this.Endpoint = endpoint;
        return this;
    }
}