using System;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{
    public class QuestController : MonoBehaviour
    {
        [SerializeField] private GameObject questPrefab;

        private void OnEnable()
        {
            Quest.OnQuestChanged += RefreshQuests;
            RefreshQuests();
        }

        private void OnDisable()
        {
            Quest.OnQuestChanged -= RefreshQuests;
        }

        public void CreateQuest(QuestData data, QuestState state)
        {
            GameObject quest = Instantiate(questPrefab, transform);
            QuestDataUI questUI = quest.GetComponent<QuestDataUI>();
            questUI.SetUp(data, state);
            Debug.Log("Quest set up with " + data.Title);
        }

        private void RefreshQuests()
        {
            ClearQuests();
            foreach (KeyValuePair<QuestData, QuestState> quest in Quest.Quests)
            {
                CreateQuest(quest.Key, quest.Value);
            }
        }

        private void ClearQuests()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}