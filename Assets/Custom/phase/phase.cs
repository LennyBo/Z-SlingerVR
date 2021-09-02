using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class phase : MonoBehaviour
{
    public scr_spawner[] spawner;
    public int credits = 101;
    private uint wave_counter = 0;
    [SerializeField] private uint zombies_per_wave;

    private bool isPhase1 = false;
    private List<Object> wave;

    [SerializeField] private Text textWave;
    [SerializeField] private Text textPhase;
    [SerializeField] private Text textDescription;
    [SerializeField] private Text textTimer;
    
    [SerializeField] private GameObject GO_heartLF;
    private Text heartLF;
    [SerializeField] private uint maxHeartLF;

    [SerializeField] private float delay;
    private bool hasStarted;

    [SerializeField] private static float PREPATIME = 10;
    private float prepaTime = PREPATIME;

    // Start is called before the first frame update
    void Start()
    {
        heartLF = GO_heartLF.GetComponent<Text>();
        StartCoroutine(Start2());
    }
    
    private IEnumerator Start2()
    {
        wave = new List<Object>();
        heartLF.text = maxHeartLF + "/" + maxHeartLF;
        GO_heartLF.active = false;
        yield return new WaitForSeconds(delay);
        Phase1();
        GO_heartLF.active = true;
        hasStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
            return;
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


    void Phase1()
    {
        Debug.Log("=== PHASE 1 ===");
        isPhase1 = true;
        
        textWave.text = "Vague " + ++wave_counter;

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
        //Debug.Log("min, max " + min + " " + max);
        foreach (var s in spawner) {
            int i = 0;
            foreach (var o in s.spawn((uint)Random.Range(min, max))) {
                wave.Add(o);
                //Debug.Log(++i);
            }
            //Debug.Log("----");
        }
    }

    private void aFunctionBecauseLambdasDontWork()
    {
        textDescription.text = "";
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
        if (f != 0 || prepaTime <= 0)
        {
            Phase2();
        }
    }
}
