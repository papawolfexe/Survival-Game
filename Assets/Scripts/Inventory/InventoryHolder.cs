using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Creates a new Inventory based on the size we give it
/// can branch to player inventory, chest inventory, etc by inherating from InventoryHolder
/// </summary>

[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int inventorySize;
    [SerializeField] protected InventorySystem inventorySystem;

    public InventorySystem InventorySystem => inventorySystem;

    public static UnityAction<InventorySystem> OnDynamicInventoryDispalyRequested;

    private void Awake()
    {
        inventorySystem = new InventorySystem(inventorySize);
    }
}
