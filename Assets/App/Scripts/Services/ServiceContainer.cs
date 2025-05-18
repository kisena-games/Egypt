using System;
using System.Collections.Generic;

namespace App.Scripts.Services
{
    public class ServiceContainer
    {
        private static ServiceContainer _instance;

        public static ServiceContainer Container => _instance ??= new ServiceContainer();

        private Dictionary<Type, IService> _services;

        public ServiceContainer()
        {
            _services = new Dictionary<Type, IService>();
        }
        
        public T Register<T>(T implementation) where T : class, IService
        {
            var type = typeof(T);
            if (_services.ContainsKey(type))
                return Get<T>();
            _services.Add(type, implementation);
            return Get<T>();
        }

        public T Get<T>() where T : class, IService
        {
            return _services[typeof(T)] as T;
        }
    }
}