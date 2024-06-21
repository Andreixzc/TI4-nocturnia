using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> inventory = new List<InventoryItem>();
    private Dictionary<ItemData, InventoryItem> itemDictionaty = new Dictionary<ItemData, InventoryItem>();

    public void Add(ItemData itemData)
    {
        if(itemDictionaty.TryGetValue(itemData, out InventoryItem item))
        {
            item.AddToStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(itemData);
            inventory.Add(newItem);
            itemDictionaty.Add(itemData, newItem);
        }
    }

    public void Remove(ItemData itemData)
    {
        if (itemDictionaty.TryGetValue(itemData, out InventoryItem item))
        {
            item.RemoveFromStack();
            if(item.stackSize == 0)
            {
                inventory.Remove(item);
                itemDictionaty.Remove(itemData);
            }
        }
     
    }

}
