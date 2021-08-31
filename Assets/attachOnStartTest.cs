using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class attachOnStartTest : MonoBehaviour
{

    public XRBaseControllerInteractor interactor;

    private float delay = 1; //Delay attach 1s

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (delay > 0)
        {
            delay -= Time.deltaTime;
            if (delay < 0)
            {
                interactor.attachTransform = transform;
            }
        }
    }
}
