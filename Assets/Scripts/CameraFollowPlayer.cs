using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private Vector3 distance;

    private void Awake()
    {
        distance = transform.position - playerTransform.position;
    }

    private void LateUpdate()
    {
        Move();
    }

    private void Move()
    {
        var camPos = transform.position;
        var newPosition = new Vector3(camPos.x, camPos.y, playerTransform.position.z + distance.z);
        transform.position = newPosition;
    }
}
