using UnityEngine;

public abstract class RecyclableObject : MonoBehaviour
{
    private ObjectPool _objectPool;

    public void Configure(ObjectPool objectPool){
        _objectPool = objectPool;
    }

    public void Recycle(){
        _objectPool.RecycleGameObject(this);
    }

    internal abstract void Init(Vector2 position);
    internal abstract void Release();
}
