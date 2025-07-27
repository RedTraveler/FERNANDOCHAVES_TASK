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


}
