using Inventory.Model;
using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Inventory
{
    public class InventoryController : MonoBehaviour, IDataPersistence
    {
        [SerializeField]
        private UIInventoryPage inventoryPage;

        [SerializeField]
        public InventorySO inventoryData;

        public List<InventoryItem> initialItems = new List<InventoryItem>();
        

        [SerializeField]
        private GameObject spellPanel;


        private void Start()
        {
            PreperUI();
            PreperInventoryData();
        }

        private void PreperInventoryData()
        {
            //inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItem item in initialItems)
            {
                if (item.IsEmpty)
                    continue;

                inventoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryPage.ResetAllItems();
            foreach (var item in inventoryState)
            {
                inventoryPage.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }

        private void PreperUI()
        {
            inventoryPage.InitializeInventoryUI(inventoryData.Size);
            inventoryPage.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryPage.OnSwapItems += HandleSwapItems;
            inventoryPage.OnStartDragging += HandleDragging;
            inventoryPage.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleItemActionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                inventoryPage.ShowItemAction(itemIndex);
                inventoryPage.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
            }

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryPage.AddAction("Drop", () => DropItem(itemIndex, inventoryItem.quantity));
            }
        }

        private void DropItem(int itemIndex, int quantity)
        {
            inventoryData.RemoveItem(itemIndex, quantity);
            inventoryPage.ResetSelection();
        }

        public void PerformAction(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryData.RemoveItem(itemIndex, 1);
            }

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                itemAction.PerformAction(gameObject, inventoryItem.itemState);
                if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                    inventoryPage.ResetSelection();
            }
        }

        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            inventoryPage.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
        }

        private void HandleSwapItems(int itemIndex1, int itemIndex2)
        {
            inventoryData.SwapItems(itemIndex1, itemIndex2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                inventoryPage.ResetSelection();
                return;
            }

            SOItem item = inventoryItem.item;
            string description = PrepareDescription(inventoryItem);
            inventoryPage.UpdateDescription(itemIndex, item.ItemImage, item.Name, description);
        }

        private string PrepareDescription(InventoryItem inventoryItem)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(inventoryItem.item.Description);
            sb.AppendLine();

            for (int i = 0; i < inventoryItem.itemState.Count; i++)
            {
                sb.Append(
                    $"{inventoryItem.itemState[i].itemParameter.ParameterName} : {inventoryItem.itemState[i].value} / {inventoryItem.item.DefaultParametarsList[i].value}"
                );
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private void Update()
        {
            OpenInventoryPageUI();
        }

        private void OpenInventoryPageUI()
        {
            if (InputManager.GetInstance().GetOpenInventoryPressed())
            {
                if (inventoryPage.isActiveAndEnabled == false)
                {
                    this.gameObject.GetComponent<Spell>().enabled = false;
                    this.gameObject.GetComponent<SpellController>().enabled = false;
                    inventoryPage.Show();
                    spellPanel.SetActive(false);
                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        inventoryPage.UpdateData(
                            item.Key,
                            item.Value.item.ItemImage,
                            item.Value.quantity
                        );
                    }
                }
                else
                {
                    
                    this.gameObject.GetComponent<Spell>().enabled = true;
                    this.gameObject.GetComponent<SpellController>().enabled = true;
                    spellPanel.SetActive(true);
                    inventoryPage.Hide();
                }
            }
        }

        public void LoadData(GameData data)
        {
            inventoryData.Initialize();
            Debug.Log("InventoryController LoadData is called");
            
            //inventoryData.inventoryItems = data.items;
            foreach(InventoryItem item in data.items)
            {
                if(item.item != null)
                {
                    inventoryData.AddItem(item);
                    Debug.Log($"Data is: {item.item.Name}");
                }
                else
                {
                    continue;
                }
            }
        }

        public void SaveData(ref GameData data)
        {
            data.items = inventoryData.inventoryItems;
        }
    }
}
