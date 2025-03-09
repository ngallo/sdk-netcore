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
    public static class CopyUtility
    {
        // Deep copies a dictionary (map).
        public static Dictionary<string, object> DeepCopyMap(Dictionary<string, object> src)
        {
            if (src == null)
                return null;

            var dst = new Dictionary<string, object>(src.Count);
            foreach (var kvp in src)
            {
                dst[kvp.Key] = DeepCopy(kvp.Value);
            }
            return dst;
        }

        // Deep copies an object, handling different types such as maps, lists, or simple values.
        public static object DeepCopy(object value)
        {
            if (value == null)
                return null;

            switch (value)
            {
                case Dictionary<string, object> dict:
                    return DeepCopyMap(dict);
                case List<object> list:
                    var copiedList = new List<object>(list.Count);
                    foreach (var item in list)
                    {
                        copiedList.Add(DeepCopy(item));
                    }
                    return copiedList;
                default:
                    return value; // Return the value itself if it's not a map or list
            }
        }
    }
}