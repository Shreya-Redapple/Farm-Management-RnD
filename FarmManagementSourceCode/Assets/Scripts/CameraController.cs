using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 10f;  // Speed of panning
    public float zoomSpeed = 5f;  // Speed of zooming
    public float rotateSpeed = 80f; // Speed of Rotation
    public float minZoom = 10f;    // Minimum zoom distance
    public float maxZoom = 50f;    // Maximum zoom distance
    private Vector3 dragOrigin;
   

    void Update()
    {
        // Pan
        Pan();
        // Zoom the camera
        Zoom();
        // Rotate
        Rotate();

    }

    private void Rotate()
    {
        if (Input.GetMouseButton(2)) // Middle mouse button
        {
            float rotX = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
            float rotY = Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;

            transform.Rotate(Vector3.up, rotX, Space.World);
            transform.Rotate(Vector3.left, rotY, Space.Self);
        }
    }

    private void Zoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            Camera.main.fieldOfView -= scroll * zoomSpeed * 100f * Time.deltaTime;
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, minZoom, maxZoom);
        }
    }

    private void Pan()
    {
        if (Input.GetMouseButtonDown(1)) // Right mouse button
        {
            dragOrigin = Input.mousePosition;
        }
        if (Input.GetMouseButton(1))
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 move = new Vector3(pos.x * panSpeed, pos.y * panSpeed, 0);
            transform.Translate(move, Space.World);
            dragOrigin = Input.mousePosition;
        }
    }
   
 
   
 

}


