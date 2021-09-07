using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateCredits : MonoBehaviour
{

    private PhaseControllerScript phaseContoller;
    // Start is called before the first frame update
    void Start()
    {
        phaseContoller = FindObjectOfType<PhaseControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Find("Text").GetComponent<Text>().text = "Credits: " + phaseContoller.credits;
    }
}
