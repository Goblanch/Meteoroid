using System.Collections;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

public class AsteroidSpawnPoint : MonoBehaviour {    
    
    [BoxGroup("Spawn Settings")] public Vector2 direction = Vector2.right;
    [BoxGroup("Spawn Settings"), Range(0f, 180f)] public float angleRange = 25f;
    [field: SerializeField, BoxGroup("Visual Settings")] private float gizmoLineLength = 2f;

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Vector3 end = transform.position + (Vector3)direction.normalized * gizmoLineLength;
        Gizmos.DrawLine(transform.position, end);

        Gizmos.color = new Color(1f, 0.5f, 0f, 0.5f);
        Quaternion leftLimit = Quaternion.Euler(0, 0, -angleRange / 2);
        Quaternion rightLimit = Quaternion.Euler(0, 0, angleRange / 2);

        Vector3 leftVector = leftLimit * direction.normalized * gizmoLineLength;
        Vector3 rightVector = rightLimit * direction.normalized * gizmoLineLength;

        Gizmos.DrawLine(transform.position, transform.position + leftVector);
        Gizmos.DrawLine(transform.position, transform.position + rightVector);
    }

    public Vector2 GetRandomSpawnDirection(){
        float randomAngle = Random.Range(-angleRange / 2, angleRange / 2);

        Quaternion rotation = Quaternion.Euler(0, 0, randomAngle);
        return rotation * direction.normalized;
    }
}