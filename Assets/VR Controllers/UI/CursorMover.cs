using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorMover : MonoBehaviour
{

    public InputActionReference actionReference;

    private Vector3 tempVector;

    private float vertical;
    private float horizontal;
    // Start is called before the first frame update
    void Start()
    {
        tempVector = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().localPosition = Quaternion.AngleAxis(45, Vector3.forward)  * actionReference.action.ReadValue<Vector2>() * 400;
        /*tempVector.y += Input.GetAxis("Vertical") * Time.deltaTime;
        tempVector.x += Input.GetAxis("Horizontal") * Time.deltaTime;
        GetComponent<RectTransform>().localPosition = Quaternion.AngleAxis(45, Vector3.forward) * tempVector*400;*/
    }
}
