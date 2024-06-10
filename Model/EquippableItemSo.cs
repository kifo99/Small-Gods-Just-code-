using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{

    [CreateAssetMenu]
    public class EquippableItemSo : SOItem, IDestroyableItem, IItemAction
    {
        public string ActionName => "Equip";

        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
           AgentWeaponScript weaponSystem = character.GetComponent<AgentWeaponScript>();   
           if(weaponSystem != null)
           {
                weaponSystem.SetWeapon(this, itemState == null ? DefaultParametarsList : itemState);
                return true;
           }
           return false;
        }
    }
}

