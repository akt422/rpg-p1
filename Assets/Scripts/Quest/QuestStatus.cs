using UnityEngine;
// ReSharper disable All

namespace Quest
{
    public class QuestStatus
    {
        private QuestState currentState;
        private QuestProgress currentProgress;

        public QuestStatus(QuestState currentState, QuestProgress currentProgress)
        {
            this.currentState = currentState;
            this.currentProgress = currentProgress;
        }
        
        public QuestState CurrentState => currentState;
        public QuestProgress CurrentProgress => currentProgress;

        public void SetState(QuestState state)
        {
            currentState = state;
        }

        public void SetProgress(QuestProgress progress)
        {
            currentProgress = progress;
        }
    }
}