using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    [System.Serializable]
    public class ItemSlot
    {
        public ItemData itemData;            // Link ItemData (Water, Bread, etc)
        public TextMeshProUGUI quantityText; // Link the text object (only the number)
    }

    public List<ItemSlot> slots = new List<ItemSlot>();

    private void Start()
    {
        RefreshAll();
    }

    public void RefreshAll()
    {
        foreach (var slot in slots)
        {
            UpdateSlot(slot.itemData);
        }
    }

    public void UpdateSlot(ItemData item)
    {
        foreach (ItemSlot slot in slots)
        {
            if (slot.itemData == item)
            {
                int qty = Inventory.Instance.GetQuantity(item);
                slot.quantityText.text = "" + qty;
                return;
            }
        }
    }
}
