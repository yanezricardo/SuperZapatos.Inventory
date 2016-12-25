using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;

namespace Inventory.Backend {
    [JsonObject]
    [JsonConverter(typeof(ServiceResponseConverter))]
    public class ServiceResponse {
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        [JsonProperty(PropertyName = "error_code", NullValueHandling = NullValueHandling.Ignore)]
        public int? ErrorCode { get; set; }

        [JsonProperty(PropertyName = "error_msg", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorMessage { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Result { get; set; }

        [JsonProperty(PropertyName = "total_elements", NullValueHandling = NullValueHandling.Ignore)]
        public int? TotalElements { get; set; }

        public ServiceResponse(bool success, HttpStatusCode statusCode, object result = null, string errorMessage = null) {
            Success = success;
            if (!success) {
                ErrorCode = (int)statusCode;
                if (string.IsNullOrWhiteSpace(ErrorMessage)) {
                    ErrorMessage = statusCode.ToString();
                }
            }
            Result = result;
            if (Result != null && Result is IEnumerable) {
                var enumerator = (Result as IEnumerable).GetEnumerator();
                int count = 0;
                while (enumerator.MoveNext()) {
                    count++;
                }
                TotalElements = count;
            }
        }
    }

    public class ServiceResponseConverter : JsonConverter {
        public override bool CanConvert(Type objectType) {
            return objectType == typeof(ServiceResponse);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            return existingValue;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            JObject jObject = new JObject();
            Type type = value.GetType();
            foreach (PropertyInfo propertyInfo in type.GetProperties()) {
                if (propertyInfo.CanRead) {
                    object propertyValue = propertyInfo.GetValue(value, null);
                    if (propertyValue != null) {
                        string propertyName = string.Empty;
                        if (propertyValue is IEnumerable && !(propertyValue is string)) {
                            propertyName = GetTypeName(propertyValue);
                        } else {                            
                            var attribute = propertyInfo.GetCustomAttributes<JsonPropertyAttribute>(true).FirstOrDefault();
                            if (attribute != null) {
                                propertyName = attribute.PropertyName;
                            }
                            if (string.IsNullOrWhiteSpace(propertyName)) {
                                var objAttribute = propertyValue.GetType().GetCustomAttributes<JsonObjectAttribute>(true).FirstOrDefault();
                                if (objAttribute != null && !string.IsNullOrWhiteSpace(objAttribute.Title)) {
                                    propertyName = objAttribute.Title;
                                } else {
                                    propertyName = propertyInfo.Name;
                                }
                            }
                        }
                        jObject.Add(propertyName, JToken.FromObject(propertyValue, serializer));
                    }
                }
            }
            jObject.WriteTo(writer);
        }

        private string GetTypeName(object instance) {
            string result = string.Empty;
            if (instance != null) {
                if (instance is IEnumerable) {
                    var enumerator = (instance as IEnumerable).GetEnumerator();
                    if (enumerator.MoveNext() && enumerator.Current != null) {
                        var attribute = enumerator.Current.GetType().GetCustomAttributes<JsonPluralNameAttribute>(true).FirstOrDefault();
                        if (attribute != null) {
                            result = attribute.PluralName;
                        }
                    }
                }
                if (string.IsNullOrWhiteSpace(result)) {
                    var attribute = instance.GetType().GetCustomAttributes<JsonObjectAttribute>(true).FirstOrDefault();
                    if (attribute != null && !string.IsNullOrWhiteSpace(attribute.Title)) {
                        result = attribute.Title;
                    } else {
                        result = instance.GetType().Name;
                    }
                }
            }
            return result;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = false)]
    public class JsonPluralNameAttribute: Attribute {
        public string PluralName { get; set; }

        public JsonPluralNameAttribute(string pluralName) {
            PluralName = pluralName;
        }
    }
}