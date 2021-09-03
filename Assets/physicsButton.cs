using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class physicsButton : MonoBehaviour
{
    public UnityEvent onPressed, onReleased;

    [SerializeField] private float threshold = .1f;
    [SerializeField] private float deadZone = .025f;

    private bool _isPressed;
    private Vector3 _startPos;
    private ConfigurableJoint _join;

    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.localPosition;
        _join = GetComponent<ConfigurableJoint>();

    }

    void Update()
    {
        if(!_isPressed && GetValue() + threshold >= 1)
        {
            Pressed();
        }
        else if(_isPressed && GetValue() - threshold <= 0)
        {
            Released();
        }
    }

    private float GetValue()
    {
        var value = Vector3.Distance(_startPos, transform.localPosition)/_join.linearLimit.limit;
        if(Math.Abs(value) < deadZone)
        {
            value = 0;
        }

        return Mathf.Clamp(value, -1, 1);
    }

    private void Pressed()
    {
        _isPressed = true;
        transform.Find("Clicker").GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        //FindObjectOfType<LevelLoaderScript>().switchToMainMap();
        onPressed.Invoke();
        Debug.Log("Pressed");
    }

    private void Released()
    {
        _isPressed = false;
        transform.Find("Clicker").GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        onReleased.Invoke();
        Debug.Log("onReleased");
    }

    // Update is called once per frame
    
}
