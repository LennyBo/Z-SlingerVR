using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class spawnBloc : MonoBehaviour
{
    public GameObject bloc;
    public int price = 10;
    private bool alreadyFired = false;
    private phase phaseContoller;

    // Start is called before the first frame update
    void Start()
    {
        phaseContoller = FindObjectOfType<phase>();
    }

    public bool tryBuy()
    {
        if (phaseContoller.credits >= price)
        {
            Vector3 vec = transform.position + transform.rotation.eulerAngles.normalized;
            Instantiate(bloc, transform.position, transform.rotation);
            phaseContoller.credits -= price;
            return true;
        }
        else
        {
            return false;
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fire2") != 0)
        {
            if (!alreadyFired)
            {
                alreadyFired = true;
                Vector3 vec = transform.position + transform.rotation.eulerAngles.normalized;
                Instantiate(bloc, transform.position,transform.rotation);
            }
        } else {
            alreadyFired = false;
        }
    }
}
