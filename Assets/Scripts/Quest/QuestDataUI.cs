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

        public void SetUp(QuestData questData, QuestState state)
        {
            iconImage.sprite = questData.Icon;
            title.text = questData.Title;
            status.text = nameof(state);
            description.text = questData.Description;
        }
    }
}