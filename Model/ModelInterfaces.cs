using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    public interface IDestroyableItem { }

    public interface IItemAction
    {
        public string ActionName { get; }

        bool PerformAction(GameObject character, List<ItemParameter> itemState);
    }
}
