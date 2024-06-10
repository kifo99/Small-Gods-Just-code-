using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterSlayCount : MonoBehaviour
{
    private int monsterSlayn = 0;
    private string monsterName;
    private bool  canAdd = false;

    private static MonsterSlayCount instance;


    public Dictionary<string, int> monstersDictionary = new Dictionary<string, int>();


    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("There is more than one instance of MonsterSlayCount");
        }
        instance = this;
    }

    public static MonsterSlayCount GetInstance()
    {
        return instance;
    }

    public void StartCount(string _monsterName)
    {
        monsterSlayn += 1;
        monsterName = _monsterName;
    }
    public void AddToMonstersDictionary()
    {
        
        Debug.Log($"Method is called and status is {canAdd}");
        if(canAdd)
        {
             Debug.Log($"Method is called and status is {canAdd}");
            if(monstersDictionary != null)
            {
                if(monstersDictionary.ContainsKey(monsterName))
                {
                    monstersDictionary[monsterName] = monsterSlayn;
                }
                else
                {
                    monstersDictionary.Add(monsterName, monsterSlayn);
                }
        }
        else
        {
            Debug.Log("Dictionary is null");
        }
        }
        else
        {
            Debug.Log("Cant add jet");
        }
        
        
    }
   

    public void ChangeStatus()
    {
        canAdd = true;
        Debug.Log($"Method is called and status is {canAdd}"); 
    }

   
}
