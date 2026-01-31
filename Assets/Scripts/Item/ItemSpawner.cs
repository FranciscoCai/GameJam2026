using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public ItemData itemData;
    private void Start()
    {
        if(ItemSpawnerManager.instance != null)
        {
            AddToItemManagerSpawnerList();
        }
        else
        {
            Debug.LogWarning("ItemSpawnerManager instance is null.");
        }
    }
    public void SpawnItem()
    {
        GameObject instantiateItem = Instantiate(itemData.prefab, gameObject.transform.position, gameObject.transform.rotation);
        WorldItem worldItem = instantiateItem.GetComponent<WorldItem>();
        worldItem.itemSpawner = this;
        ItemSpawnerManager.instance.removeFromItemSpawnerList(this);
    }
    public void AddToItemManagerSpawnerList()
    {
        ItemSpawnerManager.instance.addToItemSpawnerList(this);
    }

}
