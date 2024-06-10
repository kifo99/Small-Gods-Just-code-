using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Inventory.Model;
using Unity.VisualScripting;
using UnityEngine;

public class ItemDeliveryTypeQuest : MonoBehaviour
{
    
    [SerializeField]
    private InventorySO inventory;
    private int index = 0;
    private int amount = 0;
    private string name;

    private static ItemDeliveryTypeQuest instance;

    private void Awake() 
    {
        if(instance != null)
        {
            Debug.LogError("Ther is more than one instance og ItemDeliveryTypeQuest!");
        }
        instance = this;
    }

    public static ItemDeliveryTypeQuest GetInstance()
    {
        return instance;
    }


    public void StartItemDelivery(InventoryItem itemData,int amountToDeliver, string itemName)
    {
        inventory.AddItem(itemData);

        name = itemData.item.Name;
        amount = itemData.quantity;
        
    }



    public void Deliver()
    {
        foreach(InventoryItem item in inventory.inventoryItems.ToList())
        {
            
            if(item.item != null)
            {
                Debug.Log($"Deliver is called and item is {item.item.name}");
                if(item.item.Name == name)
                {
                    Debug.Log("Deliver is called and we have item");
                    index = inventory.inventoryItems.IndexOf(item);
                    inventory.RemoveItem(index, amount); 
                    Debug.Log($"Ther is {item.item.Name} item and index is {index}");
                }
                else
                {
                    Debug.Log($"No item with name {name}");
                }
            }
            else
            {
                continue;
            }
        }

          
 
    }

}
