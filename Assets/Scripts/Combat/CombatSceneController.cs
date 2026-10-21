using UnityEngine;
using UnityEngine.InputSystem;

public static class CombatSceneController
{
    private static int underlings = 3;
    public static void HitObject(GameObject obj, int  damage)
    {
        PlayerCombatObject support = obj.GetComponent<PlayerCombatObject>();
        support.TakeDamage(damage);
        if (support.Health <= 0)
        {
            EndGame();
        }
    }

    public static void killUnderling()
    {
        underlings--;
        if (underlings <= 0) 
        {
            EndGame();
        }
    }
    private static void EndGame()
    {
        GameObject.FindGameObjectWithTag("Player")?.SetActive(true);
        SceneTransitionData.targetSpawnPoint = "MainDoor";
        SceneTransitionManager.Instance.LoadScene("Sengali_mainhouse");
        GameStateManager.SetState(GameState.Gameplay);
        GameObject player =
            GameObject.FindGameObjectWithTag("Player");

        player.GetComponent<PlayerModeController>()
            .SetExplorationActive(true);
    }
}