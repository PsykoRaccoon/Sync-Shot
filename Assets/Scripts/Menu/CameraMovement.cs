using UnityEngine;

public class CameraMovement : MonoBehaviour
{
   public Transform[] cameraPoints; 
    public float moveSpeed;
    private Transform targetPoint;

    void Update()
    {
        if (targetPoint != null)
        {
            transform.position = Vector3.Lerp(transform.position, targetPoint.position, Time.deltaTime * moveSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetPoint.rotation, Time.deltaTime * moveSpeed);
        }
    }

    public void MoveTo(string pointName)
    {
        foreach (Transform point in cameraPoints)
        {
            if (point.name == pointName)
            {
                targetPoint = point;
                break;
            }
        }
    }
}
