using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderRoom : MonoBehaviour
{
    [SerializeField] private string targetScene;
    [SerializeField] private string targetSpawnPoint;
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            // Scene currScene = SceneManager.GetActiveScene();
            SceneTransitionData.targetSpawnPoint = targetSpawnPoint;
            // Debug.Log(SceneTransitionData.targetSpawnPoint + " ---- " + targetSpawnPoint);
            SceneTransitionManager.Instance.LoadScene(targetScene);
        }
    }
}
