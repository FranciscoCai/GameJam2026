using UnityEngine;

[CreateAssetMenu(
    fileName = "NewItem",
    menuName = "Inventory/Item"
)]
public class ItemData : ScriptableObject
{
    public string id;
    public string itemName;
    public GameObject prefab;
}