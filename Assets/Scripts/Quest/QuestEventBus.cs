using System;

public static class QuestEventBus
{
    public static event Action<string> OnQuestChanged;
    
    public static void RaiseEvent(string eventId)
    {
        OnQuestChanged?.Invoke(eventId);
    }
}