using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ItemSpawnerManager : MonoBehaviour
{
    public static ItemSpawnerManager instance;
    private List<ItemSpawner> itemSpawners = new List<ItemSpawner>();
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        StartCoroutine(SpawnItemsRoutine());
    }
    public void addToItemSpawnerList(ItemSpawner spawner)
    {
        itemSpawners.Add(spawner);
    }
    public void removeFromItemSpawnerList(ItemSpawner spawner)
    {
        itemSpawners.Remove(spawner);
    }
    private IEnumerator SpawnItemsRoutine()
    {
        while (true)
        {
            if(itemSpawners.Count == 0)
            {
                yield return new WaitForSeconds(1f);
                continue;
            }
            int index = Random.Range(0, itemSpawners.Count);
            itemSpawners[index].SpawnItem();
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }

}
