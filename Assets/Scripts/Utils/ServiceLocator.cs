using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using UnityEditorInternal;

public class ServiceLocator
{
    public static ServiceLocator Instance => _instance ?? (_instance = new ServiceLocator());
    private static ServiceLocator _instance;

    private readonly Dictionary<Type, Object> _services;

    private ServiceLocator(){
        _services = new Dictionary<Type, object>();
    }

    public void RegisterService<T>(T service){
        var type = typeof(T);
        Assert.IsFalse(_services.ContainsKey(type), $"Service {type} is already registered");
        _services.Add(type, service);
    }

    public T GetService<T>(){
        var type = typeof(T);
        if(!_services.TryGetValue(type, out var service)){
            throw new Exception($"Service {type} not found");
        }

        return (T) service;
    }
}