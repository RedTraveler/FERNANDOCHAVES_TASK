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

    private void Update()
    {
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < 6)
            {
                ChangeSelectedSlot(number - 1);
            }
        }
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

        return false;
    }

    void SpawnNewItem(Item_Script item, InventorySlot slot)
    {
        GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
        inventoryItem.CreateItem(item);
    }

    public Item_Script GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlot[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            Item_Script item = itemInSlot.item;
            if (use == true)
            {
                itemInSlot.count--;
                if (itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount();
                }
            }
            return item;
        }
        return null;
    }

}
