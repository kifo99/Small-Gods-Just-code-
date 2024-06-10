using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public long lastUpdate;
    public int playerHealth;
    public int playerPiety;
    public string globalVariablesStateJson;

    public List<InventoryItem> items;

    public GameData()
    {
        this.playerHealth = 100;
        this.playerPiety = 100;
        this.globalVariablesStateJson = "";
        items = new List<InventoryItem>();
    }
}
