using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    private List<ItemData> items = new List<ItemData>();
    public GameObject[] inventoryItems;
    [SerializeField] private int removeItemMin = 2;
    [SerializeField] private int removeItemMax = 4;
    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public bool HasItem(WorldItem worldItem)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (worldItem.itemData == items[i])
            {
                return true;
            }
        }
        return false; 
    }
    public bool HasAllItems()
    {
        List<ItemData> requiredItems = ItemRequiredManager.Instance.GetItemList();
        for (int i = 0; i < requiredItems.Count; i++)
        {
            bool found = false;
            for (int j = 0; j < items.Count; j++)
            {
                if (requiredItems[i] == items[j])
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                return false;
            }
        }
        return true;
    }
    public void AddItem(WorldItem item)
    {
        if (inventoryItems.Length > items.Count)
        {
            Transform firstChild = item.transform.childCount > 0 ? item.transform.GetChild(0) : null;
            MeshFilter worldItemMeshFilter = firstChild != null ? firstChild.GetComponent<MeshFilter>() : null;
            MeshFilter inventoryMeshFilter = inventoryItems[items.Count].GetComponent<MeshFilter>();
            MeshRenderer worldItemMeshRenderer = firstChild != null ? firstChild.GetComponent<MeshRenderer>() : null;
            MeshRenderer inventoryMeshRenderer = inventoryItems[items.Count].GetComponent<MeshRenderer>();
            if (worldItemMeshFilter != null && inventoryMeshFilter != null)
            {
                inventoryMeshFilter.mesh = worldItemMeshFilter.mesh;
                inventoryMeshRenderer.material = worldItemMeshRenderer.material;
            }
            item.itemSpawner.AddToItemManagerSpawnerList();
            items.Add(item.itemData);
            Destroy(item.gameObject);
        }
    }
    public void RemoveItem()
    {
        for(int i = 0; i < Random.Range(removeItemMin, removeItemMax); i++)
        {
            if (items.Count > 0)
            {
                items.RemoveAt(items.Count - 1);
                MeshFilter inventoryMeshFilter = inventoryItems[items.Count].GetComponent<MeshFilter>();
                if (inventoryMeshFilter != null)
                {
                    inventoryMeshFilter.mesh = null;
                    inventoryItems[items.Count].GetComponent<MeshRenderer>().material = null;
                }
            }
        }
    }

}
