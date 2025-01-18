using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ObjectPool
{
    public readonly RecyclableObject _prefab;
    private readonly HashSet<RecyclableObject> _instantiateObjects;
    private Queue<RecyclableObject> _recycledObjects;

    public ObjectPool(RecyclableObject prefab){
        _prefab = prefab;
        _instantiateObjects = new HashSet<RecyclableObject>(); 
    }

    public void Init(int numberOfInitialObjects){
        _recycledObjects = new Queue<RecyclableObject>(numberOfInitialObjects);

        for(int i = 0; i < numberOfInitialObjects; i++){
            var instance = InstantiateNewInstance();
            instance.gameObject.SetActive(false);
            _recycledObjects.Enqueue(instance);
        }
    }

    private RecyclableObject InstantiateNewInstance(){
        var instance = Object.Instantiate(_prefab);
        instance.Configure(this);

        return instance;
    }

    public T Spawn<T>(Vector2 position){
        var recycableObject = GetInstance();
        _instantiateObjects.Add(recycableObject);
        recycableObject.gameObject.SetActive(true);
        recycableObject.Init(position);
        
        return recycableObject.GetComponent<T>();
    }

    private RecyclableObject GetInstance(){
        if(_recycledObjects.Count > 0) return _recycledObjects.Dequeue();

        Debug.LogWarning($"Not enough recycled objects for {_prefab.name} consider increase initial number of objects");
        var instance = InstantiateNewInstance();
        return instance;
    }

    public void RecycleGameObject(RecyclableObject objectToRecycle){
        bool wasInstantiated = _instantiateObjects.Remove(objectToRecycle);
        Assert.IsTrue(wasInstantiated, $"{objectToRecycle.name} was not instantiate on {_prefab.name} pool");
        objectToRecycle.gameObject.SetActive(false);
        objectToRecycle.Release();
        _recycledObjects.Enqueue(objectToRecycle);
    }
}
