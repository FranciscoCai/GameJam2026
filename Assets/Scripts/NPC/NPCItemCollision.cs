using System.Collections;
using UnityEngine;

public class NPCItemCollision : MonoBehaviour
{
    [SerializeField] private NPCInventory inventory;
    [SerializeField] private GameObject pickParticle;
    private void OnTriggerEnter(Collider other)
    {
        if (inventory != null)
        {
            WorldItem worldItem = other.gameObject.GetComponent<WorldItem>();
            if (inventory.HasItem())
            {
                return;
            }
            if (worldItem != null)
            {
                GameObject instantiateObject = Instantiate(pickParticle, worldItem.transform.position, Quaternion.identity);
                StartCoroutine(DestroyParticle(instantiateObject));
                inventory.AddItem(worldItem);
            }
        }
    }
    private IEnumerator DestroyParticle(GameObject ParticleToDestroy)
    {
        yield return new WaitForSeconds(2f);
        Destroy(ParticleToDestroy);
    }
}
