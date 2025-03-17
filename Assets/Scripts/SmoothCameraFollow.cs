using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private float distance = 2f;
    [SerializeField] private float height = 1.5f;
    [SerializeField] private float rotationDamping = 8f;
    [SerializeField] private float positionDamping = 6f;

    private Vector3 smoothedForward;

    private void LateUpdate()
    {
        Vector3 targetForward = target.forward;
        smoothedForward = Vector3.Slerp(smoothedForward, targetForward, Time.deltaTime * rotationDamping);
        Vector3 horizontalOffset = -smoothedForward * distance;
        Vector3 verticalOffset = Vector3.up * height;
        Vector3 desiredPosition = target.position + horizontalOffset + verticalOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, positionDamping * Time.deltaTime);
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationDamping * Time.deltaTime);
    }
}
