using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public ItemData itemData;
    private void Start()
    {
        if(ItemSpawnerManager.instance != null)
        {
            ItemSpawnerManager.instance.addToItemSpawnerList(this);
        }
        else
        {
            Debug.LogWarning("ItemSpawnerManager instance is null.");
        }
    }
    public void SpawnItem()
    {
        Instantiate(itemData.prefab, gameObject.transform.position, gameObject.transform.rotation);
        ItemSpawnerManager.instance.removeFromItemSpawnerList(this);
    }

}
