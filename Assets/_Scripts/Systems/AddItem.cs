using UnityEngine;

public class AddItem : MonoBehaviour
{
    public Inventory_Manager inventoryManager;
    public Item_Script[] itemsToPickup;

    public void PickupItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickup[id]);
        if (result == true)
        {
            print("Item Add");
        }
        else
        {
            print("Inventory full");
        }
    }

    public void GetSelectedItem()
    {
        Item_Script receivedItem = inventoryManager.GetSelectedItem(false);
        if (receivedItem != null)
        {
            print("Received");
        }
        else
        {
            print("Not Received");
        }

    }

        public void UseSelectedItem()
    {
        Item_Script receivedItem = inventoryManager.GetSelectedItem(true);
        if (receivedItem != null)
        {
            print("Used");
        }
        else
        {
            print("Not Used");
        }

    }



}
