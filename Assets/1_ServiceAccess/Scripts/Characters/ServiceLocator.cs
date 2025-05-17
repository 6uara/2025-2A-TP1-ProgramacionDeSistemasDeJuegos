using System.Collections.Generic;
using System;
using UnityEngine;

namespace Excercise1
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> services = new();

        public static void RegisterService<T>(T service) where T : class
        {
            var type = typeof(T);
            if (!services.ContainsKey(type))
            {
                services.Add(type, service);
            }
        }

        public static T GetService<T>() where T : class
        {
            var type = typeof(T);
            if (services.TryGetValue(type, out var service))
            {
                return service as T;
            }

            throw new Exception($"Service of type {type} not found.");
        }

        public static void Clear() => services.Clear();
    }
}

