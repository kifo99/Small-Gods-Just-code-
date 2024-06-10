using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

[CreateAssetMenu]
public class QuestSO : ScriptableObject
{
    [Header("Quest ID")]
    [SerializeField]
    public string QuestID;

    [Header("Quest Description")]
    [SerializeField]
    public string QuestType;

    [SerializeField]
    [TextArea]
    public string QuestDescription;

    [Header("Reward")]
    [SerializeField]
    public string RewardName;

    [SerializeField]
    public InventoryItem RewardItem;

    [Header("QuestStatus")]
    [SerializeField]
    public int QuestStatus;

    [SerializeField]
    public bool IsQuestCompleted;

    [Header("Data for Gathering type quest")]
    [SerializeField]
    public InventoryItem ItemForGathering;

    [SerializeField]
    public int AmountToGather;

    [Header("Data for Delivering item type quest")]
    [SerializeField]
    public InventoryItem ItemForDelivering;

    [SerializeField]
    public int AmountToDeliver;

    [Header("Data for Slaying monsters type quest")]
    [SerializeField]
    public string EnemyToSlay;

    [SerializeField]
    public int AmountToSlay;
}
