using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CinemachineConfiner2D))]
public class CinemachineController : MonoBehaviour
{
    private CinemachineConfiner2D confiner;
    private Scene boundsScene;
    private bool hasBoundsScene;

    void Awake()
    {
        confiner = GetComponent<CinemachineConfiner2D>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += SubscribeBounds;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= SubscribeBounds;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    void SubscribeBounds(Scene scene, LoadSceneMode mode)
    {
        CameraBoundsMarker marker = null;

        // Search only the scene that was just loaded.
        foreach (GameObject root in scene.GetRootGameObjects())
        {
            marker = root.GetComponentInChildren<CameraBoundsMarker>(true);

            if (marker != null)
                break;
        }

        if (marker == null)
        {
            // Additive utility scenes shouldn't replace current bounds.
            if (mode == LoadSceneMode.Single)
            {
                hasBoundsScene = false;
                SetBounds(null);
            }

            return;
        }

        PolygonCollider2D bounds =
            marker.GetComponent<PolygonCollider2D>();

        if (bounds == null)
        {
            Debug.LogWarning($"CameraBounds in {scene.name} has no PolygonCollider2D.");

            if (mode == LoadSceneMode.Single)
            {
                hasBoundsScene = false;
                SetBounds(null);
            }

            return;
        }

        boundsScene = scene;
        hasBoundsScene = true;
        SetBounds(bounds);
    }

    void OnSceneUnloaded(Scene scene)
    {
        if (!hasBoundsScene || scene != boundsScene)
            return;

        hasBoundsScene = false;
        SetBounds(null);
    }

    private void SetBounds(Collider2D bounds)
    {
        confiner.BoundingShape2D = bounds;
        confiner.enabled = bounds != null;

        if (bounds != null)
            confiner.InvalidateBoundingShapeCache();
    }
}