using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button button;
    public Transform target;
    public float panSpeed = 2.0f;

    private bool panCamera = false;
    public Vector3 targetPosition;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        //button.onClick.AddListener(() => StartCameraPan());
        //targetPosition = target.position;
    }

    void Update()
    {
        if (panCamera)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition, panSpeed * Time.deltaTime);
            float distance = Vector3.Distance(cam.transform.position, targetPosition);
            if (distance < 0.1f)
            {
                cam.transform.position = targetPosition;
                panCamera = false;
            }
        }
    }

    public void StartCameraPan()
    {
        panCamera = true;
    }
}
