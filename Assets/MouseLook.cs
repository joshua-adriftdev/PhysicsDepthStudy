using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform parent; 
    public Transform target;
    public float distance = 10.0f; 
    public float minDistance = 5.0f; 
    public float maxDistance = 20.0f; 
    public float zoomSpeed = 2.0f; 

    public float xSpeed = 120.0f; 
    public float ySpeed = 120.0f; 
    public float sensitivity = 1.0f;

    private float x = 0.0f;
    private float y = 0.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        // Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {

        if (target && parent)
        {
            if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                float xSpeed = this.xSpeed / StateManager.instance.timeScale;
                float ySpeed = this.ySpeed / StateManager.instance.timeScale;
                x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime * sensitivity;
                y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime * sensitivity;
            }

            y = Mathf.Clamp(y, -90, 90);
            Quaternion localRotation = Quaternion.Euler(y, x, 0);

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            distance -= scroll * zoomSpeed;
            distance = Mathf.Clamp(distance, minDistance, maxDistance);

            Quaternion finalRotation = parent.rotation * localRotation;

            Vector3 position = target.position - (finalRotation * Vector3.forward * distance);

            transform.rotation = finalRotation;
            transform.position = position;
        }
    }
}
