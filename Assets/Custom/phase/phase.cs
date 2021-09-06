using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class phase : MonoBehaviour
{
    public static uint score;

    public List<scr_spawner> spawner;
    public int credits = 50;
    public int perPhaseBonus = 20;
    private uint wave_counter = 0;
    [SerializeField] private uint zombies_per_wave;

    private bool isPhase1 = false;
    private List<Object> wave;

    [SerializeField] private Text textWave;
    [SerializeField] private Text textPhase;
    [SerializeField] private Text textDescription;
    [SerializeField] private Text textTimer;
    
    [SerializeField] private GameObject GO_heartLF;
    [SerializeField] private GameObject GO_heart;
    private Transform heartBoom;
    private Text textHeartLF;
    [SerializeField] private int maxHeartLF;
    private int currentHeartLF;

    [SerializeField] private float delay;
    private bool hasStarted;

    [SerializeField] private static float PREPATIME = 10;
    private float prepaTime = PREPATIME;

    // Start is called before the first frame update
    void Start()
    {
        credits -= perPhaseBonus;
        currentHeartLF = maxHeartLF;
        textHeartLF = GO_heartLF.GetComponent<Text>();
        wave = new List<Object>();
        textHeartLF.text = maxHeartLF + "/" + maxHeartLF;
        GO_heartLF.active = false;
        heartBoom = GO_heart.transform.Find("Fx_OilSplashHIGH_Root");
        
        StartCoroutine(Start2());
    }
    
    private IEnumerator Start2()
    {
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
        credits += perPhaseBonus;
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
        textDescription.text = "Protégez le jambon !";
        textTimer.text = "";

        prepaTime = PREPATIME;

        this.Invoke("aFunctionBecauseLambdasDontWork", 2.5f);
        spawn();
    }

    private void spawn()
    {
        // select which spawns will actually do the spawn

        int num = Random.Range(1, spawner.Count + 1);
        Debug.Log("there will be " + num + " differents spawns");
        List<int> indexes = new List<int>(num);
        /*
        while(indexes.Count != num) {
            int r = Random.Range(1, spawner.Count + 1);
            if (indexes.Contains(r))
                continue;
            indexes.Add(r);
            Debug.Log("I added " + r);
        }
        */

        // better
        List<int> range = new List<int>();
        
        for (int i = 0; i < spawner.Count; ++i)
            range.Add(i);
        
        for (int i = 0; i < num; ++i) {
            int index = Random.Range(0, range.Count);
            indexes.Add(range[index]);
            range.RemoveAt(index);
        }
        
        // 10% de jeu
        int min = (int)((zombies_per_wave * wave_counter) * 0.9f);
        int max = (int)((zombies_per_wave * wave_counter) * 1.1f);

        int maxZombies = Random.Range(min, max);
        Debug.Log("There is between " + min + " and " + max + " zombies, and it will be " + maxZombies);

        float forkForSpawn = 40/100.0f;
        for (int i = 0; i < num; ++i) {
            var spawn = spawner[indexes[i]];
            
            int approxToSpawn = maxZombies / (num-i);

            int toSpawn;
            if (num-1 == i)
                toSpawn = approxToSpawn;
            else {
                int minWithFork = (int)((float)approxToSpawn * (1-forkForSpawn));
                if (minWithFork < 0)
                    minWithFork = 0;
            
                int maxWithFork = (int)((float)approxToSpawn * (1+forkForSpawn));
                if (maxWithFork > maxZombies)
                    maxWithFork = maxZombies;
                
                toSpawn = Random.Range(minWithFork, maxWithFork);
            }
            
            maxZombies -= toSpawn;

            foreach(Object o in spawn.spawn((uint)toSpawn)) {
                wave.Add(o);
            }
        }
    }

    public void hit(int damages)
    {
        if (currentHeartLF <= 0)
            return;

        currentHeartLF -= damages;
        textHeartLF.text = currentHeartLF + "/" + maxHeartLF;
        
        heartBoom.GetComponent<ParticleSystem>().Play();
        heartBoom.GetComponent<AudioSource>().Play();
		if(currentHeartLF <= 0)
        {
            phase.score = wave_counter;
            FindObjectOfType<LevelLoaderScript>().switchToMenu();
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
