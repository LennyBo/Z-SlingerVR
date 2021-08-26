using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Transform>();
    }

    private float xRotation = 0f;
    private float sensi = 200f;
    // Update is called once per frame
    void Update()
    {
        float move_sensi = 4f;

        float vertical = Input.GetAxis("Vertical")*move_sensi;
        float horizontal = Input.GetAxis("Horizontal")*move_sensi;
        Vector3 vec = new Vector3(horizontal, 0, vertical);
        
        float mouseX = Input.GetAxis("Mouse X") * sensi * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensi * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        camera.Rotate(Vector3.up * mouseX);
        camera.Rotate(Vector3.right * -mouseY);
        camera.Translate(vec * Time.deltaTime);
    }
}
