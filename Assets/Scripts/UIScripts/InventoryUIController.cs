using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUIController : MonoBehaviour
{
    public DynamicInventoryDisplay chestPanel;
    public DynamicInventoryDisplay playerBackpackPanel;
    private bool backpackPanelActive = false;

    private void Awake()
    {
        chestPanel.gameObject.SetActive(false);
        playerBackpackPanel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
        PlayerInventoryHolder.OnPlayerBackpackDisplayRequested += DisplayPlayerBackpack;
    }

    private void OnDisable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
        PlayerInventoryHolder.OnPlayerBackpackDisplayRequested -= DisplayPlayerBackpack;
    }

    void Update()
    {
        if (chestPanel.gameObject.activeInHierarchy && Keyboard.current.eKey.wasPressedThisFrame)
            chestPanel.gameObject.SetActive(false);

        // Toggle player backpack panel with a single press of the Tab key
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            backpackPanelActive = !backpackPanelActive;
            playerBackpackPanel.gameObject.SetActive(backpackPanelActive);
        }
    }

    void DisplayInventory(InventorySystem invToDisplay)
    {
        chestPanel.gameObject.SetActive(true);
        chestPanel.RefreshDynamicInventory(invToDisplay);
    }

    void DisplayPlayerBackpack(InventorySystem invToDisplay)
    {
        playerBackpackPanel.gameObject.SetActive(true);
        playerBackpackPanel.RefreshDynamicInventory(invToDisplay);
    }
}
