using System.Collections;
using System.Collections.Generic;
using Inventory;
using Inventory.Model;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    enum StatusOfQuest
    {
        CAN_START,
        IN_PROGRESS,
        CAN_FINISH,
        FINISHED
    }

    [SerializeField]
    private InventorySO inventoryData;

    private static QuestManager instance;

    [SerializeField]
    public List<QuestSO> allQuests = new List<QuestSO>();
    public List<QuestSO> activeQuests = new List<QuestSO>();

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one QuestManager in the scene");
        }
        instance = this;
    }

    public static QuestManager GetInstance()
    {
        return instance;
    }

    public void StartQuest(string questID, int questStatus, bool questActivity, string questType)
    {
        foreach (QuestSO quest in allQuests)
        {
            if (quest.QuestID == questID && questStatus == (int)StatusOfQuest.CAN_START && quest.IsQuestCompleted == false)
            {
                Debug.Log($"You have started quest {questID} quest");

                if (!quest.IsQuestCompleted)
                {
                    if (IsQuestActive(questActivity))
                    {
                        if (activeQuests.Contains(quest))
                        {
                            Debug.Log($"Quest is alredy active {questID}");
                        }
                        else
                        {
                            activeQuests.Add(quest);
                        }

                        if (questType == "GatheringType")
                        {
                            switch (questID)
                            {
                                case "Farmer quest":
                                    FarmersToolsQuest
                                        .GetInstance()
                                        .StartFarmersToolsQuest(
                                            quest.AmountToGather,
                                            quest.ItemForGathering.item.Name
                                        );
                                    break;
                                case "Sunflower quest":
                                    GatheringItemTypeQuest
                                        .GetInstance()
                                        .StartGatheringQuest(
                                            quest.AmountToGather,
                                            quest.ItemForGathering.item.Name
                                        );
                                    break;
                            }
                        }
                        else if (questType == "SlayMpnstersType")
                        {
                            SlayMpnstersTypeQuest
                                .GetInstance()
                                .StartSlayMonsterQuest(quest.EnemyToSlay, quest.AmountToSlay);
                        }
                        else if (questType == "ItemDeliveryType")
                        {
                            ItemDeliveryTypeQuest
                                .GetInstance()
                                .StartItemDelivery(
                                    quest.ItemForDelivering,
                                    quest.AmountToDeliver,
                                    quest.ItemForDelivering.item.Name
                                );
                        }
                        else
                        {
                            Debug.Log("This type of quest does'nt exist");
                        }
                    }
                }
                else
                {
                    Debug.Log($"{questID} is completed.");
                }
            }
            else if (quest.QuestID == questID && questStatus == (int)StatusOfQuest.IN_PROGRESS && quest.IsQuestCompleted == false)
            {
                if (questType == "GatheringType")
                {
                    switch (questID)
                    {
                        case "Farmer quest":
                            FarmersToolsQuest
                                .GetInstance()
                                .GatherItemMethod(
                                    quest.ItemForGathering,
                                    quest.AmountToGather,
                                    quest.ItemForGathering.item.Name
                                );
                            break;
                        case "Sunflower quest":
                            GatheringItemTypeQuest
                                .GetInstance()
                                .GatherItemMethod(
                                    quest.ItemForGathering,
                                    quest.AmountToGather,
                                    quest.ItemForGathering.item.Name
                                );
                            break;
                    }
                }
                else if (questType == "SlayMpnstersType")
                {
                    SlayMpnstersTypeQuest
                        .GetInstance()
                        .StartSlayMonsterQuest(quest.EnemyToSlay, quest.AmountToSlay);
                }
                else if (questType == "ItemDeliveryType")
                {
                    ItemDeliveryTypeQuest
                        .GetInstance()
                        .StartItemDelivery(
                            quest.ItemForDelivering,
                            quest.AmountToDeliver,
                            quest.ItemForDelivering.item.Name
                        );
                }
                else
                {
                    Debug.Log("This type of quest does'nt exist");
                }
            }
        }
    }

    public void FinishQuest(string questID, int questStatus, string rewardName, int rewardAmont)
    {
        foreach (QuestSO quest in allQuests)
        {
            if (questStatus == (int)StatusOfQuest.CAN_FINISH)
            {
                if (questID == quest.QuestID)
                {
                    activeQuests.Remove(quest);
                    quest.IsQuestCompleted = true;
                    if (rewardName == quest.RewardName)
                        inventoryData.GiveReward(rewardName, rewardAmont, quest.RewardItem);

                    Debug.Log($"{questID} is finished");
                }
            }
        }
    }

    private bool IsQuestActive(bool status)
    {
        if (status)
        {
            Debug.Log(status);
            return status;
        }
        else
        {
            Debug.Log(status);
            return status;
        }
    }
}
