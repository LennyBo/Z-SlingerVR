using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class phase : MonoBehaviour
{
    public scr_spawner[] spawner;


    private bool isPhase1 = false;
    private List<Object> wave;
    public Text textElement;

    // Start is called before the first frame update
    void Start()
    {
        wave = new List<Object>();
        Phase1();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPhase1)
        {
            waitForValidation();
        } else {
            waitForEndOfWave();
        }
    }

    void Phase1()
    {
        Debug.Log("=== PHASE 1 ===");
        isPhase1 = true;

        textElement.text = "Pressez f pour valider";
    }

    void Phase2()
    {
        Debug.Log("=== PHASE 2 ===");
        isPhase1 = false;
        
        textElement.text = "Éliminez tous les zombies !";
        this.Invoke("aFunctionBecauseLambdasDontWork", 2.5f);
        foreach (var s in spawner)
            foreach (var o in s.spawn((uint)Random.Range(3, 10)))
                wave.Add(o);
    }

    private void aFunctionBecauseLambdasDontWork()
    {
        textElement.text = "";
    }

    private void d(float f) {
        Debug.Log("hey " + f);
    }

    void waitForEndOfWave()
    {
        bool isEmpty = true;
        foreach (Object o in wave)
        {
            if (o != null)
            {
                isEmpty = false;
                break;
            }
        }

        if (isEmpty)
            Phase1();
    }

    void waitForValidation()
    {
        float f = Input.GetAxis("Interaction");
        //Debug.Log("f is " + f);
        if (f != 0)
        {
            Phase2();
        }
    }
}
