using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryManager : MonoBehaviour
{
    private Inventory inventory;

    private void Awake()
    {
        inventory = new Inventory(UseItem, 18);
    }

    public void UseItem(Item inventoryItem)
    {
        Debug.Log("Use Item: " + inventoryItem);
    }

    public Inventory GetInventory()
    {
        return inventory;
    }
}
