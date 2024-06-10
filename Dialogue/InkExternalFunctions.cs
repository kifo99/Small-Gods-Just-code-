using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class InkExternalFunctions
{
    public void Bind(Story story)
    {
        story.BindExternalFunction(
            "startQuest",
            (string questID, int questStatus, bool questActivity, string questType) =>
            {
                QuestManager
                    .GetInstance()
                    .StartQuest(questID, questStatus, questActivity, questType);
            }
        );

        story.BindExternalFunction(
            "finishQuest",
            (string questID, int questStatus, string rewardName, int rewardAmount) =>
            {
                QuestManager
                    .GetInstance()
                    .FinishQuest(questID, questStatus, rewardName, rewardAmount);
            }
        );

        story.BindExternalFunction(
            "finishGatheringQuest",
            () =>
            {
                GatheringItemTypeQuest.GetInstance().FinishGatheringQuest();
            }
        );

        story.BindExternalFunction(
            "canFinishSunflowerQuest",
            () =>
            {
                GatheringItemTypeQuest.GetInstance().CanFinishSunflowerQuest();
            }
        );
        story.BindExternalFunction(
            "finishFarmersToolQuest",
            () =>
            {
                FarmersToolsQuest.GetInstance().FinishFarmersToolsQuest();
            }
        );

        story.BindExternalFunction(
            "canFinishFarmersQuest",
            () =>
            {
                FarmersToolsQuest.GetInstance().CanFinishFarmersQuest();
            }
        );

        story.BindExternalFunction(
            "deliver",
            () =>
            {
                ItemDeliveryTypeQuest.GetInstance().Deliver();
            }
        );

        story.BindExternalFunction(
            "finishSlayMonsterQuest",
            () =>
            {
                SlayMpnstersTypeQuest.GetInstance().FinishSlayMonstersQuest();
            }
        );
    }

    public void Unbind(Story story)
    {
        
        story.UnbindExternalFunction("startQuest");
        story.UnbindExternalFunction("finishQuest");
        story.UnbindExternalFunction("canFinishSunflowerQuest");
        story.UnbindExternalFunction("finishGatheringQuest");
        story.UnbindExternalFunction("canFinishFarmersQuest");
        story.UnbindExternalFunction("finishFarmersToolQuest");
        story.UnbindExternalFunction("finishSlayMonsterQuest");
    }
}
