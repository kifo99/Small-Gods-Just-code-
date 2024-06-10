using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class SlayMpnstersTypeQuest : MonoBehaviour
{
    public int slayMonsterAmount = 0;
    private static SlayMpnstersTypeQuest instance;

    private string name;
    private int amount = 0;
    private bool status = false;

    
    private void Awake() {
        if(instance != null)
        {
            Debug.LogError("There is more than one instance of SlayMonstersTypeQuest");
        }
        instance = this;
    }

    private void Update() 
    {
        //PrintDictionary();
    }
    public static SlayMpnstersTypeQuest GetInstance()
    {
        return instance;
    }

    public void StartSlayMonsterQuest(string monsterName, int amountToSlay)
    {
        if(MonsterSlayCount.GetInstance().monstersDictionary != null)
        {
            MonsterSlayCount.GetInstance().ChangeStatus();
        }

        name = monsterName;
        amount = amountToSlay;
    }

    public void FinishSlayMonstersQuest()
    {
         Debug.Log($"Method is called and status is {status} You have to  kille {amount} of {name}");
         foreach(var monster in MonsterSlayCount.GetInstance().monstersDictionary)
         {
            if(monster.Key == name)
            {
                Debug.Log($"Ther is {monster.Key} key in dictionary and value is {monster.Value}");
                if(monster.Value >= amount)
                {
                    status = true;
                    Debug.Log($"You have kiled {amount} of {name}");
                    Debug.Log($"You can finish SlayMonstersTypeQuest and status is {status}");
                    SetQuestStatus(DialogueManager.GetInstance().GetCurrentStory(),"quest_status", 2);
                    TeleportTrigger.instance.SetCanFinish(true);
                }
            }
            else
            {
                Debug.Log($"Ther is no {name} key in dictionary");
            }
         }
       
    }


    public void SetQuestStatus(Story story, string variable, object value)
    {
        Debug.Log($"SetQuestStatus Method is called and status is {status}");

        if (status)
        {
            Debug.Log($"SetVariablesInsideInk Method is called and value before chane is {story.variablesState[variable]}");
            story.variablesState[variable] = value;
            Debug.Log("Value after change  is: " + story.variablesState[variable]);
        }
    }

    
}
