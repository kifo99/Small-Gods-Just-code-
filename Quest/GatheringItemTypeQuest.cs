using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;
using Ink.Runtime;
using System.Linq;

public class GatheringItemTypeQuest : MonoBehaviour
{
    [SerializeField]
    private InventorySO inventory;

    private static GatheringItemTypeQuest instance;

    private bool status = false;
    private int amount = 0;
    private InventoryItem item;
    private string itemName;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Ther is more than one instance og GatheringItemTypeQuest!");
        }
        instance = this;
    }

    public static GatheringItemTypeQuest GetInstance()
    {
        return instance;
    }

    public void StartGatheringQuest(int amount, string itemName)
    {
        Debug.Log($"StartGathering quest is called and item name is {itemName}");

        foreach (InventoryItem item in inventory.inventoryItems)
        {
            if (item.item != null && item.item.Name == itemName)
            {
                GatherItemMethod(item, amount, itemName);
            }
            else
            {
                continue;
            }
        }
    }

    public void GatherItemMethod(InventoryItem itemData, int amountToGather, string name)
    {
        Debug.Log($"Stack size is: {itemData.quantity}");

        if (itemData.item.Name != null)
        {
            if (itemData.item.Name == name)
            {
                this.amount = amountToGather;
                this.item = itemData;
                this.itemName = itemData.item.Name;
            }
            else
            {
                Debug.Log($"Ther is no item named {itemData.item.Name}!");
            }
        }
        else
        {
            Debug.LogError("itemData.item.Name is null");
        }
    }

    public void FinishGatheringQuest()
    {
        foreach (InventoryItem item in inventory.inventoryItems.ToList())
        {
            if (item.item != null)
            {
                if (item.item.Name == itemName)
                {
                    int index = inventory.inventoryItems.IndexOf(item);
                    Debug.Log($"FinishFarmersToolsQuest and index is {index}");
                    if (GetCanFinishStatus(item, amount, itemName))
                    {
                        Debug.Log("Amount is " + amount);
                        inventory.RemoveItem(index, amount);
                    }
                }
                else
                {
                    Debug.Log($"No item with name {itemName}");
                }
            }
            else
            {
                continue;
            }
        }
    }

    public bool GetCanFinishStatus(InventoryItem itemData, int amountToGather, string name)
    {
        bool status = false;
        foreach (InventoryItem item in inventory.inventoryItems.ToList())
        {
            if (item.item != null)
            {
                if (item.item.Name == name)
                {
                    if (item.quantity >= amountToGather)
                    {
                        Debug.Log(
                            $"There is enough {item.item.Name} in inventory to finish quest!"
                        );
                        status = true;
                    }
                    else
                    {
                        Debug.Log($"Ther is no enough {item.item.Name}!");
                        status = false;
                    }
                }
                else
                {
                    Debug.Log($"Ther is no item named {item.item.Name}!");
                }
            }
            else
            {
                continue;
            }
        }
        return status;
    }

    public void CanFinishSunflowerQuest()
    {
        if (GetCanFinishStatus(item, amount, itemName))
        {
            SetQuestStatus(DialogueManager.GetInstance().GetCurrentStory(), "quest_status", 2);
        }
        else
        {
            Debug.Log("You cnat finish quest just jet");
        }
    }

    public void SetQuestStatus(Story story, string variable, object value)
    {
        if (GetCanFinishStatus(item, amount, itemName))
        {
            Debug.Log(
                $"SetVariablesInsideInk Method is called and value before chane is {story.variablesState[variable]}"
            );
            story.variablesState[variable] = value;
            Debug.Log("Value after change  is: " + story.variablesState[variable]);
        }
    }
}
