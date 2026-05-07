using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float fixedY = 0f;

    [Range(0.01f, 1f)]
    [SerializeField] private float smoothTime = 0.2f; 

    private Vector3 velocity = Vector3.zero; 

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = new Vector3(target.position.x, fixedY, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        }
    }
}