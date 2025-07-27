using UnityEngine;

public class Inventory_Manager : MonoBehaviour
{
    public InventorySlot[] inventorySlot;
    public GameObject inventoryItemPrefab;
    public int inventoryMax = 4;

    int selectedSlot = -1;

    void Start()
    {
        ChangeSelectedSlot(0);
    }

    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlot[selectedSlot].DeSelect();
        }
        inventorySlot[newValue].Select();
        selectedSlot = newValue;
    }

    public bool AddItem(Item_Script item)
    {
        for (int i = 0; i < inventorySlot.Length; i++)
        {
            InventorySlot slot = inventorySlot[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null &&
            itemInSlot.item == item &&
            itemInSlot.count < inventoryMax &&
            itemInSlot.item.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        for (int i = 0; i < inventorySlot.Length; i++)
        {
            InventorySlot slot = inventorySlot[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }   

        return true;
    }

    void SpawnNewItem(Item_Script item, InventorySlot slot)
    {
        GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
        inventoryItem.CreateItem(item);
    }

}
