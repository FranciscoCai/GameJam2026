using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollision : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject PickParticle;
    private void OnTriggerEnter(Collider other)
    {
        if (inventory != null)
        {
            WorldItem worldItem = other.gameObject.GetComponent<WorldItem>();
            if(ItemRequiredManager.Instance != null)
            {
                List<ItemData> requiredItems = ItemRequiredManager.Instance.GetItemList();
                bool isRequired = false;
                foreach (ItemData itemData in requiredItems)
                {
                    if (worldItem != null && worldItem.itemData == itemData)
                    {
                        isRequired = true;
                        break;
                    }
                }
                if (!isRequired)
                {
                    return;
                }
            }
            if (inventory.HasItem(worldItem))
            {
                return;
            }
            if (worldItem != null)
            {
                Instantiate(PickParticle, worldItem.transform.position, Quaternion.identity);
                StartCoroutine(DestroyParticle(PickParticle));
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
