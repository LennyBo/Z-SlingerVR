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
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().localPosition = Quaternion.AngleAxis(45, Vector3.forward)  * actionReference.action.ReadValue<Vector2>() * 400;
    }
}
