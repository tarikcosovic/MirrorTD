using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float panSpeed = 30f;
    [SerializeField]
    float panBorderOffset = 50f;
    [SerializeField]
    float zoomSpeed = 1f;
    [SerializeField]
    float zoomLength = 5f;
    [SerializeField]
    float minimumZoom = 10f;
    [SerializeField]
    float maximumZoom = 50f;

    private bool zoomEnabled = false;
    private float elapsedTime = 0f;

    private float currentZoomPosition;
    private float nextZoomPosition;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panBorderOffset)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorderOffset)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBorderOffset)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorderOffset)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }

        //Zoom functionality
        if (zoomEnabled)
        {
            elapsedTime += Time.deltaTime / zoomSpeed;
            Camera.main.orthographicSize = Mathf.Lerp(currentZoomPosition, nextZoomPosition, elapsedTime);
            if (elapsedTime >= 1.0f)
            {
                elapsedTime = 0;
                zoomEnabled = false;
            }
        }
        else
        {
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            if (scrollInput != 0)
            {
                if (scrollInput > 0)
                {
                    zoomLength = Mathf.Abs(zoomLength);
                    if (Camera.main.orthographicSize - zoomLength < minimumZoom)
                        return;
                }
                else if (scrollInput < 0)
                {
                    zoomLength = -Mathf.Abs(zoomLength);
                    if (Camera.main.orthographicSize - zoomLength > maximumZoom)
                        return;
                }

                currentZoomPosition = Camera.main.orthographicSize;
                nextZoomPosition = currentZoomPosition - zoomLength;

                zoomEnabled = true;
            }
        }
    }
}
