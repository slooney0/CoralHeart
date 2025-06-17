using UnityEngine;

public class CameraScript : MonoBehaviour
{

    private Vector3 offset = new Vector3(0f, 0f, -10f);

    private float smoothingRate = 0.25f;

    private Vector3 vel = Vector3.zero;

    [SerializeField] Transform target;

    void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, smoothingRate);
    }
}
