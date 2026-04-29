using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0f, 5f, -7f);
    public float rotationSpeed = 100f;
    public float smoothSpeed = 0.125f;
    public float minDistance = 1.0f;
    public float maxDistance = 10.0f;
    public LayerMask obstacleLayer;

    private float currentX = 0f;
    private float currentY = 0f;
    private float yAngleMin = -40f;
    private float yAngleMax = 180f;
    private float zoomSpeed = 2f;

    private void LateUpdate()
    {
        currentX += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        currentY -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        currentY = Mathf.Clamp(currentY, yAngleMin, yAngleMax);

        float scrollInput = -Input.GetAxis("Mouse ScrollWheel");
        offset += offset.normalized * scrollInput * zoomSpeed;

        float currentDistance = offset.magnitude;
        if (currentDistance < minDistance)
        {
            offset = offset.normalized * minDistance;
        }
        else if (currentDistance > maxDistance)
        {
            offset = offset.normalized * maxDistance;
        }

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 desiredPosition = player.position + rotation * offset;

        if (Physics.Raycast(player.position, (desiredPosition - player.position).normalized, out RaycastHit hit, offset.magnitude, obstacleLayer))
        {
            desiredPosition = hit.point - (desiredPosition - player.position).normalized * 0.2f;
        }

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.LookAt(player);
    }
}
