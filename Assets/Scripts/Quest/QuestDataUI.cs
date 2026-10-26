using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace Quest
{
    public class QuestDataUI : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text status;
        [SerializeField] private TMP_Text description;
        [SerializeField] private TMP_Text progression;

        public void SetUp(QuestData questData, QuestStatus questStatus)
        {
            iconImage.sprite = questData.Icon;
            title.text = questData.Title;
            status.text = questStatus.CurrentState.ToString();
            description.text = questData.Description;
            progression.text = questStatus.CurrentProgress.ObjectiveName;
        }
    }
}