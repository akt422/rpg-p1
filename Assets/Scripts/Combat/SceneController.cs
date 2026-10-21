using UnityEngine;

namespace Combat
{
    public class SceneController : MonoBehaviour
    {
        void Start()
        {
            GameStateManager.SetState(GameState.Combat);
            GameObject player =
                GameObject.FindGameObjectWithTag("Player");

            player.GetComponent<PlayerModeController>()
                .SetExplorationActive(false);
        }
    }
}
