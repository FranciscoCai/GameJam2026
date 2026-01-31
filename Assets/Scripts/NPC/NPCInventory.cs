using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInventory : MonoBehaviour
{
    private List<ItemData> items = new List<ItemData>();
    public GameObject[] inventoryItems;
    public bool HasItem()
    {
        if(items.Count == 0)
        {
            return false;
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
}
