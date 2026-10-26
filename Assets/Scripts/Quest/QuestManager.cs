using System.Collections.Generic;
using Quest;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    void OnEnable()
    {
        QuestEventBus.OnQuestChanged += UpdateQuest;
    }
    
    void OnDisable()
    {
        QuestEventBus.OnQuestChanged -= UpdateQuest;
    }

    private void UpdateQuest(string eventId)
    {
        foreach (KeyValuePair<QuestData, QuestStatus> questInfo in Quest.Quest.Quests)
        {
            if (questInfo.Value.CurrentState != QuestState.Active)
                continue;
            QuestProgress curr = questInfo.Value.CurrentProgress;

            if (curr == null)
                continue;
            int indexOfCurr = questInfo.Key.Progressions.IndexOf(curr);
            if (curr.ObjectiveId == eventId)
            {
                if (indexOfCurr == questInfo.Key.Progressions.Count - 1)
                {
                    questInfo.Value.SetState(QuestState.Complete);
                    return;
                }
                questInfo.Value.SetProgress(questInfo.Key.Progressions[indexOfCurr + 1]);
            }
        }
    }
}