using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {

        [SerializeField]
        public List<InventoryItem> inventoryItems;

        [field: SerializeField]
        public int Size { get; set; } = 10;

        public int StackSize = 0;

        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;

        

        public void Initialize()
        {
            inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }

      
        public int AddItem(SOItem item, int quantity, List<ItemParameter> itemState = null)
        {
            Debug.Log("AddItem is called");
            if (item.IsStackable == false)
            {
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    while (quantity > 0 && IsInventoryFull() == false)
                    {
                        quantity -= AddItemToFirstEmptySlot(item, 1, itemState);
                    }
                    InformAboutChange();
                    StackSize = quantity;
                    return quantity;
                }
            }
            quantity = AddStackableItem(item, quantity);
            InformAboutChange();
            StackSize = quantity;
            return quantity;
        }

        private int AddItemToFirstEmptySlot(SOItem item, int quantity, List<ItemParameter> itemState = null)
        {
            InventoryItem newItem = new InventoryItem { item = item, quantity = quantity,itemState =new List<ItemParameter>(itemState == null ? item.DefaultParametarsList : itemState)};
            
            Debug.Log($"AddItemToFirstEmptySlot is called: {item.Name}, {quantity}");
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = newItem;
                    Debug.Log($"InventoryItem  is {inventoryItems[i].item.Name}, and quantity is {quantity}");
                    return quantity;
                    
                }
            }
            return 0;
        }

        private bool IsInventoryFull() => inventoryItems.Where(item => item.IsEmpty).Any() == false;

        private int AddStackableItem(SOItem item, int quantity)
        {
            Debug.Log($"AddStackableItem is called: {item.Name}, {quantity}");
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                    continue;

                if (inventoryItems[i].item.ID == item.ID)
                {
                    int posibleAmount =
                        inventoryItems[i].item.MaxStackSize - inventoryItems[i].quantity;

                    if (quantity > posibleAmount)
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(
                            inventoryItems[i].item.MaxStackSize
                        );

                        quantity -= posibleAmount;
                        Debug.Log($"InventoryItem  is {inventoryItems[i].item.Name}, and quantity is {quantity}");
                    }
                    else
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(
                            inventoryItems[i].quantity + quantity
                        );
                        InformAboutChange();
                        Debug.Log($"InventoryItem  is {inventoryItems[i].item.Name}, and quantity is {quantity}");
                        return 0;
                    }
                }
            }
            while (quantity > 0 && IsInventoryFull() == false)
            {
                int newQuantity = Math.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstEmptySlot(item, newQuantity);
            }

            return quantity;
        }
        public void GiveReward(string rewardName, int rewardAmount, InventoryItem item)
        {
            if(item.item.Name == rewardName)
            {
                Debug.Log("GiveReward called");
                AddItem(item.item,rewardAmount);

            }
        }

        public void AddItem(InventoryItem item)
        {
            
            AddItem(item.item, item.quantity);
            Debug.Log($"Added item {item.item.Name}");
        }

        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    continue;
                }
                returnValue[i] = inventoryItems[i];
            }
            return returnValue;
        }

        public InventoryItem GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }

        public void SwapItems(int itemIndex1, int itemIndex2)
        {
            InventoryItem item1 = inventoryItems[itemIndex1];
            inventoryItems[itemIndex1] = inventoryItems[itemIndex2];
            inventoryItems[itemIndex2] = item1;
            InformAboutChange();
        }

        private void InformAboutChange()
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }

        public void RemoveItem(int itemIndex, int amount)
        {
            Debug.Log("Remove item was called ");
            if (inventoryItems.Count > itemIndex)
            {
                Debug.Log($"Item index is {itemIndex}");
                if (inventoryItems[itemIndex].IsEmpty)
                {
                    Debug.Log("Item is empty");
                    return;
                }
                int reminder = inventoryItems[itemIndex].quantity - amount;
                if (reminder <= 0)
                {
                    Debug.Log($"Reminder is {reminder} and item is: {inventoryItems[itemIndex].item.Name}");
                    inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
                }
                else
                {
                    Debug.Log($"Reminder is {reminder} and item is: {inventoryItems[itemIndex].item.Name}");
                    inventoryItems[itemIndex] = inventoryItems[itemIndex].ChangeQuantity(reminder);
                }

                InformAboutChange();
            }
        }
    }

    [Serializable]
    public struct InventoryItem 
    {
        public int quantity;
        public SOItem item;
        public List<ItemParameter> itemState;

        public bool IsEmpty => item == null;

        public InventoryItem ChangeQuantity(int newQuantity)
        {
            return new InventoryItem 
            { 
                item = this.item, 
                quantity = newQuantity, 
                itemState = new List<ItemParameter>(this.itemState),
            };
        }

        public static InventoryItem GetEmptyItem() =>
            new InventoryItem { item = null, quantity = 0, itemState = new List<ItemParameter>() };
    }
}
