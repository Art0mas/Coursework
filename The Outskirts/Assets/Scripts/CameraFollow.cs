using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    [Range(0.01f, 1f)]
    [SerializeField] private float smoothTime = 0.2f;

    [SerializeField] private float upThreshold = 2f;
    [SerializeField] private float downThreshold = 2f;

    [SerializeField] private float minY = 0f;
    [SerializeField] private float maxY = 100f;

    private Vector3 velocity = Vector3.zero;
    private float currentTargetY;

    void Start()
    {
        if (target != null)
        {
            currentTargetY = target.position.y;
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            if (target.position.y > currentTargetY + upThreshold)
            {
                currentTargetY = target.position.y - upThreshold;
            }
            else if (target.position.y < currentTargetY - downThreshold)
            {
                currentTargetY = target.position.y + downThreshold;
            }

            currentTargetY = Mathf.Clamp(currentTargetY, minY, maxY);
            Vector3 desiredPosition = new Vector3(target.position.x, currentTargetY, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 center = transform.position;

        Gizmos.DrawLine(new Vector3(center.x - 5, center.y + upThreshold, center.z),
                        new Vector3(center.x + 5, center.y + upThreshold, center.z));
        Gizmos.DrawLine(new Vector3(center.x - 5, center.y - downThreshold, center.z),
                        new Vector3(center.x + 5, center.y - downThreshold, center.z));

        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(center.x - 10, minY, center.z),
                        new Vector3(center.x + 10, minY, center.z));
    }
}