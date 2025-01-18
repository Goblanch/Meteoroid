using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class AsteroidController : RecyclableObject
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal override void Init(Vector2 position)
    {
        throw new System.NotImplementedException();
    }

    internal override void Release()
    {
        throw new System.NotImplementedException();
    }
}
