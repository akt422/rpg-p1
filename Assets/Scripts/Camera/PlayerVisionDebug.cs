using UnityEngine;

public class PlayerVisionDebug : MonoBehaviour
{
    [SerializeField] private LayerMask visionBlockers;
    [SerializeField] private float visionRadius = 5f;
    [SerializeField, Range(30, 360)]
    private int rayCount = 180;

    void Update()
    {
        Vector2 origin = transform.position;

        for (int i = 0; i < rayCount; i++)
        {
            float angle = i * 360f / rayCount;
            float radians = angle * Mathf.Deg2Rad;

            Vector2 direction = new Vector2(
                Mathf.Cos(radians),
                Mathf.Sin(radians)
            );

            RaycastHit2D hit = Physics2D.Raycast(
                origin,
                direction,
                visionRadius,
                visionBlockers
            );

            Vector2 endpoint = hit.collider
                ? hit.point
                : origin + direction * visionRadius;

            // Debug.DrawLine(
            //     origin,
            //     endpoint,
            //     hit.collider ? Color.red : Color.green,
            //     0f,
            //     false
            // );
        }
    }
}