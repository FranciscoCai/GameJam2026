using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCDataFunctions : MonoBehaviour
{
    [Header("Vision Settings")]
    [SerializeField] private float viewDistance = 10f;
    [SerializeField] private float viewRadius = 2f; // Radio de la visión
    [SerializeField] private LayerMask viewMask;

    [Header("`Nav Settings")]
    public NavMeshAgent agent;
    public GameObject[] patrolPoints;


    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * viewDistance, Color.red);

        Debug.DrawLine(transform.position + transform.forward * viewDistance, transform.position + transform.forward * viewDistance + Vector3.up * viewRadius, Color.green);
    }
    public List<GameObject> DetectObjectsInView()
    {
        List<GameObject> detectedObjects = new List<GameObject>();
        Collider[] hits = Physics.OverlapCapsule(transform.position, transform.position + transform.forward * viewDistance, viewRadius, viewMask);
        foreach (Collider hit in hits)
        {
            detectedObjects.Add(hit.gameObject);
        }
        return detectedObjects;
    }
}
