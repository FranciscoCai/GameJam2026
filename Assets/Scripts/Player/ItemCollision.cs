using UnityEngine;

public class ItemCollision : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    private void OnTriggerEnter(Collider other)
    {
        if (inventory != null)
        {
            WorldItem worldItem = other.gameObject.GetComponent<WorldItem>();
            if(inventory.HasItem(worldItem))
            {
                return;
            }
            if (worldItem != null)
            {
                inventory.AddItem(worldItem);
            }
        }
    }
}
