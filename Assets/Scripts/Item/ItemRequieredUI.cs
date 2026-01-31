using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemRequieredUI : MonoBehaviour
{
    public TextMeshProUGUI[] itemNameText;
    private void Start()
    {
        List<ItemData> items = ItemRequiredManager.Instance.GetItemList();
        for (int i = 0; i < items.Count; i++)
        {
            itemNameText[i].text = items[i].itemName;
        }
    }
}
