﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace {{ spec.title | caseUcfirst }}
{
    public static class ExtensionMethods
    {
        public static string ToJson(this Dictionary<string, object> dict)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter> { new StringEnumConverter() }
            };

            return JsonConvert.SerializeObject(dict, settings);
        }

        public static string ToQueryString(this Dictionary<string, object> parameters)
        {
            var query = new List<string>();

            foreach (var kvp in parameters)
            {
                if (kvp.Value != null)
                {
                    if (kvp.Value is List<object> list)
                    {
                        foreach(object entry in list)
                        {
                            query.Add($"{kvp.Key}[]={Uri.EscapeUriString(entry.ToString()!)}");
                        }
                    }
                    else
                    {
                        query.Add($"{kvp.Key}={Uri.EscapeUriString(kvp.Value.ToString()!)}");
                    }
                }
            }
            return string.Join("&", query);
        }
    }
}