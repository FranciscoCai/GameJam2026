using UnityEngine;
using UnityEngine.VFX;

public class VfxCollision : MonoBehaviour
{
    [Header("VFX")]
    public GameObject collisionVFXPrefab;

    [Header("Spawn Points")]
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;

    private void OnCollisionEnter(Collision collision)
    {
        HandleCollision(collision.GetContact(0).point);
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        Vector3 closestPoint = other.ClosestPoint(transform.position);
        HandleCollision(closestPoint);
    }
    */

    private void HandleCollision(Vector3 collisionPoint)
    {
        Vector3 directionToCollision = collisionPoint - transform.position;directionToCollision.y = 0f;

        if (directionToCollision.sqrMagnitude < 0.001f)return;

        directionToCollision.Normalize();

        float angle = Vector3.SignedAngle(transform.forward,directionToCollision,Vector3.up);

        if (angle < -14f)
        {
            SpawnVFX(leftPoint);
        }
        else if (angle > 14f)
        {
            SpawnVFX(rightPoint);
        }
        else
        {
            SpawnVFX(leftPoint);
            SpawnVFX(rightPoint);
        }
    }

    private void SpawnVFX(Transform point)
    {
        if (collisionVFXPrefab == null || point == null)return;

        Instantiate(collisionVFXPrefab,point.position,point.rotation);
    }
}
