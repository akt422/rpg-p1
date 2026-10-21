using System;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{
    public static class Quest
    {
        private static Dictionary<QuestData, QuestState> quests = new Dictionary<QuestData, QuestState>();
        public static event Action OnQuestChanged;

        public static void AddQuest(QuestData questData)
        {
            if (!quests.TryAdd(questData, QuestState.Active)) return;
            OnQuestChanged?.Invoke();
        }
        
        public static Dictionary<QuestData, QuestState> Quests => quests;

        public static void SetQuestState(QuestData questData, QuestState state)
        {
            if (!quests.ContainsKey(questData)) return;
            quests[questData] = state;
            OnQuestChanged?.Invoke();
        }

        public static QuestState GetQuestState(QuestData questData)
        {
            if (!quests.ContainsKey(questData)) return QuestState.Invalid;
            return quests[questData];
        }

        public static void RemoveQuest(QuestData questData)
        {
            if (!quests.ContainsKey(questData)) return;
            quests.Remove(questData);
            OnQuestChanged?.Invoke();
        }
    }
}