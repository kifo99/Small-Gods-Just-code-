using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPietySystem : MonoBehaviour
{
    private int playerPiety = 0;

    [SerializeField]
    private int maxPiety = 100;

    [SerializeField]
    private PietyBar pietyBar;
    private void Awake()
    {
        playerPiety = maxPiety;
        pietyBar.SetPiety(maxPiety);
    }
    public void UpdatePlayerPiety(int amount)
    {
        playerPiety += amount;

        pietyBar.SetPiety(playerPiety);

        if (playerPiety > maxPiety)
        {
            playerPiety = maxPiety;
            
        }
        else if (playerPiety <= 0f)
        {
            playerPiety = 0;
            
        }
    }

    public int GetMaxPiety()
    {
        return maxPiety;
    }
    public int GetPlayerPiety()
    {
        return playerPiety;
    }

    public void LoadData(GameData data)
    {
        this.playerPiety = data.playerPiety;
        pietyBar.SetPiety(playerPiety);
    }

    public void SaveData(ref GameData data)
    {
        data.playerPiety = this.playerPiety;
        pietyBar.SetPiety(playerPiety);
    }
}
