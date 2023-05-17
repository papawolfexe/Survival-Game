using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class InventorySystem 
{
    [SerializeField] private List<InventorySlot> inventorySlots; // List of inventory slots
    
    public List<InventorySlot> InventorySlots => inventorySlots;
    public int InventorySize => InventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int size) // Constructer that sets the amount of slots
    {
        inventorySlots = new List<InventorySlot>(size);

        for (int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd)
    {
        if (ContainsItem(itemToAdd, out List<InventorySlot> invSlot)) // Checks if the item exists in inventory
        {
            foreach (var slot in invSlot)
            {
                if(slot.EnoughRoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }
            
        }

        if (HasFreeSlot(out InventorySlot freeSlot)) // Gets the first available slot
        {
            if (freeSlot.EnoughRoomLeftInStack(amountToAdd))
            {
                freeSlot.UpateInventorySlot(itemToAdd, amountToAdd);
                OnInventorySlotChanged?.Invoke(freeSlot);
                return true;
            }
            // Add implemtation to only take what can fill the stack, and check for another free slot to put the rest in
        }

        return false;
    }

    public bool ContainsItem(InventoryItemData itemToAdd, out List<InventorySlot> invSlot) // Checks if any of our slots have the item to add in them
    {
       invSlot = InventorySlots.Where(i => i.ItemData == itemToAdd).ToList(); // If they do,  they get a list of all of them

       return invSlot == null ? false : true; // If they do return true, if not return false 
    } 

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(i => i.ItemData == null); 
        return freeSlot == null ? false : true;
    }
}
