using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteracatbleEnventHandler : MonoBehaviour
{
    
    public void onHoverEntered()
    {
        //Activate glow
        GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    }

    public void onHoverExit()
    {
        //Disable glow
        GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
    }

    public void onSelectEnter()
    {
        //Disable rigidbody
        GetComponent<Collider>().enabled = false;
    }

    public void onSelectExit()
    {
        //Enable rigidbody
        GetComponent<Collider>().enabled = true;
    }

}
