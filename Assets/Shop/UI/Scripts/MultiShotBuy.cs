using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiShotBuy : UIButton
{
    private SlingShotScript slingScript;

    [SerializeField] private int price;
    [SerializeField] private string text;
    private PhaseControllerScript phaseContoller;

    // Start is called before the first frame update
    void Start()
    {
        Text textPrice = transform.Find("Text").GetComponent<Text>();
        textPrice.text = text + "\n" + price + " $";
        slingScript = FindObjectOfType<SlingShotScript>();
        phaseContoller = FindObjectOfType<PhaseControllerScript>();
        if (slingScript.hasUgrapdedMultiShot)
        {
            transform.Find("WhiteBackground").GetComponent<Image>().color = new Color(0, 255, 0);
            Destroy(transform.Find("ColliderObject").gameObject);
        }
    }

    private void Update()
    {
        
    }


    public override void pressed()
    {
        if (phaseContoller.credits >= price)
        {
            slingScript.upgradeToMultiShot();
            transform.Find("WhiteBackground").GetComponent<Image>().color = new Color(0, 255, 0);
            Destroy(transform.Find("ColliderObject").gameObject);
            phaseContoller.credits -= price;
        }
    }
}
