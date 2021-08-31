using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CursorCollision : MonoBehaviour
{

    public Color enabledColor = Color.cyan;
    public Color disabledColor = Color.white;

    public InputActionReference actionReference;

    private GameObject currentButton;


    private bool lockSwitch = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentButton != null && !lockSwitch && actionReference.action.ReadValue<float>() > 0.9f)
        {
            lockSwitch = true; //Locks until button is released
            if (currentButton.tag == "NavigationButton")
            {
                currentButton.GetComponent<NavigationScript>().pressed();
            }
            else if (currentButton.tag == "BuyButton")
            {
                if(currentButton.GetComponent<spawnBloc>().tryBuy())
                {

                }
            }
        }
        else if (lockSwitch && actionReference.action.ReadValue<float>() < 0.9f)
        {
            lockSwitch = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == gameObject.tag)
        {
            if (currentButton != null)
            {
                disableButton();
            }
            currentButton = other.gameObject.transform.parent.gameObject;
            enableButton();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == gameObject.tag)
        {
            disableButton();
        }
    }

    void enableButton()
    {
        if (currentButton.transform.Find("Text") != null)
        {
            currentButton.transform.Find("Text").GetComponent<Outline>().enabled = true;
            currentButton.transform.Find("WhiteBackground").GetComponent<Image>().color = enabledColor;
        }
        else
        {
            Debug.LogWarning("Text and whitebackground not instantiated yet");
        }
    }

    void disableButton()
    {
        if (currentButton != null)
        {
            currentButton.transform.Find("Text").GetComponent<Outline>().enabled = false;
            currentButton.transform.Find("WhiteBackground").GetComponent<Image>().color = disabledColor;
            currentButton = null;
        }
    }
}
