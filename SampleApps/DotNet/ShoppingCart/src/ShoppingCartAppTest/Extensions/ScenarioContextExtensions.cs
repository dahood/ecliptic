using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace ShoppingCartApp.Tests.Extensions
{
    public static class ScenarioContextExtensions
    {
        public static void StoreList<T>(this ScenarioContext context, IEnumerable<T> items, string key = null)
        {
            var keyValue = string.IsNullOrEmpty(key) ? GetDefaultKey<T>() : key;
            context.Set(items, keyValue);
        }

        private static string GetDefaultKey<T>()
        {
            return typeof(T).FullName;
        }

        public static void RemoveList<T>(this ScenarioContext context, string key = null)
        {
            var keyValue = string.IsNullOrEmpty(key) ? GetDefaultKey<T>() : key;
            context.Remove(keyValue);
        }

        public static IEnumerable<T> GetList<T>(this ScenarioContext context, string key = null)
        {
            var keyValue = string.IsNullOrEmpty(key) ? GetDefaultKey<T>() : key;
            return context.Get<IEnumerable<T>>(keyValue);
        }
        
        public static IEnumerable<T> SafeGetList<T>(this ScenarioContext context, string key = null)
        {
            var keyValue = string.IsNullOrEmpty(key) ? GetDefaultKey<T>() : key;
            return context.ContainsKey(keyValue) ? context.Get<IEnumerable<T>>(keyValue) : Enumerable.Empty<T>();
        }

        public static bool ContainsKey<T>(this ScenarioContext context)
        {
            return context.ContainsKey(GetDefaultKey<T>());
        }

        public static T Store<T>(this ScenarioContext context, T item)
        {
            context.Set(item);
            return item;
        }

        public static T Store<T>(this ScenarioContext context, T item, string key)
        {
            context.Set(item, key);
            return item;
        }

        public static T FromStorage<T>(this ScenarioContext context)
        {
            return context.Get<T>();
        }

        public static T SafeGet<T>(this ScenarioContext context)
        {
            //this allows for nulls to be retrieved from the context storage
            return (T)context[typeof(T).FullName];
        }

        public static T GetFromList<T>(this ScenarioContext context, Predicate<T> where)
        {
            return context.GetList<T>().Single(x => where(x));
        }
        
        public static void StoreIdMapping<T>(this ScenarioContext context, long idLabel, long actualDbId)
        {
            var key = "IdMap" + GetDefaultKey<T>();
            if (context.ContainsKey(key))
            {
                var map = context.Get<IDictionary<long, long>>(key);
                if (map.ContainsKey(idLabel))
                {
                    map[idLabel] = actualDbId;
                }
                else
                {
                    map.Add(idLabel, actualDbId);
                }
            }
            else
            {
                var map = new Dictionary<long, long> { { idLabel, actualDbId } };
                context.Store(map, key);
            }
        }

        public static long GetMappedDbId<T>(this ScenarioContext context, long idLabel)
        {
            var notFoundMessage = string.Format("No ID mapping exists for ID label [{0}] for entity of type [{1}].  Did you remember to define this item in a previous step?", idLabel, typeof(T).Name);
            var key = "IdMap" + GetDefaultKey<T>();
            if (!context.ContainsKey(key)) throw new SpecFlowException(notFoundMessage);
            var dictionary = (Dictionary<long, long>)context[key];
            if (!dictionary.ContainsKey(idLabel)) throw new SpecFlowException(notFoundMessage);
            return dictionary[idLabel];
        }

        public static long GetMappedTestingId<T>(this ScenarioContext context, long actualDbId)
        {
            var notFoundMessage = string.Format("No ID Mapping exists for DB ID [{0}] for entity of type [{1}].  Did you remember to define this item in a previous step?", actualDbId, typeof(T).Name);
            var key = "IdMap" + GetDefaultKey<T>();
            if (!context.ContainsKey(key)) throw new SpecFlowException(notFoundMessage);
            var dictionary = (Dictionary<long, long>)context[key];
            var query = dictionary.Where(x => x.Value == actualDbId).ToArray();
            if (!query.Any()) throw new SpecFlowException(notFoundMessage);
            if (query.Count() > 1)
            {
                throw new SpecFlowException(string.Format("Multuple possible ID mappings found for DB ID [{0}].", actualDbId));
            }
            return query.First().Key;
        }
    }
}
