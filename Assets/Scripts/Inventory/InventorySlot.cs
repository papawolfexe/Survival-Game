using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField] private InventoryItemData itemData; // referance to the data of what makes an item
    [SerializeField] private int stackSize; // current stack size - how many of the data do we have

    public InventoryItemData ItemData => itemData;
    public int StackSize => stackSize;

    public InventorySlot(InventoryItemData source, int amount) // Constructer to make an occupied inventory slot 
    {
        itemData = source;
        stackSize = amount;
    }

    public InventorySlot() // Constructer to make an empty inventory slot 
    {
        ClearSlot();
    }

    public void ClearSlot() // Clears the slot 
    {
        itemData = null;
        stackSize = -1;
    }

    public void AssignItem(InventorySlot invSlot) // Assigns an item to the slot 
    {
        if (itemData == invSlot.ItemData) AddToStack(invSlot.stackSize); // Does the slot contain the same item. If yes, add to stack
        else // Overwrites slot with inventory slot that we're passing in 
        {
            itemData = invSlot.itemData;
            stackSize = 0;
            AddToStack(invSlot.stackSize);
        }
    }

    public void UpateInventorySlot(InventoryItemData data, int amount) // Updates slot directly 
    {
        itemData = data;
        stackSize = amount;
    }

    public bool EnoughRoomLeftInStack(int amountToAdd, out int amountRemaining) // Checks if there's enough room in stack for the amount we're trying to add
    {
        amountRemaining = ItemData.MaxStackSize - stackSize;

        return EnoughRoomLeftInStack(amountToAdd);
    }

    public bool EnoughRoomLeftInStack(int amountToAdd)
    {
        if (itemData == null || stackSize + amountToAdd <= itemData.MaxStackSize) return true;
        else return false;
    }

    public void AddToStack(int amount) // Adds to stack
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount) // Removes from stack
    { 
        stackSize -= amount;
    }

    public bool SplitStack(out InventorySlot splitStack) 
    {
        if (stackSize <= 1) // Is there enough to split
        {
            splitStack = null;
            return false;
        }

        int halfStack = Mathf.RoundToInt(stackSize / 2); // Gets half of the stack
        RemoveFromStack(halfStack);

        splitStack = new InventorySlot(itemData, halfStack); // Makes a copy of this slot with half the stack size
        return true;
    }

}
