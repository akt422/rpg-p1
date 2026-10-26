using System;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{
    public static class Quest
    {
        private static Dictionary<QuestData, QuestStatus> quests = new Dictionary<QuestData, QuestStatus>();
        public static event Action OnQuestChanged;

        public static void AddQuest(QuestData questData)
        {
            if (!quests.TryAdd(questData, new QuestStatus(QuestState.Active, questData.Progressions[0]))) return;
            OnQuestChanged?.Invoke();
        }
        
        public static Dictionary<QuestData, QuestStatus> Quests => quests;

        public static bool DoesStateExist(QuestData questData)
        {
            return quests.ContainsKey(questData);
        }

        public static void SetQuestState(QuestData questData, QuestState state)
        {
            if (!quests.ContainsKey(questData)) return;
            quests[questData].SetState(state);
            OnQuestChanged?.Invoke();
        }

        public static QuestState GetQuestState(QuestData questData)
        {
            if (!quests.ContainsKey(questData)) return QuestState.Invalid;
            return quests[questData].CurrentState;
        }

        public static void RemoveQuest(QuestData questData)
        {
            if (!quests.ContainsKey(questData)) return;
            quests.Remove(questData);
            OnQuestChanged?.Invoke();
        }

        public static void SetQuestProgress(QuestData questData, QuestProgress progress)
        {
            if (!quests.ContainsKey(questData)) return;
            quests[questData].SetProgress(progress);
        }

        public static QuestProgress GetQuestProgress(QuestData questData)
        {
            if (!quests.ContainsKey(questData)) return null;
            return quests[questData].CurrentProgress;
        }
    }
}