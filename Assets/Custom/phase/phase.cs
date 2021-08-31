using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class phase : MonoBehaviour
{
    public scr_spawner[] spawner;

    private uint wave_counter = 0;
    [SerializeField] private uint zombies_per_wave;

    private bool isPhase1 = false;
    private List<Object> wave;
    [SerializeField] private Text textPhase;
    [SerializeField] private Text textDescription;
    [SerializeField] private Text textTimer;

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


    private string getStringTime()
    {
        if (prepaTime < 0)
            return "-1";
        string ret = "";
        while(prepaTime > 60) {
            ret += "60:";
        }
        ret += Mathf.Ceil(prepaTime).ToString();

        return ret;
    }

    [SerializeField] private static float PREPATIME = 10;
    private float prepaTime = PREPATIME;
    void Phase1()
    {
        Debug.Log("=== PHASE 1 ===");
        isPhase1 = true;
        ++wave_counter;

        textPhase.text = "Phase 1";
        textDescription.text = "Pressez f pour valider, début dans";
        textTimer.text = getStringTime();
    }

    void Phase2()
    {
        Debug.Log("=== PHASE 2 ===");
        isPhase1 = false;
        
        textPhase.text = "Phase 2";
        textDescription.text = "Éliminez tous les zombies !";
        textTimer.text = "";

        prepaTime = PREPATIME;

        this.Invoke("aFunctionBecauseLambdasDontWork", 2.5f);
        uint min = (uint)(zombies_per_wave * wave_counter*0.7f);
        uint max = (uint)(zombies_per_wave * wave_counter*1.5f);
        Debug.Log("min, max " + min + " " + max);
        foreach (var s in spawner) {
            int i = 0;
            foreach (var o in s.spawn((uint)Random.Range(min, max))) {
                wave.Add(o);
                Debug.Log(++i);
            }
            Debug.Log("----");
        }
    }

    private void aFunctionBecauseLambdasDontWork()
    {
        textDescription.text = "";
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
        prepaTime -= Time.deltaTime;
        textTimer.text = getStringTime();

        float f = Input.GetAxis("Interaction");
        //Debug.Log("f is " + f);
        if (f != 0 || prepaTime <= 0)
        {
            Phase2();
        }
    }
}
