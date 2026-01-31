using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ItemRequiredManager : MonoBehaviour
{
    public static ItemRequiredManager Instance;
    [SerializeField] private List<ItemData> posibleItemData;
    [SerializeField] private List<ItemData> itemData;
    [SerializeField] private int itemsToRequire = 6;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        List<ItemData> shuffledList = new List<ItemData>(posibleItemData);
        for (int i = shuffledList.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = shuffledList[i];
            shuffledList[i] = shuffledList[j];
            shuffledList[j] = temp;
        }
        itemData.Clear();
        for (int i = 0; i < itemsToRequire; i++)
        {
            itemData.Add(shuffledList[i]);
        }
    }
    public List<ItemData> GetItemList()
    {
        return itemData;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
