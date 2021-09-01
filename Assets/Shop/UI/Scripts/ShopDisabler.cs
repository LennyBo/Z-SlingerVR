using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopDisabler : MonoBehaviour
{
    /// <summary>
    /// Show/Hides the shop UI if actionreference is on/off
    /// </summary>
    public InputActionReference actionReference;
    public GameObject shopUI;

    private float oldTouchValue = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float newTouch = actionReference.action.ReadValue<float>();
        if(newTouch != oldTouchValue)
        {
            if(newTouch == 1)
            {
                enableUI();
            }
            else
            {
                disableUI();
            }
            oldTouchValue = newTouch;
        }
    }

    private void disableUI()
    {
        Destroy(transform.GetChild(0).gameObject);
    }

    private void enableUI()
    {
        Instantiate(shopUI, transform);
    }
}
