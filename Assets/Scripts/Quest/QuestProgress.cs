using Quest;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestProgress", menuName = "Quest System/QuestProgress")]
public class QuestProgress : ScriptableObject
{
    [SerializeField] private string objectiveId;
    [SerializeField] private string objectiveName;
    
    public string ObjectiveId => objectiveId;
    public string ObjectiveName => objectiveName;
}