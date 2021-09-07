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
    public InputActionReference actionReferenceOn;
    /// <summary>
    /// Disables the shop ui no matter what
    /// </summary>
    public InputActionReference actionReferenceOverride;
    public GameObject shopUI;

    private bool _isUIOn;
    private bool isUIOn
    {
        set
        {
            if (value != _isUIOn)
            {
                _isUIOn = value;
                if (_isUIOn)
                {
                    enableUI();
                }
                else
                {
                    disableUI();
                }
            }
        }
        get
        {
            return _isUIOn;
        }
    }

    private float oldTouchValue = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(actionReferenceOverride.action.ReadValue<float>());
        if (actionReferenceOverride.action.ReadValue<float>() == 1)
        {
            isUIOn = false;
        }
        else
        {
            isUIOn = actionReferenceOn.action.ReadValue<float>() == 1;
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
