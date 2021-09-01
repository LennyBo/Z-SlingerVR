using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuyButton : UIButton
{
    public GameObject bloc;
    public int price = 10;
    private phase phaseContoller;

    // Start is called before the first frame update
    void Start()
    {
        phaseContoller = FindObjectOfType<phase>();
    }

    public override void pressed()
    {
        if (phaseContoller.credits >= price)
        {
            Vector3 vec = transform.position + transform.rotation.eulerAngles.normalized;
            Instantiate(bloc, transform.position, transform.rotation);
            phaseContoller.credits -= price;
        }
    }
}
