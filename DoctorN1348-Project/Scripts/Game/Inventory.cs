using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    private Dictionary<ItemData, int> items = new Dictionary<ItemData, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool UseItem(ItemData itemData)
    {
        if (!items.ContainsKey(itemData) || items[itemData] <= 0)
            return false;

        items[itemData]--;

        FindFirstObjectByType<InventoryUI>()?.UpdateSlot(itemData);

        return true;
    }

    public void AddItem(ItemData itemData, int amount)
    {
        if (!items.ContainsKey(itemData))
            items[itemData] = 0;

        items[itemData] += amount;

        FindFirstObjectByType<InventoryUI>()?.UpdateSlot(itemData);
    }


    public int GetQuantity(ItemData item)
    {
        if (!items.ContainsKey(item))
            return 0;

        return items[item];
    }
}
