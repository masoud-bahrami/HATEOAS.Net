/*
  HATEOAS.Net solution contains implementations of 
    Hypermedia as the engine of application state (HATEOAS)
    based on different specifications.

 HATEOAS.Net.HAL is an implementation of HAL's Specification, and it also contains some
 extra features such as Link httpVerb(GET, POST ...) and also action parameters.

 Masoud Bahrami
 http://refactor.ir
 https://twitter.com/masodbahrami
 */

using HATEOAS.Net.HAL.Exceptions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HATEOAS.Net.HAL
{
    public static class ObjectExtensions
    {
        public static Dictionary<string, object> BreakesToItsProperties(this object obj)
        {
            if (typeof(IEnumerable<object>).IsAssignableFrom(obj.GetType()))
            {
                throw new BreakingCollectionToItsPropertiesExeption();
            }

            Dictionary<string, object> result = new Dictionary<string, object>();
            var propertyInfos = obj.GetType().GetProperties()
                .Where(p => !Attribute.IsDefined(p, typeof(IgnoreAttribute)));

            foreach (var propertyInfo in propertyInfos)
            {
                var value = obj.GetType().GetProperty(propertyInfo.Name)
                    .GetValue(obj, null);

                result.Add(propertyInfo.Name, value);
            }

            return result;
        }

        public static bool IsConvertableToValueToken(this object obj)
        {
            var types = new List<Type>
            {
                typeof(DateTime),
                typeof(DateTimeOffset),
                typeof(Guid),
                typeof(JValue),
                typeof(TimeSpan),
                typeof(Uri),
                typeof(bool),
                typeof(char),
                typeof(decimal),
                typeof(double),
                typeof(float),
                typeof(float),
                typeof(string),
                typeof(long),
                typeof(ulong),
            };
            var value = obj.GetType();
            return value.IsValueType || types.Contains(value);
        }
        public static bool IsConvertableToObjectToken(this object obj)
        {
            var types = new List<Type>
            {
                typeof(JObject)
            };
            var value = obj.GetType();
            return !value.IsValueType || types.Contains(value);
            
        }
        public static bool IsConvertableToArrayToken(this object obj)
        {
            var types = new List<Type>
            {
                typeof(JObject)
            };
            var value = obj.GetType();
            return typeof(IEnumerable<object>).IsAssignableFrom(value);
        }
        public static JToken ToJToken(this object obj)
        {
            if (obj.IsConvertableToValueToken())
            {
                return new JValue(obj);
            }
            else if (obj.IsConvertableToArrayToken())
            {
                return JArray.FromObject(obj);
            }
            else if (obj.IsConvertableToObjectToken())
            {
                 return JObject.FromObject(obj);
            }
            throw new Exception();
        }
    }
}