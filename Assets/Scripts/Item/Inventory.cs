using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<ItemData> items = new List<ItemData>();
    public GameObject[] inventoryItems;
    [SerializeField] private int removeItemMin = 2;
    [SerializeField] private int removeItemMax = 4;
    //public GameObject startItemData;
    private void Start()
    {
        //AddItem(startItemData.GetComponent<WorldItem>());
        //Invoke(nameof(RemoveItem), 5f);
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
