using UnityEngine;

public abstract class RecyclableObject : MonoBehaviour
{
    public ObjectPool objectPool {get; private set;}

    public void Configure(ObjectPool objectPool){
        this.objectPool = objectPool;
    }

    public void Recycle(){
        objectPool.RecycleGameObject(this);
    }

    internal abstract void Init(Vector2 position);
    internal abstract void Release();
}
