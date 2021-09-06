using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BuyButton : UIButton
{
    public GameObject bloc;
    [SerializeField] private int price;
    [SerializeField] private string text;

    private phase phaseContoller;


    // Start is called before the first frame update
    void Start()
    {   
        Text textPrice = GetComponent<Text>();
        textPrice.text = text + "\n" + price + " $";
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
