using System.Collections.Generic;
using UnityEngine;

namespace Quest
{
    [CreateAssetMenu(fileName = "QuestData", menuName = "Quest Data/Quest")]
    public class QuestData : ScriptableObject
    {
        [SerializeField] private string questId;
        [SerializeField] private string title;
        [SerializeField] private string description;
        [SerializeField] private Sprite icon;
        [SerializeField] private List<QuestProgress> progressions;

        public string QuestId => questId;
        public string Title => title;
        public string Description => description;
        public Sprite Icon => icon;
        public List<QuestProgress> Progressions => progressions;
    }
}