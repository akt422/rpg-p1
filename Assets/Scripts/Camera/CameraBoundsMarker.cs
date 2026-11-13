using UnityEngine;

public class CameraBoundsMarker : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D polygonCollider2D;
    
    public PolygonCollider2D PolygonCollider2D => polygonCollider2D;
}