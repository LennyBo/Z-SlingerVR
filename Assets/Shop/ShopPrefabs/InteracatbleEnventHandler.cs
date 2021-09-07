using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteracatbleEnventHandler : MonoBehaviour
{

    private Material defaultMaterial;
    public Material hoverMaterial;

    private void Start()
    {
        defaultMaterial = GetComponent<Renderer>().material;
    }

    public void onHoverEntered()
    {
        //Activate glow
        GetComponent<Renderer>().material = hoverMaterial;
    }

    public void onHoverExit()
    {
        //Disable glow
        GetComponent<Renderer>().material = defaultMaterial;
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
